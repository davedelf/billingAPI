using System.Text.Json.Serialization;
using ventasAPI.Models;
using ventasAPI.Services;

namespace ventasAPI.DTOS
{
    public class InvoiceDetailDTO
    {
        //Navigation
        public int ProductId { get; set; }
        public int Quantify { get; set; }
        [JsonIgnore]
        public int InvoiceId { get; set; }

    }
}
