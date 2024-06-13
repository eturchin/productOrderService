using AutoMapper;
using MediatR;
using ProductOrderService.DataContext;
using ProductOrderService.DataContext.Entities;
using ProductOrderService.ViewModels;

namespace ProductOrderService.Commands.Products.Create;

public class CreateProductHandler(AppDbContext context, IMapperBase mapper)
    : IRequestHandler<CreateProductRequest, CreateProductResponse>
{
    public async Task<CreateProductResponse> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var productEntity = mapper.Map<ProductEntity>(request);
        
        context.Products.Add(productEntity);
        await context.SaveChangesAsync(cancellationToken);

        var productViewModel = mapper.Map<ProductViewModel>(productEntity);
        
        return new CreateProductResponse
        {
            Message = "Product have been successfully created.",
            StatusCode = StatusCodes.Status201Created,
            Item = productViewModel 
        };
    }
}