using ProductManagement.Domain.Common;

namespace ProductManagement.Domain;

public class Provider : BaseEntity
{
    public string CNPJ { get; set; }
    public string Nome { get; set; }
    public string Endereco { get; set; }
    public string Telefone { get; set; }
    public ICollection<Product> Products { get; set; }
}
