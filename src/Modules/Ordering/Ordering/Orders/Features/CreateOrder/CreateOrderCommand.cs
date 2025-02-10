namespace Ordering.Orders.Features.CreateOrder;

public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderCommandResponse>;