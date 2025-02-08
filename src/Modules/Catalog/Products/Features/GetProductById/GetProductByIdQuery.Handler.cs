namespace Catalog.Products.Features.GetProductById;

internal class GetProductByIdQueryHandler(CatalogDbContext context)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdQueryResponse>
{
    public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await context.Products
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == query.Id, cancellationToken);
        
        if (product is null)
            throw new Exception($"Product not found: {query.Id}");

        var productDto = product.Adapt<ProductDto>();

        return new GetProductByIdQueryResponse(productDto);
    }
}