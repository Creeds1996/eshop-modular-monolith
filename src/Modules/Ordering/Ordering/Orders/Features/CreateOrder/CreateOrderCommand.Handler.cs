namespace Ordering.Orders.Features.CreateOrder;

internal class CreateOrderCommandHandler(OrderingDbContext context) : ICommandHandler<CreateOrderCommand, CreateOrderCommandResponse>
{
    public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = CreateNewOrder(command.Order);

        await context.Orders.AddAsync(order, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return new CreateOrderCommandResponse(order.Id);
    }

    private static Order CreateNewOrder(OrderDto orderDto)
    {
        var shippingAddress = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName,
            orderDto.ShippingAddress.Email, orderDto.ShippingAddress.AddressLine,
            orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode);
        
        var billingAddress = Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName,
            orderDto.BillingAddress.Email, orderDto.BillingAddress.AddressLine,
            orderDto.BillingAddress.Country, orderDto.BillingAddress.State, orderDto.BillingAddress.ZipCode);

        var newOrder = Order.Create(
            Guid.NewGuid(),
            orderDto.CustomerId,
            orderDto.OrderName,
            shippingAddress,
            billingAddress,
            Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod)
        );
        
        orderDto.Items.ForEach(item =>
        {
            newOrder.Add(
                item.ProductId,
                item.Quantity,
                item.Price
            );
        });
        
        return newOrder;
    }
}