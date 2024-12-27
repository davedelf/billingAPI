using System.ComponentModel.DataAnnotations;

namespace ventasAPI.Models
{
    public class Customer : User
    {

        //Navigation
        private ICollection<Invoice> Invoices { get; set; }


    }
}
