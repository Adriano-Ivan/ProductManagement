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

    public CreateProductCommandHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateProductCommandValidator(this._productRepository);
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
