using System.ComponentModel.DataAnnotations;

namespace ventasAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Details { get; set; }
        [Required]
        [MaxLength(10)]
        public string Code { get; set; }
        [Required]
        public string ImageURL { get; set; }

        //Navigation
        public ICollection<InvoiceDetail> InvoiceDetails { get; set; }  //Navigation
    }
}
