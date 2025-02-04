using System.ComponentModel.DataAnnotations;
using ventasAPI.Models;

namespace ventasAPI.DTOS
{
    public class AdminDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BornDate { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(80)]
        public string Email { get; set; }
        [Required]
        public long Document { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public string Telephone { get; set; }

        //User
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
