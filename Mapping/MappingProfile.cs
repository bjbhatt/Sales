using AutoMapper;
using Sales.Models;
using Sales.Controllers.Resources;

namespace Sales.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile ()
        {
            CreateMap<Customer, CustomerResource>();
            CreateMap<CustomerResource, Customer>()
                .ForMember(cr => cr.Id, opt => opt.Ignore())
                .ForMember(cr => cr.Orders, opt => opt.Ignore());
            CreateMap<Product, ProductResource>();
            CreateMap<ProductResource, Product>()
                .ForMember(pr => pr.Id, opt => opt.Ignore())
                .ForMember(pr => pr.OrderDetails, opt => opt.Ignore());
            CreateMap<Order, OrderResource>();
            CreateMap<OrderResource, Order>()
                .ForMember(or => or.Id, opt => opt.Ignore())
                .ForMember(or => or.OrderDetails, opt => opt.Ignore());
            CreateMap<OrderDetail, OrderDetailResource>();
            CreateMap<OrderDetailResource, OrderDetail>()
                .ForMember(odr => odr.Id, opt => opt.Ignore())
                .ForMember(odr => odr.Order, opt => opt.Ignore())
                .ForMember(odr => odr.Product, opt => opt.Ignore());
        }
    }
}