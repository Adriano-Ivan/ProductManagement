using AutoMapper;
using ProductManagement.Application.Features.Products.Commands.CreateProduct;
using ProductManagement.Application.Features.Products.Commands.UpdateProduct;
using ProductManagement.Application.Features.Products.Queries.GetProductDetails;
using ProductManagement.Domain;

namespace ProductManagement.Application.MappingProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<CreateProductCommand, Product>().ReverseMap();
        CreateMap<ProductDetailsDto, Product>().ReverseMap();
        CreateMap<UpdateProductCommand, Product>().ReverseMap();
    }
}
