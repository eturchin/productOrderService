using MediatR;

namespace ProductOrderService.Commands.Products.Delete;

public class DeleteProductRequest : IRequest<DeleteProductResponse>
{
    public Guid ProductId { get; init; }
}