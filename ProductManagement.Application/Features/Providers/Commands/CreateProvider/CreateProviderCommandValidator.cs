using FluentValidation;
using ProductManagement.Application.Contracts;
using ProductManagement.Domain.Extensions;

namespace ProductManagement.Application.Features.Providers.Commands.CreateProvider;

public class CreateProviderCommandValidator : AbstractValidator<CreateProviderCommand>
{
    private readonly IProviderRepository _providerRepository;

    public CreateProviderCommandValidator(IProviderRepository providerRepository)
    {
        _providerRepository = providerRepository;

        RuleFor(p => p.CNPJ)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(60).WithMessage("{PropertyName} must be fewer than 60 characters");

        RuleFor(p => p.Nome)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(250).WithMessage("{PropertyName} must be fewer than 250 characters");

        RuleFor(p => p.Telefone)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(250).WithMessage("{PropertyName} must be fewer than 250 characters");

        RuleFor(p => p.Cep)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull();

        RuleFor(q => q)
            .Must(CnpjValid)
            .WithMessage("Invalid CNPJ");

        RuleFor(q => q)
            .MustAsync(ProviderCnpjUnique)
            .WithMessage("Provider already exists");
    }

    private async Task<bool> ProviderCnpjUnique(CreateProviderCommand command, CancellationToken token)
    {
        return await _providerRepository.IsProviderCnpjUnique(command.CNPJ.FormatCnpjToDatabase());
    }

    private bool CnpjValid(CreateProviderCommand command)
    {
        return command.CNPJ.CnpjIsValid();
    }
}
