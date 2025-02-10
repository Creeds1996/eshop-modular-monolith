namespace Basket.Basket.Features.CheckoutBasket;

public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator()
    {
        RuleFor(x => x.BasketCheckout).NotNull().WithMessage("Basket checkout cannot be null");
        RuleFor(x => x.BasketCheckout.UserName).NotEmpty().WithMessage("Username is required.");
    }
}