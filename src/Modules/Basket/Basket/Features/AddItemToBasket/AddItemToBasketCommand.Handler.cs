namespace Basket.Basket.Features.AddItemToBasket;

internal class AddItemToBasketCommandHandler(BasketDbContext context)
    : ICommandHandler<AddItemToBasketCommand, AddItemToBasketCommandResponse>
{
    public async Task<AddItemToBasketCommandResponse> Handle(AddItemToBasketCommand command, CancellationToken cancellationToken)
    {
        var shoppingCart = await context.ShoppingCarts
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.UserName == command.UserName, cancellationToken);

        if (shoppingCart is null)
            throw new BasketNotFoundException(command.UserName);
        
        shoppingCart.AddItem(
            command.ShoppingCartItemDto.ProductId,
            command.ShoppingCartItemDto.Quantity,
            command.ShoppingCartItemDto.Color,
            command.ShoppingCartItemDto.Price,
            command.ShoppingCartItemDto.ProductName
        );

        await context.SaveChangesAsync(cancellationToken);

        return new AddItemToBasketCommandResponse(shoppingCart.Id);
    }
}