using Shared.Exceptions;

namespace Basket.Basket.Exceptions;

public class BasketNotFoundException : NotFoundException
{
    public BasketNotFoundException(string username) : base("ShoppingCart", username)
    {
    }
}