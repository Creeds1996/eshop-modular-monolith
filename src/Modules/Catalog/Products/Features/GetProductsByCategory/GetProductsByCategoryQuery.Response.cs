namespace Catalog.Products.Features.GetProductsByCategory;

public record GetProductsByCategoryQueryResponse(IEnumerable<ProductDto> Products);