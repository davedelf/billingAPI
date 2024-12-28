using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ventasAPI.Models
{
    public class Invoice
    {
        public Invoice()
        {
            Status = true;
            Code = Guid.NewGuid();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }
        public Boolean Status { get; set; }

        public Guid Code { get; set; } = Guid.NewGuid();



        public int SellerId { get; set; }
        public Seller Seller { get; set; }



        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        //Navigation
        public ICollection<InvoiceDetail> InvoiceDetails { get; set; }


    }
}
