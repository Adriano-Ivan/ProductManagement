﻿using MediatR;
using ProductManagement.Domain.Enum;

namespace ProductManagement.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest<bool>
{
    public UpdateProductCommand(Guid id, string descricao, string marca, UnidadeMedida unidadeMedida, double valorDeCompra, Guid? providerId)
    {
        Id = id;
        Descricao = descricao;
        Marca = marca;
        UnidadeMedida = unidadeMedida;
        ValorDeCompra = valorDeCompra;
        ProviderId = providerId;
    }

    public Guid Id { get; set; }
    public string Descricao { get; set; }
    public string Marca { get; set; }
    public UnidadeMedida UnidadeMedida { get; set; }
    public double ValorDeCompra { get; set; }
    public Guid? ProviderId { get; set; } = null;
}
