namespace Ordering.Orders.Features.GetOrderById;

public record GetOrderByIdQuery(Guid Id) : IQuery<GetOrderByIdQueryResponse>;