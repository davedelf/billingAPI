using AutoMapper;
using System.Text.Json.Serialization;
using ventasAPI.Models;
using ventasAPI.Services;

namespace ventasAPI.DTOS
{
    public class InvoiceDTO
    {
        //public string Code { get; set; }
        public DateTime Date { get; set; }
        [JsonIgnore]
       public Boolean Status { get; set; }
        [JsonIgnore]
        public Guid Code { get; set; }
        public int SellerId { get; set; }
        public int CustomerId { get; set; }

        public ICollection<InvoiceDetailDTO> InvoiceDetailsDto { get; set; }

    }
}
