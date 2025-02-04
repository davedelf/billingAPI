using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ventasAPI.Models
{
    public class Customer : User
    {

        //Navigation
        public ICollection<Invoice> Invoices { get; set; }
        [ForeignKey("UsuarioId")]
        public int UsuarioId { get; set; }
        [JsonIgnore] // Evita el bucle en la serialización
        public Usuario Usuario { get; set; }


    }
}
