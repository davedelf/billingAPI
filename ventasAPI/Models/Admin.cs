using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ventasAPI.Models
{
    public class Admin:User
    {
        [Key]
        public int Id { get; set; }
        [JsonIgnore]
        public Usuario Usuario { get; set; }
    }
}
