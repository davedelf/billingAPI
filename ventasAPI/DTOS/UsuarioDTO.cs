using ventasAPI.Models;

namespace ventasAPI.DTOS
{
    public class UsuarioDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Rol Rol { get; set; }
    }
}
