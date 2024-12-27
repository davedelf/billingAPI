using AutoMapper;
using ventasAPI.DTOS;
using ventasAPI.Models;
namespace ventasAPI.Services
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<CustomerDTO, Customer>();
            CreateMap<Customer, CustomerDTO>();

            CreateMap<Seller, SellerDTO>();
            CreateMap<SellerDTO, Seller>();

            CreateMap<Invoice, InvoiceDTO>();
            CreateMap<InvoiceDTO, InvoiceDTO>();

            CreateMap<Product,ProductDTO>();
            CreateMap<ProductDTO,Product>();
        }
    }
}
