namespace Catalog.Products.Features.GetProductsByCategory;

public record GetProductsByCategoryQueryResponse(PaginatedResult<ProductDto> Products);