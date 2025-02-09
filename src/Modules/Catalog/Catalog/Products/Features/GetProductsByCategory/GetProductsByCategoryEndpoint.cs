namespace Catalog.Products.Features.GetProductsByCategory;

public record GetProductsByCategoryResponse(PaginatedResult<ProductDto> Products);

public class GetProductsByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async ([AsParameters] PaginatedRequest paginatedRequest, string category, ISender sender) =>
        {
            var result = await sender.Send(new GetProductsByCategoryQuery(paginatedRequest, category));

            var response = result.Adapt<GetProductsByCategoryResponse>();

            return Results.Ok(response);
        })
        .WithName("GetProductsByCategory")
        .Produces<GetProductsByCategoryResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products by Category")
        .WithDescription("Get Products by Category");
    }
}