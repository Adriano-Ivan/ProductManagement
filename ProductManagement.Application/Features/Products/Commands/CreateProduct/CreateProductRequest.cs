using ProductManagement.Domain.Enum;

namespace ProductManagement.Application.Features.Products.Commands.CreateProduct;

public class CreateProductRequest
{
    public string Descricao { get; set; }
    public string Marca { get; set; }
    public UnidadeMedida UnidadeMedida { get; set; }
    public double ValorDeCompra { get; set; }
}
