using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ventasAPI.Models
{
    public class InvoiceDetail
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ProductId")]
        public int ProductId { get; set; }

        //Navigation
        public Product Product { get; set; }

        public int Quantify { get; set; }

        [ForeignKey("InvoiceId")]
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

    }
}
