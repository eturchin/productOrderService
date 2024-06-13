using MediatR;

namespace ProductOrderService.Commands.Products.Create;

public class CreateProductRequest : IRequest<CreateProductResponse>
{
    public string Name { get; set; }
    
    public decimal Price { get; set; }
}