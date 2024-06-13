using MediatR;

namespace ProductOrderService.Commands.Orders.Delete;

public class DeleteOrderRequest : IRequest<DeleteOrderResponse>
{
    public Guid Id { get; init; }
}