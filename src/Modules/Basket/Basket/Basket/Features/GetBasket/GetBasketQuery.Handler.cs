namespace Basket.Basket.Features.GetBasket;

internal class GetBasketQueryHandler(IBasketRepository repository) : IQueryHandler<GetBasketQuery, GetBasketQueryResponse>
{
    public async Task<GetBasketQueryResponse> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        var basket = await repository.GetBasket(query.UserName, cancellationToken: cancellationToken);

        var basketDto = basket.Adapt<ShoppingCartDto>();

        return new GetBasketQueryResponse(basketDto);
    }
}