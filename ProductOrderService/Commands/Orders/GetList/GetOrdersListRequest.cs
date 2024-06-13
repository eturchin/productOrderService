using MediatR;

namespace ProductOrderService.Commands.Orders.GetList;

public class GetOrdersListRequest : IRequest<GetOrdersListResponse>;