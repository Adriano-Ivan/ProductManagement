using FluentValidation;
using ProductManagement.Application.Contracts;
using ProductManagement.Application.Features.Products.Commands.UpdateProduct;

namespace ProductManagement.Application.Features.Products.Commands.CreateProduct;

public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandValidator(IProductRepository productRepository)
    {
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
    }

    private async Task<bool> ProductNameUnique(CreateProductCommand command, CancellationToken token)
    {
        return await _productRepository.IsProductNameUnique(command.Descricao);
    }
}
