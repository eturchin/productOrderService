using ProductOrderService.AbstractResponses;

namespace ProductOrderService.Commands.Products.Delete;

public class DeleteProductResponse : IResponse
{
    public string Message { get; set; }
    
    public int StatusCode { get; set; }
}