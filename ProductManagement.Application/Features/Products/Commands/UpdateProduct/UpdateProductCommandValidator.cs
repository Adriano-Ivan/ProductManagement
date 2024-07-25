using FluentValidation;
using ProductManagement.Application.Contracts;

namespace ProductManagement.Application.Features.Products.Commands.UpdateProduct;

public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>    
{
    private readonly IProductRepository _productRepository;
    private readonly IProviderRepository _providerRepository;

    public UpdateProductCommandValidator(IProductRepository productRepository, IProviderRepository providerRepository)
    {
        RuleFor(p => p.Id)
            .NotNull()
            .MustAsync(ProductMustExist);

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

        _productRepository = productRepository;
        _providerRepository = providerRepository;
    }

    private async Task<bool> ProductNameUnique(UpdateProductCommand command, CancellationToken token)
    {
        return await _productRepository.IsProductNameUniqueBesidesThisProduct(command.Id, command.Descricao);
    }

    private async Task<bool> ProductMustExist(Guid id, CancellationToken token)
    {
        return await _productRepository.AnyAsync(id);
    }

    private async Task<bool> ProviderMustExistOrBeNull(Guid? providerId, CancellationToken token)
    {
        if(providerId == null) 
        {
            return true;
        }

        return await _providerRepository.AnyAsync(providerId.Value);
    }
}
