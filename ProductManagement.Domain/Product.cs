using ProductManagement.Domain.Common;
using ProductManagement.Domain.Enum;

namespace ProductManagement.Domain;

public class Product : BaseEntity
{
    public string Descricao { get; set; }
    public string Marca { get; set; }
    public UnidadeMedida UnidadeMedida { get; set; }
    public double ValorDeCompra { get; set; }
}
