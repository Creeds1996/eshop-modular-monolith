namespace Ordering.Orders.Features.GetOrders;

public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);

public class GetOrdersEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async ([AsParameters]PaginatedRequest request, ISender sender) =>
        {
            var result = await sender.Send(new GetOrdersQuery(request));

            var response = result.Adapt<GetOrdersResponse>();

            return Results.Ok(response);
        })
        .WithName("GetOrders")
        .Produces<GetOrdersResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Orders")
        .WithDescription("Get Orders");
    }
}