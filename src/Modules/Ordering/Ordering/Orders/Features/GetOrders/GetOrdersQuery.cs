namespace Ordering.Orders.Features.GetOrders;

public record GetOrdersQuery(PaginatedRequest PaginationRequest) : IQuery<GetOrdersQueryResponse>;