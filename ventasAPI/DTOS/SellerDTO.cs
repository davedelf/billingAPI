using ventasAPI.Models;
using ventasAPI.Services;

namespace ventasAPI.DTOS
{
    public class SellerDTO
    {

        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public Gender Gender { get; set; }
    }
}
