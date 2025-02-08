namespace Catalog.Products.Features.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(p => p.Product.Name)
            .NotEmpty()
            .WithMessage("Name is required.");

        RuleFor(p => p.Product.Categories)
            .NotEmpty()
            .WithMessage("Categories are required.");
        
        RuleFor(p => p.Product.Description)
            .NotEmpty()
            .WithMessage("Description is required.");

        RuleFor(p => p.Product.ImageFile)
            .NotEmpty()
            .WithMessage("Image file is required.");

        RuleFor(p => p.Product.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0.");
    }
}