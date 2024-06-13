using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductOrderService.Commands.Orders.Create;
using ProductOrderService.Commands.Orders.Delete;
using ProductOrderService.Commands.Orders.GetList;

namespace ProductOrderService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(GetOrdersListResponse), 200)]
    public async Task<ActionResult<GetOrdersListResponse>> GetOrders() =>
        Ok(await sender.Send(new GetOrdersListRequest()));

    [HttpPost]
    [ProducesResponseType(typeof(CreateOrderResponse), 201)]
    public async Task<ActionResult<CreateOrderResponse>> CreateOrder(CreateOrderRequest request) =>
        Ok(await sender.Send(request));

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(DeleteOrderResponse), 200)]
    public async Task<IActionResult> DeleteOrder(Guid id) =>
        Ok(await sender.Send(new DeleteOrderRequest { Id = id }));
}