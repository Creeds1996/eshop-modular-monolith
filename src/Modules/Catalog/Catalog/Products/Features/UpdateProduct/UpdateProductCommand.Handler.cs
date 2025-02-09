namespace Catalog.Products.Features.UpdateProduct;

internal class UpdateProductCommandHandler(CatalogDbContext context)
    : ICommandHandler<UpdateProductCommand, UpdateProductCommandResponse>
{
    public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await context.Products
            .FindAsync([command.Product.Id], cancellationToken);

        if (product is null)
            throw new ProductNotFoundException(command.Product.Id);

        UpdateProductWithNewValues(product, command.Product); 

        context.Products.Update(product);
        await context.SaveChangesAsync(cancellationToken);

        return new UpdateProductCommandResponse(true);
    }

    private static void UpdateProductWithNewValues(Product product, ProductDto productDto)
    {
        product.Update(
            productDto.Name,
            productDto.Categories,
            productDto.Description,
            productDto.ImageFile,
            productDto.Price
        );
    }
}