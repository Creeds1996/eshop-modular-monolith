namespace Catalog.Products.Features.GetProductsByCategory;

internal class GetProductsByCategoryQueryHandler(CatalogDbContext context)
    : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryQueryResponse>
{
    public async Task<GetProductsByCategoryQueryResponse> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await context.Products
            .AsNoTracking()
            .Where(p => p.Categories.Contains(query.Category))
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);

        var productDtos = products.Adapt<List<ProductDto>>();

        return new GetProductsByCategoryQueryResponse(productDtos);
    }
}