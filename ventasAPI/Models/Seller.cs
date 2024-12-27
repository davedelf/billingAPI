using System.ComponentModel.DataAnnotations;

namespace ventasAPI.Models
{
    public class Seller:User
    {

        //Navigation
        private ICollection<Invoice> Invoices { get; set; }




        
    }
}
