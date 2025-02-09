namespace Catalog.Products.Features.DeleteProduct;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(p => p.ProductId)
            .NotEmpty()
            .WithMessage("Id is required.");
    }
}