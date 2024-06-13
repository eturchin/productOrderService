using ProductOrderService.AbstractResponses;

namespace ProductOrderService.Commands.Orders.Delete;

public class DeleteOrderResponse : IResponse
{
    public string Message { get; set; }
    
    public int StatusCode { get; set; }
}