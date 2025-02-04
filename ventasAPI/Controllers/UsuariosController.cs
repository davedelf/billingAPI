using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ventasAPI.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using ventasAPI.DTOS;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization.Infrastructure;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ventasAPI.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;
        private IConfiguration _config; //Con esta dependencia accedemos al appsettings.json


        public UsuariosController(ApplicationDbContext context, IMapper mapper, IConfiguration config)
        {
            _context = context;
            _mapper = mapper;
            _config = config;
        }

        [HttpGet("getUsuario{id:int}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<UsuarioDTO>(usuario));
        }


        [HttpGet("GetAll")]
        public async Task<IEnumerable<UsuarioDTO>> GetAllUsuarios()
        {


            return await _context.Usuarios.ProjectTo<UsuarioDTO>(_mapper.ConfigurationProvider).ToListAsync();


        }

        [HttpPost]
        [Route("login")]
        public async Task<dynamic> IniciarSesion([FromBody] Object optData)
        {
            var data = JsonConvert.DeserializeObject<dynamic>(optData.ToString());
            string userName = data.username.ToString();
            string password = data.password.ToString();

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Username == userName && u.Password == password);
            if (usuario == null)
            {
                return NotFound();
            }



            //Con esto "mapeamos" o convertimos la configuracion de jwt en appsettings.json a un modelo
            var jwt = _config.GetSection("Jwt").Get<Jwt>();


            //Especificamos todo lo que va a encapsular el token
            var claims = new[]
            {
                //Propiedades de configuración básicas
                new Claim(JwtRegisteredClaimNames.Sub,jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),

                //Opcionales o que queremos encapsular en el token
                new Claim("id",usuario.Id.ToString()),
                new Claim("usuario",usuario.Username),

            };

            //Generamos nuestra key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));

            //Generamos inicio de sesión
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            //Creamos el token
            var token = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(5)
            );

            return new
            {
                success = true,
                message = "Exito",
                result = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}
