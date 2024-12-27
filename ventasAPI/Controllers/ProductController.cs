using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ventasAPI.DTOS;
using ventasAPI.Models;

namespace ventasAPI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public ProductController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }




        [HttpGet]
        public async Task<ActionResult<ProductDTO>> GetProductByCode(string code)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Code == code);
            if (product == null)
            {
                return NotFound($"No existe producto con código: {code}");
            }
            else
            {
                return _mapper.Map<ProductDTO>(product);
            }

        }


        [HttpGet("GetAll")]
        public async Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            return await _context.Products.ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();

        }

        [HttpPost]
        public async Task<ActionResult> PostProduct(ProductDTO productDto)
        {
            string randomCode = GenerateRandomCode(10);
            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Code == randomCode);
            if (existingProduct != null)
            {

                return BadRequest($"Ya existe producto con código: {randomCode}");
            }
            var newProduct = _mapper.Map<Product>(productDto);



            while (await _context.Products.AnyAsync(p => p.Code == randomCode))
            {

                randomCode = GenerateRandomCode(10);


            }
            newProduct.Code = randomCode;
            _context.Add(newProduct);
            _context.Entry(newProduct).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return Ok($"Producto con código {randomCode} registrado");

        }

        //[HttpDelete]
        //public async Task<ActionResult> DeleteProduct(string code)
        //{
        //    var product = await _context.Products.AsTracking().FirstOrDefaultAsync(p => p.Code == code);
        //    if (product == null)
        //    {
        //        return NotFound($"Producto con código {code} no encontrado");
        //    }
        //    _context.Products.Remove(product);
        //    await _context.SaveChangesAsync();
        //    return Ok("Producto eliminado");
        //}

        [HttpPut]
        public async Task<ActionResult> UpdateProduct(ProductDTO productDto, string code)
        {
            var product = await _context.Products.AsTracking()
                .FirstOrDefaultAsync(p => p.Code == code);

            if (product == null)
            {
                return NotFound($"Producto con código {code} no encontrado");
            }
            product = _mapper.Map(productDto, product);
            product.Code = code;
            await _context.SaveChangesAsync();
            return Ok("Producto actualizado");



        }


        private string GenerateRandomCode(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Range(0, length)
                .Select(_ => chars[random.Next(chars.Length)])
                .ToArray());
        }

    }


}
