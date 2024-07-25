using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.Features.Products.Commands.CreateProduct;
using ProductManagement.Application.Features.Products.Commands.DeleteProduct;
using ProductManagement.Application.Features.Products.Commands.UpdateProduct;
using ProductManagement.Application.Features.Providers.Commands.CreateProvider;
using ProductManagement.Application.Features.Providers.Commands.DeleteProvider;
using ProductManagement.Application.Features.Providers.Commands.UpdateProvider;

namespace ProductManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProvidersController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProvidersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> PostAsync([FromBody] CreateProviderRequest request)
    {
        var command = new CreateProviderCommand(request.CNPJ, request.Nome, request.Cep, request.Telefone);
        var response = await _mediator.Send(command);

        return Created();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(400)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<bool>> Put(Guid id, [FromBody] UpdateProviderRequest request)
    {
        var command = new UpdateProviderCommand(id, request.CNPJ, request.Nome, request.Cep, request.Telefone);
        await _mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(Guid id)
    {
        var command = new DeleteProviderCommand(id);
        await _mediator.Send(command);

        return NoContent();
    }
}
