using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductOrderService.DataContext;

namespace ProductOrderService.Commands.Products.Delete;

public class DeleteProductHandler(AppDbContext context) : IRequestHandler<DeleteProductRequest, DeleteProductResponse>
{
    public async Task<DeleteProductResponse> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        var product = await context.Products
            .Include(p => p.OrderProducts)
            .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);

        if (product == null)
        {
            return new DeleteProductResponse
            {
                Message = "Product not found.",
                StatusCode = StatusCodes.Status404NotFound
            };
        }

        context.Products.Remove(product);
        await context.SaveChangesAsync(cancellationToken);

        return new DeleteProductResponse
        {
            Message = "Product deleted successfully.",
            StatusCode = StatusCodes.Status200OK
        };
    }
}