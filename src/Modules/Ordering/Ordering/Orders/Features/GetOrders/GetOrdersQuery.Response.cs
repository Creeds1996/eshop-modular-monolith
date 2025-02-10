namespace Ordering.Orders.Features.GetOrders;

public record GetOrdersQueryResponse(PaginatedResult<OrderDto> Orders);