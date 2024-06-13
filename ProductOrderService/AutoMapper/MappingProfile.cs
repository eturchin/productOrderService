using AutoMapper;
using ProductOrderService.Commands.Orders.Create;
using ProductOrderService.Commands.Products.Create;
using ProductOrderService.DataContext.Entities;
using ProductOrderService.ViewModels;

namespace ProductOrderService.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateProductRequest, ProductEntity>();

        CreateMap<ProductEntity, ProductViewModel>();
        
        CreateMap<OrderEntity, OrderViewModel>()
            .ForMember(dest => dest.ProductIds, opt => opt.MapFrom(src => src.OrderProducts.Select(p => p.ProductId)));
        
        CreateMap<CreateOrderRequest, OrderEntity>();
    }
}