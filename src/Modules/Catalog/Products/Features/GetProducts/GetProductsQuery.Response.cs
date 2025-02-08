namespace Catalog.Products.Features.GetProducts;

public record GetProductsQueryResponse(PaginatedResult<ProductDto> Products);