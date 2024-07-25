using FluentValidation;
using ProductManagement.Application.Contracts;
using ProductManagement.Application.Features.Products.Commands.UpdateProduct;

namespace ProductManagement.Application.Features.Products.Commands.CreateProduct;

public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IProviderRepository _providerRepository;

    public CreateProductCommandValidator(IProductRepository productRepository, IProviderRepository providerRepository)
    {
        RuleFor(p => p.ProviderId)
            .MustAsync(ProviderMustExistOrBeNull);

        RuleFor(p => p.Descricao)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(250).WithMessage("{PropertyName} must be fewer than 250 characters");

        RuleFor(p => p.Marca)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(250).WithMessage("{PropertyName} must be fewer than 250 characters");

        RuleFor(p => p.ValorDeCompra)
            .NotNull().WithMessage("{PropertyName} is required")
            .NotNull()
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

        RuleFor(p => p.UnidadeMedida)
            .IsInEnum();

        RuleFor(q => q)
            .MustAsync(ProductNameUnique)
            .WithMessage("Product already exists");

        this._productRepository = productRepository;
        _providerRepository = providerRepository;
    }

    private async Task<bool> ProductNameUnique(CreateProductCommand command, CancellationToken token)
    {
        return await _productRepository.IsProductNameUnique(command.Descricao);
    }

    private async Task<bool> ProviderMustExistOrBeNull(Guid? providerId, CancellationToken token)
    {
        if (providerId == null)
        {
            return true;
        }

        return await _providerRepository.AnyAsync(providerId.Value);
    }
}
