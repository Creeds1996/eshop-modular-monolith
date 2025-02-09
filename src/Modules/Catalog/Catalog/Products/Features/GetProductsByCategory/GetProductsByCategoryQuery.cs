namespace Catalog.Products.Features.GetProductsByCategory;

public record GetProductsByCategoryQuery(PaginatedRequest PaginatedRequest, string Category) : IQuery<GetProductsByCategoryQueryResponse>;