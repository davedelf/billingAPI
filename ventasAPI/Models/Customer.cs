using System.ComponentModel.DataAnnotations;

namespace ventasAPI.Models
{
    public class Customer : User
    {

        //Navigation
        public ICollection<Invoice> Invoices { get; set; }


    }
}
