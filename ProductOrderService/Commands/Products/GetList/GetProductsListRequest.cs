using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductOrderService.AbstractResponses;
using ProductOrderService.DataContext;
using ProductOrderService.ViewModels;

namespace ProductOrderService.Commands.Products.GetList;

public class GetProductsListRequest : IRequest<GetProductsListResponse>;

public class GetProductsListResponse : PageViewResponse<ProductViewModel>;

public class GetProductsListHandler(AppDbContext context, IMapper mapper)
    : IRequestHandler<GetProductsListRequest, GetProductsListResponse>
{
    public async Task<GetProductsListResponse> Handle(GetProductsListRequest request, CancellationToken cancellationToken)
    {
        var products = await context.Products
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        
        var productViewModels = mapper.Map<List<ProductViewModel>>(products);

        return new GetProductsListResponse
        {
            Message = "Products have been successfully received.",
            StatusCode = StatusCodes.Status200OK,
            Elements = productViewModels 
        };
    }
}