namespace Ordering.Orders.Features.GetOrderById;

internal class GetOrderByIdQueryHandler(OrderingDbContext context) : IQueryHandler<GetOrderByIdQuery, GetOrderByIdQueryResponse>
{
    public async Task<GetOrderByIdQueryResponse> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
    {
        var order = await context.Orders
            .AsNoTracking()
            .Include(x => x.Items)
            .SingleOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

        if (order is null)
            throw new OrderNotFoundException(query.Id);

        var orderDto = order.Adapt<OrderDto>();

        return new GetOrderByIdQueryResponse(orderDto);
    }
}