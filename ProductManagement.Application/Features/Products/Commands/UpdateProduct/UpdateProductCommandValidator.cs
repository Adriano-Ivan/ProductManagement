using FluentValidation;
using ProductManagement.Application.Contracts;

namespace ProductManagement.Application.Features.Products.Commands.UpdateProduct;

public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>    
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandValidator(IProductRepository productRepository)
    {
        RuleFor(p => p.Id)
            .NotNull()
            .MustAsync(ProductMustExist);

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

    private async Task<bool> ProductNameUnique(UpdateProductCommand command, CancellationToken token)
    {
        return await _productRepository.IsProductNameUniqueBesidesThisProduct(command.Id, command.Descricao);
    }

    private async Task<bool> ProductMustExist(Guid id, CancellationToken token)
    {
        return await _productRepository.AnyAsync(id);
    }
}
