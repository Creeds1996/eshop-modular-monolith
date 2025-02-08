namespace Catalog.Products.Features.GetProducts;

public record GetProductsQuery(PaginatedRequest PaginatedRequest) : IQuery<GetProductsQueryResponse>;