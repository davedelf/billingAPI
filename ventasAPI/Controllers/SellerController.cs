using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using ventasAPI.DTOS;
using ventasAPI.Models;

namespace ventasAPI.Controllers
{
    [ApiController]
    [Route("api/sellers")]
    public class SellerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public SellerController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }




        [HttpGet("GetByDocument")]
        public async Task<ActionResult<SellerDTO>> GetSellerByDocument(long document)
        {
            var seller = await _context.Sellers.FirstOrDefaultAsync(s => s.Document == document);
            if (seller == null)
            {
                return NotFound($"Doesn't exists seller with document {document}");
               
            }
            return _mapper.Map<SellerDTO>(seller);
        }





        [HttpGet("GetAll")]
        public async Task<IEnumerable<SellerDTO>> GetAllSellers()
        {
            return await _context.Sellers.ProjectTo<SellerDTO>(_mapper.ConfigurationProvider).ToListAsync();

        }

        [HttpPost]
        public async Task<ActionResult> PostSeller(SellerDTO sellerDto)
        {
            var existingSeller = await _context.Sellers.AnyAsync(s => s.Document == sellerDto.Document);
            if (existingSeller)
            {

                return BadRequest($"Already exists a seller with document {sellerDto.Document}");
            }
            var newUsuario = new Usuario
            {
                Username = sellerDto.Username,
                Password = sellerDto.Password,
                Rol = Rol.Seller          
            };
            var newSeller = new Seller
            {
                Usuario = newUsuario,
                Name =sellerDto.Name,
                LastName = sellerDto.LastName,
                Document = sellerDto.Document,
                Email = sellerDto.Email,
                Telephone =sellerDto.Telephone,
                BornDate = sellerDto.BornDate,
                Gender = sellerDto.Gender
            };
            _context.Add(newSeller);
            _context.Entry(newSeller).State = EntityState.Added;
            await _context.SaveChangesAsync();
            //return Ok($"Seller with document {newSeller.Document} has been registered");
            return Ok(_mapper.Map<SellerDTO>(newSeller));

        }

        [HttpDelete("DeleteByDocument")]
        public async Task<ActionResult> DeleteSeller(long document)
        {
            var seller = await _context.Sellers.AsTracking().FirstOrDefaultAsync(s => s.Document == document);
            if (seller == null)
            {
                return NotFound($"Documento no encontrado: {document}");
            }
            _context.Sellers.Remove(seller);
            await _context.SaveChangesAsync();
            return Ok("Seller has been deleted");
        }

        [HttpPut("UpdateByDocument")]
        public async Task<ActionResult> UpdateSeller(SellerDTO sellerDto, long document)
        {
            var seller = await _context.Sellers.AsTracking()
                .FirstOrDefaultAsync(s=> s.Document == document);

            if (seller == null)
            {
                return NotFound($"Seller with document {document} not found");
            }
            seller = _mapper.Map(sellerDto, seller);
            seller.Document = document;
            await _context.SaveChangesAsync();
            return Ok("Seller has been updated");



        }
    }
}
