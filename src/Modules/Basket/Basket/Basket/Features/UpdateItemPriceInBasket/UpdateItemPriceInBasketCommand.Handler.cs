using Microsoft.Extensions.Caching.Distributed;

namespace Basket.Basket.Features.UpdateItemPriceInBasket;

internal class UpdateItemPriceInBasketCommandHandler(BasketDbContext context, IDistributedCache cache) :
    ICommandHandler<UpdateItemPriceInBasketCommand, UpdateItemPriceInBasketCommandResponse>
{
    public async Task<UpdateItemPriceInBasketCommandResponse> Handle(UpdateItemPriceInBasketCommand command, CancellationToken cancellationToken)
    {
        var itemsToUpdate = await context.ShoppingCartItems
            .Where(x => x.ProductId == command.ProductId)
            .ToListAsync(cancellationToken);

        if (itemsToUpdate.Count == 0)
            return new UpdateItemPriceInBasketCommandResponse(false);
        
        itemsToUpdate.ForEach(item =>
        {
            item.UpdatePrice(command.Price);
        });
        
        var shoppingCartIds = itemsToUpdate
            .Select(x => x.ShoppingCartId)
            .ToList();

        var baskets = await context.ShoppingCarts
            .Where(x => shoppingCartIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        foreach (var basket in baskets)
        {
            await cache.RemoveAsync(basket.UserName, cancellationToken);
        }

        await context.SaveChangesAsync(cancellationToken);

        return new UpdateItemPriceInBasketCommandResponse(true);
    }
}