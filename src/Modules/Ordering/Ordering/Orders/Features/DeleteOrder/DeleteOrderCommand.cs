namespace Ordering.Orders.Features.DeleteOrder;

public record DeleteOrderCommand(Guid OrderId) : ICommand<DeleteOrderCommandResponse>;