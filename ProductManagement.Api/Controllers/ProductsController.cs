using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.Features.Products.Commands.CreateProduct;
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
    }
}
