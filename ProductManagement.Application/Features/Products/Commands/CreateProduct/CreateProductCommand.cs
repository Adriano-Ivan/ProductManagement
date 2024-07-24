using MediatR;
using ProductManagement.Domain.Enum;

namespace ProductManagement.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommand : IRequest<Guid>
{
    public CreateProductCommand(string descricao, string marca, UnidadeMedida unidadeMedida, double valorDeCompra)
    {
        Descricao = descricao;
        Marca = marca;
        UnidadeMedida = unidadeMedida;
        ValorDeCompra = valorDeCompra;
    }

    public string Descricao { get; set; }
    public string Marca { get; set; }
    public UnidadeMedida UnidadeMedida { get; set; }
    public double ValorDeCompra { get; set; }
}
