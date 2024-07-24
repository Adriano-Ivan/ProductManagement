using ProductManagement.Domain.Enum;

namespace ProductManagement.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductRequest
{
    public string Descricao { get; set; }
    public string Marca { get; set; }
    public UnidadeMedida UnidadeMedida { get; set; }
    public double ValorDeCompra { get; set; }
    public Guid? ProviderId { get; set; } = null;
}
