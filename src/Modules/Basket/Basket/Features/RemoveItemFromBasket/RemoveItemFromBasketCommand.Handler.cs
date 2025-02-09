namespace Basket.Basket.Features.RemoveItemFromBasket;

internal class RemoveItemFromBasketCommandHandler(BasketDbContext context)
    : ICommandHandler<RemoveItemFromBasketCommand, RemoveItemFromBasketCommandResponse>
{
    public async Task<RemoveItemFromBasketCommandResponse> Handle(RemoveItemFromBasketCommand command, CancellationToken cancellationToken)
    {
        var shoppingCart = await context.ShoppingCarts
            .Include(x => x.Items)
            .SingleOrDefaultAsync(x => x.UserName == command.UserName, cancellationToken);

        if (shoppingCart is null)
            throw new BasketNotFoundException(command.UserName);
        
        shoppingCart.RemoveItem(command.ProductId);
        
        await context.SaveChangesAsync(cancellationToken);

        return new RemoveItemFromBasketCommandResponse(shoppingCart.Id);
    }
}