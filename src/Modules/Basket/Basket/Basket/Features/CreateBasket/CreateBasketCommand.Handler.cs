namespace Basket.Basket.Features.CreateBasket;

internal class CreateBasketCommandHandler(IBasketRepository repository)
    : ICommandHandler<CreateBasketCommand, CreateBasketCommandResponse>
{
    public async Task<CreateBasketCommandResponse> Handle(CreateBasketCommand command, CancellationToken cancellationToken)
    {
        var shoppingCart = CreateNewBasket(command.ShoppingCart);

        await repository.CreateBasket(shoppingCart, cancellationToken);

        return new CreateBasketCommandResponse(shoppingCart.Id);
    }

    private ShoppingCart CreateNewBasket(ShoppingCartDto shoppingCartDto)
    {
        var newBasket = ShoppingCart.Create(
            Guid.NewGuid(),
            shoppingCartDto.UserName
        );
        
        shoppingCartDto.Items.ForEach(item => newBasket.AddItem(item.ProductId,
            item.Quantity,
            item.Color,
            item.Price,
            item.ProductName));

        return newBasket;
    }
}