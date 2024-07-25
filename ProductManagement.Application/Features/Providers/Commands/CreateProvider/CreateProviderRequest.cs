namespace ProductManagement.Application.Features.Providers.Commands.CreateProvider;

public class CreateProviderRequest
{
    public string CNPJ { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Cep { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
}
