namespace Ordering.Orders.Features.GetOrderById;

public record GetOrderByIdResponse(OrderDto Order);

public class GetOrderByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetOrderByIdQuery(id));

            var response = result.Adapt<GetOrderByIdResponse>();

            return Results.Ok(response);
        })
        .WithName("GetOrderById")
        .Produces<GetOrderByIdResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Order By Id")
        .WithDescription("Get Order By Id");
    }
}