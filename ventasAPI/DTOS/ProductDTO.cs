using ventasAPI.Services;

namespace ventasAPI.DTOS
{
    public class ProductDTO
    {
        private AutoMapperProfile mapper;
        public string Name { get; set; }
        public double Price { get; set; }
        public string Details { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
