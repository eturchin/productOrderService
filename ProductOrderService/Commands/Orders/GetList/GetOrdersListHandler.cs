using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductOrderService.DataContext;
using ProductOrderService.ViewModels;

namespace ProductOrderService.Commands.Orders.GetList;

public class GetOrdersListHandler(AppDbContext context, IMapperBase mapper) : IRequestHandler<GetOrdersListRequest, GetOrdersListResponse>
{
    public async Task<GetOrdersListResponse> Handle(GetOrdersListRequest request, CancellationToken cancellationToken)
    {
        var orders = await context.Orders
            .AsNoTracking()
            .Include(o => o.OrderProducts)
            .ToListAsync(cancellationToken);

        var orderViewModels = mapper.Map<List<OrderViewModel>>(orders);

        return new GetOrdersListResponse
        {
            Message = "Orders have been successfully received.",
            StatusCode = StatusCodes.Status200OK,
            Elements = orderViewModels 
        };
    }
}