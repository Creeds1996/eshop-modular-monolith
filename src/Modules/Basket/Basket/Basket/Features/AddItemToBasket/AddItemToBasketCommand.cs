namespace Basket.Basket.Features.AddItemToBasket;

public record AddItemToBasketCommand(string UserName, ShoppingCartItemDto ShoppingCartItemDto) : ICommand<AddItemToBasketCommandResponse>;