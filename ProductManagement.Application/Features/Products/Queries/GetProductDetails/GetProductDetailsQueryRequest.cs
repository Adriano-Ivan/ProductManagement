using MediatR;

namespace ProductManagement.Application.Features.Products.Queries.GetProductDetails;

public record GetProductDetailsQueryRequest(Guid productId) : IRequest<ProductDetailsDto>;
