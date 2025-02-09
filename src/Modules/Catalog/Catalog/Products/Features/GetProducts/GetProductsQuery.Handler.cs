namespace Catalog.Products.Features.GetProducts;

internal class GetProductsQueryHandler(CatalogDbContext context)
    : IQueryHandler<GetProductsQuery, GetProductsQueryResponse>
{
    public async Task<GetProductsQueryResponse> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginatedRequest.PageIndex;
        var pageSize = query.PaginatedRequest.PageSize;

        var totalCount = await context.Products.LongCountAsync(cancellationToken);
        
        var products = await context.Products
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var productDtos = products.Adapt<List<ProductDto>>();

        return new GetProductsQueryResponse(
            new PaginatedResult<ProductDto>(
                pageIndex, 
                pageSize, 
                totalCount, 
                productDtos
            )
        );
    }
}