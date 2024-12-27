using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ventasAPI.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public Boolean Status { get; set; }


        [ForeignKey("SellerId")]
        public int SellerId { get; set; }
        public Seller Seller { get; set; }



        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        //Navigation
        public ICollection<InvoiceDetail> InvoiceDetails { get; set; }


    }
}
