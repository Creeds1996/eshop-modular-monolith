namespace Basket.Basket.Features.CreateBasket;

public class CreateBasketCommandValidator : AbstractValidator<CreateBasketCommand>
{
    public CreateBasketCommandValidator()
    {
        RuleFor(b => b.ShoppingCart.UserName)
            .NotEmpty()
            .WithMessage("UserName is required.");
    }
}