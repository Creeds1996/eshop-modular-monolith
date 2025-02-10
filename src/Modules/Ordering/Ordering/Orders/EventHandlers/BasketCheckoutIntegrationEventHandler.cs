namespace Ordering.Orders.EventHandlers;

public class BasketCheckoutIntegrationEventHandler(ISender sender, ILogger<BasketCheckoutIntegrationEventHandler> logger) : IConsumer<BasketCheckoutIntegrationEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutIntegrationEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);
        var createOrderCommand = MapToCreateOrderCommand(context.Message);
        await sender.Send(createOrderCommand);
    }

    private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutIntegrationEvent message)
    {
        var addressDto = new AddressDto(message.FirstName, message.LastName, message.Email, message.AddressLine, message.Country, message.State, message.ZipCode);
        var paymentDto = new PaymentDto(message.CardNumber, message.CardNumber, message.Expiration, message.Cvv, message.PaymentMethod);
        var orderId = Guid.NewGuid();

        var orderDto = new OrderDto(
            orderId,
            message.CustomerId,
            message.UserName,
            addressDto,
            addressDto,
            paymentDto,
            [
                new OrderItemDto(orderId, new Guid("1e401c71-56be-49f4-b5f4-bfbb10f6d25c"), 2, 750),
                new OrderItemDto(orderId, new Guid("59c755c9-62cc-41be-9da6-c66547199196"), 1, 400)
            ]
        );

        return new CreateOrderCommand(orderDto);
    }
}