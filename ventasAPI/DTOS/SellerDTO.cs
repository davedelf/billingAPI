using System.ComponentModel.DataAnnotations;
using ventasAPI.Models;
using ventasAPI.Services;

namespace ventasAPI.DTOS
{
    public class SellerDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(80)]
        public string Email { get; set; }
        [Required]
        public long Document { get; set; }
        [Required]
        public Gender Gender { get; set; }

    }
}
