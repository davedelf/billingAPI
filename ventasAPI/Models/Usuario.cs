using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ventasAPI.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Rol Rol { get; set; }

        public ICollection<Customer> Customers { get; set; }

        public ICollection<Seller> Sellers { get; set; }
    }
}
