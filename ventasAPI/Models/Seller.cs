using System.ComponentModel.DataAnnotations;

namespace ventasAPI.Models
{
    public class Seller:User
    {
        //Navigation
        public ICollection<Invoice> Invoices { get; set; }

    }
}
