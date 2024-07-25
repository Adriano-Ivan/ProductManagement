using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.Features.Products.Commands.CreateProduct;
using ProductManagement.Application.Features.Products.Commands.DeleteProduct;
using ProductManagement.Application.Features.Products.Commands.UpdateProduct;
using ProductManagement.Application.Features.Products.Queries.GetProductDetails;
using System.Reflection.Metadata.Ecma335;

namespace ProductManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetailsDto>> GetProduct(Guid id)
        {
            var productRequest = new GetProductDetailsQueryRequest(id);
            var product = await _mediator.Send(productRequest);

            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> PostAsync([FromBody] CreateProductRequest request)
        {
            var command = new CreateProductCommand(request.Descricao, request.Marca, request.UnidadeMedida, request.ValorDeCompra);
            var response = await _mediator.Send(command);

            return Created();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<bool>> Put(Guid id, [FromBody] UpdateProductRequest request)
        {
            var command = new UpdateProductCommand(id, request.Descricao, request.Marca, request.UnidadeMedida, request.ValorDeCompra);
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteProductCommand(id);
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
