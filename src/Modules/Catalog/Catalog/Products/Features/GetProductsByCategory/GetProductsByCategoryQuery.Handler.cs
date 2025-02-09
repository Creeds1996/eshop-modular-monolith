namespace Catalog.Products.Features.GetProductsByCategory;

internal class GetProductsByCategoryQueryHandler(CatalogDbContext context)
    : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryQueryResponse>
{
    public async Task<GetProductsByCategoryQueryResponse> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginatedRequest.PageIndex;
        var pageSize = query.PaginatedRequest.PageSize;

        var totalCount = await context.Products
            .LongCountAsync(p => p.Categories.Contains(query.Category), cancellationToken);
        
        var products = await context.Products
            .AsNoTracking()
            .Where(p => p.Categories.Contains(query.Category))
            .OrderBy(p => p.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var productDtos = products.Adapt<List<ProductDto>>();

        return new GetProductsByCategoryQueryResponse(
            new PaginatedResult<ProductDto>(
                pageIndex,
                pageSize,
                totalCount,
                productDtos
            )
        );
    }
}