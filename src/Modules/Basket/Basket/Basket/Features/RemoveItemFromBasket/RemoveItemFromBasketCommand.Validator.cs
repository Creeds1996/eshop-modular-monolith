namespace Basket.Basket.Features.RemoveItemFromBasket;

public class RemoveItemFromBasketCommandValidator : AbstractValidator<RemoveItemFromBasketCommand>
{
    public RemoveItemFromBasketCommandValidator()
    {
        RuleFor(b => b.UserName)
            .NotEmpty()
            .WithMessage("UserName is required");

        RuleFor(b => b.ProductId)
            .NotEmpty()
            .WithMessage("ProductId is required");
    }
}