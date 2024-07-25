using AutoMapper;
using MediatR;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using ProductManagement.Application.Contracts;
using ProductManagement.Application.Exceptions;
using ProductManagement.Application.Features.Products.Commands.UpdateProduct;
using ProductManagement.Application.Features.Providers.Commands.UpdateProvider;
using ProductManagement.Domain.DTO;
using System.Net.Http;

namespace ProductManagement.Application.Features.Providers.Handlers;

public sealed class UpdateProviderCommandHandler : IRequestHandler<UpdateProviderCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly IProviderRepository _providerRepository;
    private readonly HttpClient _httpClient;

    public UpdateProviderCommandHandler(IMapper mapper, IProviderRepository providerRepository, IHttpClientFactory factory)
    {
        _mapper = mapper;
        _providerRepository = providerRepository;
        _httpClient = factory.CreateClient("ViaCepClient");
    }

    public async Task<bool> Handle(UpdateProviderCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateProviderCommandValidator(_providerRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
        {
            throw new BadRequestException("Invalid Product", validationResult);
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

        await _providerRepository.UpdateAsync(provider);

        return true;
    }
}
