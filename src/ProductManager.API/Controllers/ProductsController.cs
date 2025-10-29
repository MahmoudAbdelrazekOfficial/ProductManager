using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductManager.Application.DTOs;
using ProductManager.Application.Features.Products.Commands;
using ProductManager.Application.Features.Products.Queries;

namespace ProductManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetProductsQuery());

        if (!result.IsSuccess)
            return BadRequest(new { isSuccess = false, error = result.Error, message = "Failed to find product" });

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _mediator.Send(new GetProductByIdQuery(id));


        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
    {
        var result = await _mediator.Send(new CreateProductCommand(dto));

        if (!result.IsSuccess)
            return BadRequest(new { isSuccess = false, error = result.Error, message = "Failed to create product" });

        return CreatedAtAction(nameof(Get), new { id = result.Data!.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductDto dto)
    {
        var result = await _mediator.Send(new UpdateProductCommand(id, dto));

        if (!result.IsSuccess)
            return NotFound(new { isSuccess = false, error = result.Error, message = "Product not found" });

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteProductCommand(id));

        if (!result.IsSuccess)
            return NotFound(new { isSuccess = false, error = result.Error, message = "Product not found" });

        return Ok(result);
    }
}
