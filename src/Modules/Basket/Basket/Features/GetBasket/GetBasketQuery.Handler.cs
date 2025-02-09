namespace Basket.Basket.Features.GetBasket;

internal class GetBasketQueryHandler(BasketDbContext context) : IQueryHandler<GetBasketQuery, GetBasketQueryResponse>
{
    public async Task<GetBasketQueryResponse> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        var basket = await context.ShoppingCarts
            .AsNoTracking()
            .Include(b => b.Items)
            .FirstOrDefaultAsync(b => b.UserName == query.UserName, cancellationToken);

        if (basket is null)
            throw new BasketNotFoundException(query.UserName);

        var basketDto = basket.Adapt<ShoppingCartDto>();

        return new GetBasketQueryResponse(basketDto);
    }
}