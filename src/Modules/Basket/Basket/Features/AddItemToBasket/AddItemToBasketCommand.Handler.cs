namespace Basket.Basket.Features.AddItemToBasket;

internal class AddItemToBasketCommandHandler(IBasketRepository repository)
    : ICommandHandler<AddItemToBasketCommand, AddItemToBasketCommandResponse>
{
    public async Task<AddItemToBasketCommandResponse> Handle(AddItemToBasketCommand command, CancellationToken cancellationToken)
    {
        var shoppingCart = await repository.GetBasket(command.UserName, false, cancellationToken);
        
        shoppingCart.AddItem(
            command.ShoppingCartItemDto.ProductId,
            command.ShoppingCartItemDto.Quantity,
            command.ShoppingCartItemDto.Color,
            command.ShoppingCartItemDto.Price,
            command.ShoppingCartItemDto.ProductName
        );

        await repository.SaveChangesAsync(command.UserName, cancellationToken);

        return new AddItemToBasketCommandResponse(shoppingCart.Id);
    }
}