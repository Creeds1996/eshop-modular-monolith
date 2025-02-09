namespace Basket.Basket.Features.AddItemToBasket;

public record AddItemToBasketRequest(string UserName, ShoppingCartItemDto ShoppingCartItem);

public record AddItemToBasketResponse(Guid Id);

public class AddItemToBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket/{userName}/items", async ([FromRoute] string userName, [FromBody]AddItemToBasketRequest request, ISender sender) =>
        {
            var command = new AddItemToBasketCommand(userName, request.ShoppingCartItem);
            
            var result = await sender.Send(command);

            var response = result.Adapt<AddItemToBasketResponse>();

            return Results.Created($"/basket/{response.Id}", response);
        })
        .WithName("AddItemToBasket")
        .Produces<AddItemToBasketResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Add Item To Basket")
        .WithDescription("Add Item To Basket");
    }
}