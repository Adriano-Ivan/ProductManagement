using MediatR;
using ProductManagement.Domain.Extensions;

namespace ProductManagement.Application.Features.Providers.Commands.CreateProvider;

public class CreateProviderCommand : IRequest<Guid>
{
    public CreateProviderCommand(string cNPJ, string nome, string cep, string telefone)
    {
        CNPJ = cNPJ.FormatCnpjToDatabase();
        Nome = nome;
        Cep = cep;
        Telefone = telefone;
    }

    public string CNPJ { get; set; }
    public string Nome { get; set; }
    public string Cep { get; set; }
    public string Telefone { get; set; }
}
