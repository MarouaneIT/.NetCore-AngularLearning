using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.OrderAggregate;
using System.Security.Cryptography.X509Certificates;
using Address = Core.Entities.Identity.Address;

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
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<CustomerBasketDto,CustomerBasket>();
            CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));
            CreateMap<OrderItem, OrderItemToReturnDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ItemOrdered.ProductItemId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ItemOrdered.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.ItemOrdered.PictureUrl));
        }
    }
}
