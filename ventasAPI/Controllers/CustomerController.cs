using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ventasAPI.DTOS;
using ventasAPI.Models;

namespace ventasAPI.Controllers
{


    [ApiController]
    [Route("api/customers")]


    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public CustomerController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        [HttpGet]
        public async Task<ActionResult<CustomerDTO>> GetByDocument(long document)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Document == document);
            if (customer == null)
            {
                return NotFound($"No existe cliente con el documento: {document}");
            }
            else
            {
                return _mapper.Map<CustomerDTO>(customer);
            }

        }


        [HttpGet("getAll")]
        public async Task<IEnumerable<CustomerDTO>> getAll()
        {
            return await _context.Customers.ProjectTo<CustomerDTO>(_mapper.ConfigurationProvider).ToListAsync();

        }

        [HttpPost]
        public async Task<ActionResult> Post(CustomerDTO customerDto)
        {
            var existingCustomer = await _context.Customers.AnyAsync(c => c.Document == customerDto.Document);
            if (existingCustomer)
            {

                return BadRequest($"Ya existe un cliente con ese documento: {customerDto.Document}");
            }
            var newCustomer = _mapper.Map<Customer>(customerDto);
            _context.Add(newCustomer);
            _context.Entry(newCustomer).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return Ok();

        }

        [HttpDelete]
        public async Task<ActionResult> deleteCustomer(long document)
        {
            var customer=_context.Customers.FirstOrDefaultAsync(c=>c.Document == document);
            if (customer == null)
            {
                return NotFound($"Documento no encontrado: {document}");
            }
            _context.Remove(customer);
            await _context.SaveChangesAsync();
            return Ok("Cliente eliminado");
        }

        [HttpPut]
        public async Task<ActionResult<>> updateCustomer(CustomerDTO customerDto,long document)
        {
            var customer=await _context.Customers.AsTracking()
                .FirstOrDefaultAsync(c=>c.Document==document);

            if(customer == null)
            {
                return NotFound($"Cliente con documento {document} no encontrado");
            }
            customer=_mapper.Map(customerDto, customer);



        }
    }
}
