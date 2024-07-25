using MediatR;
using ProductManagement.Application.Contracts;
using ProductManagement.Application.Exceptions;
using ProductManagement.Application.Features.Products.Commands.DeleteProduct;
using ProductManagement.Domain;

namespace ProductManagement.Application.Features.Products.Handlers;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);

        if (product == null)
        {
            throw new NotFoundException(nameof(Product), request.Id);
        }

        await _productRepository.DeleteAsync(product);

        return true;
    }
}
