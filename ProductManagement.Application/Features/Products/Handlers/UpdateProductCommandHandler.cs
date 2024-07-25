using AutoMapper;
using MediatR;
using ProductManagement.Application.Contracts;
using ProductManagement.Application.Exceptions;
using ProductManagement.Application.Features.Products.Commands.UpdateProduct;

namespace ProductManagement.Application.Features.Products.Handlers;

public sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;
    private readonly IProviderRepository _providerRepository;

    public UpdateProductCommandHandler(IMapper mapper, IProductRepository productRepository, IProviderRepository providerRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
        _providerRepository = providerRepository;
    }

    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateProductCommandValidator(this._productRepository, this._providerRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
        {
            throw new BadRequestException("Invalid Product", validationResult);
        }

        var product = _mapper.Map<Domain.Product>(request);

        await _productRepository.UpdateAsync(product);

        return true;
    }
}
