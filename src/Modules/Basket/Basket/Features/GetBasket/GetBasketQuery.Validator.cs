namespace Basket.Basket.Features.GetBasket;

public class GetBasketQueryValidator : AbstractValidator<GetBasketQuery>
{
    public GetBasketQueryValidator()
    {
        RuleFor(b => b.UserName)
            .NotEmpty()
            .WithMessage("UserName is required.");
    }
}