using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductOrderService.DataContext;
using ProductOrderService.DataContext.Entities;
using ProductOrderService.ViewModels;

namespace ProductOrderService.Commands.Orders.Create;

public class CreateOrderHandler(AppDbContext context, IMapperBase mapper) : IRequestHandler<CreateOrderRequest, CreateOrderResponse>
{
    public async Task<CreateOrderResponse> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var orderEntity = mapper.Map<OrderEntity>(request);
        
        var products = await context.Products
            .Where(p => request.ProductIds.Contains(p.Id))
            .ToListAsync(cancellationToken);

        orderEntity.OrderProducts = products.Select(p => new OrderProductEntity
        {
            Order = orderEntity,
            Product = p
        }).ToList();

        context.Orders.Add(orderEntity);
        await context.SaveChangesAsync(cancellationToken);

        var orderViewModel = mapper.Map<OrderViewModel>(orderEntity);
        
        return new CreateOrderResponse
        {
            Message = "Order have been successfully created.",
            StatusCode = StatusCodes.Status201Created,
            Item = orderViewModel 
        };
    }
}