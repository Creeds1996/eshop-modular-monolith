namespace Catalog.Products.Features.DeleteProduct;

internal class DeleteProductCommandHandler(CatalogDbContext context)
    : ICommandHandler<DeleteProductCommand, DeleteProductCommandResponse>
{
    public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await context.Products
            .FindAsync([command.ProductId], cancellationToken);
        
        if (product is null)
            throw new Exception($"Product not found: {command.ProductId}");
        
        context.Products.Remove(product);
        await context.SaveChangesAsync(cancellationToken);

        return new DeleteProductCommandResponse(true);
    }
}
