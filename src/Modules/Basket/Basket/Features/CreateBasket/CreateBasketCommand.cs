using Basket.Basket.Dtos;
using Shared.CQRS;

namespace Basket.Basket.Features.CreateBasket;

public record CreateBasketCommand(ShoppingCartDto ShoppingCart) : ICommand<CreateBasketCommandResponse>;