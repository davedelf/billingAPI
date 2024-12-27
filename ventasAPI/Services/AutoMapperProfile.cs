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
            CreateMap<InvoiceDTO, Invoice>()
                .ForMember(dest=>dest.Id, opt=> opt.Ignore()); //Ignora el campo Id

            CreateMap<Product,ProductDTO>();
            CreateMap<ProductDTO,Product>();

            CreateMap<InvoiceDetail, InvoiceDetailDTO>();
            CreateMap<InvoiceDetailDTO, InvoiceDetail>();
        }
    }
}
