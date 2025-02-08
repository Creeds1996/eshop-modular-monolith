namespace Catalog.Products.Features.GetProducts;

internal class GetProductsQueryHandler(CatalogDbContext context)
    : IQueryHandler<GetProductsQuery, GetProductsQueryResponse>
{
    public async Task<GetProductsQueryResponse> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await context.Products
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);

        var productDtos = products.Adapt<List<ProductDto>>();

        return new GetProductsQueryResponse(productDtos);
    }
}