namespace Catalog.Products.Features.CreateProduct;

internal class CreateProductHandler(CatalogDbContext context)
    : ICommandHandler<CreateProductCommand, CreateProductCommandResponse>
{
    public async Task<CreateProductCommandResponse> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = CreateNewProduct(command.Product);

        context.Products.Add(product);
        await context.SaveChangesAsync(cancellationToken);

        return new CreateProductCommandResponse(product.Id);
    }
    
    private Product CreateNewProduct(ProductDto productDto)
    {
        return Product.Create(
            Guid.NewGuid(),
            productDto.Name,
            productDto.Categories,
            productDto.Description,
            productDto.ImageFile,
            productDto.Price
        );
    }
}