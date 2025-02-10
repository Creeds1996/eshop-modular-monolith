namespace Ordering.Orders.Features.DeleteOrder;

internal class DeleteOrderCommandHandler(OrderingDbContext context) : ICommandHandler<DeleteOrderCommand, DeleteOrderCommandResponse>
{
    public async Task<DeleteOrderCommandResponse> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await context.Orders
            .FindAsync([command.OrderId], cancellationToken);

        if (order is null)
            throw new OrderNotFoundException(command.OrderId);
        
        context.Orders.Remove(order);
        await context.SaveChangesAsync(cancellationToken);
        return new DeleteOrderCommandResponse(true);
    }
}