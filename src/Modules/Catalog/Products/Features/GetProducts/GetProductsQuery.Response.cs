namespace Catalog.Products.Features.GetProducts;

public record GetProductsQueryResponse(IEnumerable<ProductDto> Products);