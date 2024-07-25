using MediatR;

namespace ProductManagement.Application.Features.Providers.Commands.UpdateProvider;

public class UpdateProviderCommand : IRequest<bool>
{
    public UpdateProviderCommand(Guid id, string cNPJ, string nome, string cep, string telefone)
    {
        Id = id;
        CNPJ = cNPJ;
        Nome = nome;
        Cep = cep;
        Telefone = telefone;
    }

    public Guid Id { get; set; }
    public string CNPJ { get; set; }
    public string Nome { get; set; }
    public string Cep { get; set; }
    public string Telefone { get; set; }
}
