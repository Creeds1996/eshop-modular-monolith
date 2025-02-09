using Catalog.Contracts.Products.Features.GetProductById;

namespace Basket.Basket.Features.AddItemToBasket;

internal class AddItemToBasketCommandHandler(IBasketRepository repository, ISender sender)
    : ICommandHandler<AddItemToBasketCommand, AddItemToBasketCommandResponse>
{
    public async Task<AddItemToBasketCommandResponse> Handle(AddItemToBasketCommand command, CancellationToken cancellationToken)
    {
        var shoppingCart = await repository.GetBasket(command.UserName, false, cancellationToken);

        var productResult = await sender.Send(new GetProductByIdQuery(command.ShoppingCartItemDto.ProductId), cancellationToken);
        
        shoppingCart.AddItem(
            command.ShoppingCartItemDto.ProductId,
            command.ShoppingCartItemDto.Quantity,
            command.ShoppingCartItemDto.Color,
            productResult.Product.Price,
            productResult.Product.Name
        );

        await repository.SaveChangesAsync(command.UserName, cancellationToken);

        return new AddItemToBasketCommandResponse(shoppingCart.Id);
    }
}