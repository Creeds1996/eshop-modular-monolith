namespace Basket.Basket.Features.DeleteBasket;

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(b => b.UserName)
            .NotEmpty()
            .WithMessage("UserName is required.");
    }
}