using MediatR;

namespace ProductOrderService.Commands.Orders.Create;

public class CreateOrderRequest : IRequest<CreateOrderResponse>
{
    public DateTime OrderDate { get; init; }
    
    public List<Guid> ProductIds { get; init; }
}