namespace Basket.Basket.Features.DeleteBasket;

internal class DeleteBasketCommandHandler(BasketDbContext context)
    : ICommandHandler<DeleteBasketCommand, DeleteBasketCommandResponse>
{
    public async Task<DeleteBasketCommandResponse> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await context.ShoppingCarts
            .SingleOrDefaultAsync(b => b.UserName == command.UserName, cancellationToken);

        if (basket is null)
            throw new BasketNotFoundException(command.UserName);

        context.ShoppingCarts.Remove(basket);
        await context.SaveChangesAsync(cancellationToken);

        return new DeleteBasketCommandResponse(true);
    }
}