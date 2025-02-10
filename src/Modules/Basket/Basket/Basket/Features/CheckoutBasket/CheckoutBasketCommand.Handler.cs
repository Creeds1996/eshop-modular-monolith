using System.Text.Json;
using MassTransit;
using Shared.Messaging.Events;

namespace Basket.Basket.Features.CheckoutBasket;

internal class CheckoutBasketCommandHandler(BasketDbContext context, IBus bus) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketCommandResponse>
{
    public async Task<CheckoutBasketCommandResponse> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var basket = await context.ShoppingCarts
                .Include(x => x.Items)
                .SingleOrDefaultAsync(x => x.UserName == command.BasketCheckout.UserName, cancellationToken);

            if (basket is null)
                throw new BasketNotFoundException(command.BasketCheckout.UserName);

            var eventMessage = command.Adapt<BasketCheckoutIntegrationEvent>();
            eventMessage.TotalPrice = basket.TotalPrice;

            var outboxMessage = new OutboxMessage
            {
                Id = Guid.NewGuid(),
                Type = typeof(BasketCheckoutIntegrationEvent).AssemblyQualifiedName!,
                Content = JsonSerializer.Serialize(eventMessage),
                OccurredOn = DateTime.UtcNow
            };

            await context.OutboxMessages.AddAsync(outboxMessage, cancellationToken);

            context.ShoppingCarts.Remove(basket);

            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return new CheckoutBasketCommandResponse(true);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            return new CheckoutBasketCommandResponse(false);
        }
    }
}