namespace ProductManagement.Application.Features.Providers.Commands.UpdateProvider;

public class UpdateProviderRequest
{
    public string CNPJ { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Cep { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
}
