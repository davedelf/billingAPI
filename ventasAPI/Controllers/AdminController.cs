using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ventasAPI.DTOS;
using ventasAPI.Models;

namespace ventasAPI.Controllers
{
    [ApiController]
    [Route("api/administradores")]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AdminController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<AdminDTO>> GetAdmin(int id)
        {
            var admin = await _context.Administradores.FirstOrDefaultAsync(a => a.Id == id);
            if (admin == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AdminDTO>(admin));
        }

        [HttpPost("Post")]
        public async Task<IActionResult> PostAdmin(AdminDTO adminDto)
        {
            var existingAdmin = await _context.Administradores.AnyAsync(c => c.Document == adminDto.Document);
            if (existingAdmin)
            {

                return BadRequest($"Ya existe un administrador con ese documento: {adminDto.Document}");
            }
            var newUsuario = new Usuario
            {
                Username = adminDto.Username,
                Password = adminDto.Password,
                Rol = Rol.Admin

            };
            var newAdmin = new Admin
            {
                Usuario = newUsuario,
                Name = adminDto.Name,
                LastName = adminDto.LastName,
                Document = adminDto.Document,
                Email = adminDto.Email,
                Telephone = adminDto.Telephone,
                BornDate = adminDto.BornDate,
                Gender = adminDto.Gender,

            };

            _context.Add(newAdmin);
            _context.Entry(newAdmin).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<AdminDTO>(newAdmin));

        }
    }
}
