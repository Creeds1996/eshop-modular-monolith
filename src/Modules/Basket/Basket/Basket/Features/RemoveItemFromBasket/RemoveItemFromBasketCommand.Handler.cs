namespace Basket.Basket.Features.RemoveItemFromBasket;

internal class RemoveItemFromBasketCommandHandler(IBasketRepository repository)
    : ICommandHandler<RemoveItemFromBasketCommand, RemoveItemFromBasketCommandResponse>
{
    public async Task<RemoveItemFromBasketCommandResponse> Handle(RemoveItemFromBasketCommand command, CancellationToken cancellationToken)
    {
        var shoppingCart = await repository.GetBasket(command.UserName, false, cancellationToken);
        
        shoppingCart.RemoveItem(command.ProductId);
        
        await repository.SaveChangesAsync(command.UserName, cancellationToken);

        return new RemoveItemFromBasketCommandResponse(shoppingCart.Id);
    }
}