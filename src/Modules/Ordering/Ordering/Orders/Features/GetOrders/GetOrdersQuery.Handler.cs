namespace Ordering.Orders.Features.GetOrders;

internal class GetOrdersQueryHandler(OrderingDbContext context) : IQueryHandler<GetOrdersQuery, GetOrdersQueryResponse>
{
    public async Task<GetOrdersQueryResponse> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await context.Orders.LongCountAsync(cancellationToken);

        var orders = await context.Orders
            .AsNoTracking()
            .Include(x => x.Items)
            .OrderBy(x => x.OrderName)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        
        var orderDtos = orders.Adapt<List<OrderDto>>();

        return new GetOrdersQueryResponse(
            new PaginatedResult<OrderDto>(
                pageIndex,
                pageSize,
                totalCount,
                orderDtos
            )
        );
    }
}