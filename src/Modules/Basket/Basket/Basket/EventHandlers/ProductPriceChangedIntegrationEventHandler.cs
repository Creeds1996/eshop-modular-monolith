﻿using Basket.Basket.Features.UpdateItemPriceInBasket;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Messaging.Events;

namespace Basket.Basket.EventHandlers;

public class ProductPriceChangedIntegrationEventHandler(ISender sender, ILogger<ProductPriceChangedIntegrationEventHandler> logger)
    : IConsumer<ProductPriceChangedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<ProductPriceChangedIntegrationEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var command = new UpdateItemPriceInBasketCommand(context.Message.ProductId, context.Message.Price);
        
        var result = await sender.Send(command);
        
        if (!result.IsSuccess)
            logger.LogError("Error updating price in basket for product {ProductId}", context.Message.ProductId);
        else
            logger.LogInformation("Price updated in basket for product {ProductId}", context.Message.ProductId);
    }
}