using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProductManagement.Application.Contracts;
using ProductManagement.Application.Exceptions;
using ProductManagement.Application.Features.Providers.Commands.CreateProvider;
using ProductManagement.Domain.DTO;

namespace ProductManagement.Application.Features.Providers.Handlers;

public sealed class CreateProviderCommandHandler : IRequestHandler<CreateProviderCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly IProviderRepository _providerRepository;
    private readonly HttpClient _httpClient;

    public CreateProviderCommandHandler(IMapper mapper, 
        IProviderRepository providerRepository,
        IHttpClientFactory factory)
    {
        _mapper = mapper;
        _providerRepository = providerRepository;
        _httpClient = factory.CreateClient("ViaCepClient");
    }

    public async Task<Guid> Handle(CreateProviderCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateProviderCommandValidator(this._providerRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
        {
            throw new BadRequestException("Invalid Provider", validationResult);
        }

        var enderecoString = "";
        try
        {
            var serializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var enderecoInfo = await _httpClient.GetAsync($"{request.Cep}/json");
            var responseContent = await enderecoInfo.Content.ReadAsStringAsync();
            var addressResponse = JsonConvert.DeserializeObject<EnderecoViaCepDto>(responseContent, serializerSettings);
            enderecoString = addressResponse.ToString();
        }
        catch
        {
            throw new BadRequestException("Failed when trying to find address information");
        }

        var provider = _mapper.Map<Domain.Provider>(request);
        provider.Endereco = enderecoString;

        var createdProduct = await _providerRepository.CreateAsync(provider);

        return createdProduct.Id;
    }
}
