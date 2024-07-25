using MediatR;

namespace ProductManagement.Application.Features.Providers.Commands.DeleteProvider;

public record DeleteProviderCommand(Guid Id) : IRequest<bool>;
