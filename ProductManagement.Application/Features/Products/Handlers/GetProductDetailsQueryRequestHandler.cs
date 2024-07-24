using AutoMapper;
using MediatR;
using ProductManagement.Application.Contracts;
using ProductManagement.Application.Features.Products.Queries.GetProductDetails;

namespace ProductManagement.Application.Features.Products.Handlers;

public sealed class GetProductDetailsQueryRequestHandler : IRequestHandler<GetProductDetailsQueryRequest, ProductDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public GetProductDetailsQueryRequestHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<ProductDetailsDto> Handle(GetProductDetailsQueryRequest request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.productId);

        //if (product == null)
        //{
        //    throw new NotFoundException(nameof(Product), request.productId);
        //}

        var productDto = _mapper.Map<ProductDetailsDto>(product);

        return productDto;
    }
}
