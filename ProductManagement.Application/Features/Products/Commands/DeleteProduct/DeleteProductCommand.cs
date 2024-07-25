using MediatR;

namespace ProductManagement.Application.Features.Products.Commands.DeleteProduct;

public record DeleteProductCommand(Guid Id): IRequest<bool>;
