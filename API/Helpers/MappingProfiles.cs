using API.Dtos;
using AutoMapper;
using Core.Entities;
using System.Security.Cryptography.X509Certificates;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(x => x.ProductBrand, op => op.MapFrom(x => x.ProductBrand.Name))
                .ForMember(x => x.ProductType, op => op.MapFrom(x => x.ProductType.Name))
                .ForMember(x=>x.PictureUrl,op=>op.MapFrom<ProductUrlResolver>());
                
        }
    }
}
