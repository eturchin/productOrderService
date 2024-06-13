using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductOrderService.DataContext;

namespace ProductOrderService.Commands.Orders.Delete;

public class DeleteOrderHandler(AppDbContext context) : IRequestHandler<DeleteOrderRequest, DeleteOrderResponse>
{
    public async Task<DeleteOrderResponse> Handle(DeleteOrderRequest request, CancellationToken cancellationToken)
    {
        var order = await context.Orders
            .Include(o => o.OrderProducts)
            .FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

        if (order == null)
        {
            return new DeleteOrderResponse
            {
                Message = "Order not found.",
                StatusCode = StatusCodes.Status404NotFound
            };
        }

        context.OrderProducts.RemoveRange(order.OrderProducts);
        context.Orders.Remove(order);
        await context.SaveChangesAsync(cancellationToken);

        return new DeleteOrderResponse
        {
            Message = "Order deleted successfully.",
            StatusCode = StatusCodes.Status200OK
        };
    }
}