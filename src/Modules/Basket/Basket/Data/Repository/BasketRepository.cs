namespace Basket.Data.Repository;

public class BasketRepository(BasketDbContext context) : IBasketRepository
{
    public async Task<ShoppingCart> GetBasket(string userName, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        var query = context.ShoppingCarts
            .Include(x => x.Items)
            .Where(x => x.UserName == userName);

        if (asNoTracking)
            query.AsNoTracking();

        return await query.SingleOrDefaultAsync(cancellationToken) ?? throw new BasketNotFoundException(userName);
    }

    public async Task<ShoppingCart> CreateBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        context.ShoppingCarts.Add(basket);
        await context.SaveChangesAsync(cancellationToken);
        return basket;
    }

    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        var basket = await GetBasket(userName, false, cancellationToken);
        context.ShoppingCarts.Remove(basket);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<int> SaveChangesAsync(string? userName, CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
}