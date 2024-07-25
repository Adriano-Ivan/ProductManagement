using MediatR;
using ProductManagement.Application.Contracts;
using ProductManagement.Application.Exceptions;
using ProductManagement.Application.Features.Providers.Commands.DeleteProvider;
using ProductManagement.Domain;

namespace ProductManagement.Application.Features.Providers.Handlers;

public sealed class DeleteProviderCommandHandler : IRequestHandler<DeleteProviderCommand, bool>
{
    private readonly IProviderRepository _providerRepository;

    public DeleteProviderCommandHandler(IProviderRepository providerRepository)
    {
        _providerRepository = providerRepository;
    }

    public async Task<bool> Handle(DeleteProviderCommand request, CancellationToken cancellationToken)
    {
        var provider = await _providerRepository.GetByIdAsync(request.Id);

        if (provider == null)
        {
            throw new NotFoundException(nameof(Provider), request.Id);
        }

        await _providerRepository.DeleteAsync(provider);

        return true;
    }
}
