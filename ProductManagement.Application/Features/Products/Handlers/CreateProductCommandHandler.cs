using AutoMapper;
using MediatR;
using ProductManagement.Application.Contracts;
using ProductManagement.Application.Exceptions;
using ProductManagement.Application.Features.Products.Commands.CreateProduct;

namespace ProductManagement.Application.Features.Products.Handlers;

public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;
    private readonly IProviderRepository _providerRepository;

    public CreateProductCommandHandler(IMapper mapper, IProductRepository productRepository, IProviderRepository providerRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
        _providerRepository = providerRepository;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateProductCommandValidator(this._productRepository, this._providerRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
        {
            throw new BadRequestException("Invalid Product", validationResult);
        }

        var product = _mapper.Map<Domain.Product>(request);

        var createdProduct = await _productRepository.CreateAsync(product);

        return createdProduct.Id;
    }
}
