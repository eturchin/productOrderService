using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductOrderService.Commands.Products.Create;
using ProductOrderService.Commands.Products.Delete;
using ProductOrderService.Commands.Products.GetList;

namespace ProductOrderService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(GetProductsListResponse), 200)]
    public async Task<ActionResult<GetProductsListResponse>> GetProducts() =>
        Ok(await sender.Send(new GetProductsListRequest()));

    [HttpPost]
    [ProducesResponseType(typeof(CreateProductResponse), 201)]
    public async Task<ActionResult<CreateProductResponse>> CreateProduct(CreateProductRequest request) =>
        Ok(await sender.Send(request));

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(DeleteProductResponse), 200)]
    public async Task<IActionResult> DeleteProduct(Guid id) =>
        Ok(await sender.Send(new DeleteProductRequest { ProductId = id }));
}