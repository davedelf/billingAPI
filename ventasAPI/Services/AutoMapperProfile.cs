using AutoMapper;
using ventasAPI.DTOS;
using ventasAPI.Models;
namespace ventasAPI.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<CustomerDTO, Customer>();
            CreateMap<Customer, CustomerDTO>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Usuario.Username))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Usuario.Password));

            CreateMap<SellerDTO, Seller>();
            CreateMap<Seller, SellerDTO>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Usuario.Username))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Usuario.Password));

            CreateMap<Invoice, InvoiceDTO>();
            CreateMap<InvoiceDTO, Invoice>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); //Ignora el campo Id

            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();

            CreateMap<InvoiceDetail, InvoiceDetailDTO>();
            CreateMap<InvoiceDetailDTO, InvoiceDetail>();

            CreateMap<Usuario, UsuarioDTO>();
            CreateMap<UsuarioDTO, Usuario>();
        }
    }
}
