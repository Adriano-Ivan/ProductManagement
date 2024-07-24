using ProductManagement.Domain.Enum;

namespace ProductManagement.Application.Features.Products.Queries.GetProductDetails;

public record ProductDetailsDto
{
    public Guid Id { get; set; }
    public string Descricao { get; set; }
    public string Marca { get; set; }
    public UnidadeMedida UnidadeMedida { get; set; }
    public double ValorDeCompra { get; set; }
}
