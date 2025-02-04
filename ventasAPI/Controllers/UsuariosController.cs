using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ventasAPI.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using ventasAPI.DTOS;

namespace ventasAPI.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;
        public UsuariosController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("getUsuario{id:int}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario= await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
            if(usuario == null)
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
    }
}
