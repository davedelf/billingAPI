using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using ventasAPI.DTOS;
using ventasAPI.Models;

namespace ventasAPI.Controllers
{
    [ApiController]
    [Route("api/invoices")]
    public class InvoiceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public InvoiceController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("GuidCode")]
        public async Task<ActionResult<InvoiceDTO>> GetInvoiceByCode(Guid code)
        {
            var invoice = await _context.Invoices.FirstOrDefaultAsync(i => i.Code == code);
            if (invoice == null)
            {
                return NotFound($"GUID {code} not found");
            }
            else
            {
                return Ok(_mapper.Map<InvoiceDTO>(invoice));
            }
        }

        [HttpGet("sellerId")]
        public async Task<ActionResult<List<InvoiceDTO>>> GetAllInvoicesBySeller(int sellerId)
        {

            var invoices = await _context.Invoices
                .Where(i => i.SellerId == sellerId)
                .Select(i => new InvoiceDTO
                {
                    Code = i.Code,
                    Date = i.Date,
                    SellerId = i.SellerId,
                    CustomerId = i.CustomerId
                })
                .ToListAsync();

            if (invoices.Count == 0)
            {
                return NotFound("Invoices not found");
            }
            else
            {
                return Ok(invoices);
            }
        }

        [HttpGet("customerId")]
        public async Task<ActionResult<List<InvoiceDTO>>> GetAllInvoicesByCustomerId(int customerId)
        {

            var invoices = await _context.Invoices
                .Where(i => i.CustomerId == customerId)
                .Select(i => new InvoiceDTO
                {
                    Code = i.Code,
                    Date = i.Date,
                    SellerId = i.SellerId,
                    CustomerId = i.CustomerId
                })
                .ToListAsync();

            if (invoices.Count == 0)
            {
                return NotFound("Invoices not found");
            }
            else
            {
                return Ok(invoices);
            }
        }




        [HttpPost("postInvoice")]
        public async Task<ActionResult> PostInvoice(InvoiceDTO invoiceDto)
        {


            var verifyCustomer = await _context.Customers.AnyAsync(c => c.Id == invoiceDto.CustomerId);
            var verifySeller = await _context.Sellers.AnyAsync(s => s.Id == invoiceDto.SellerId);


            if (!verifyCustomer || !verifySeller)
            {
                return BadRequest($"No se puede grabar la factura");
            }
            var newInvoice = _mapper.Map<Invoice>(invoiceDto);
            newInvoice.Code = Guid.NewGuid();
            _context.Add(newInvoice);
            _context.Entry(newInvoice).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return Ok($"Factura registrada");
        }

        [HttpDelete("deleteId")]
        public async Task<ActionResult> DeleteInvoiceById(int id)
        {
            var invoice = await _context.Invoices.AsTracking().FirstOrDefaultAsync(i => i.Id == id);
            if (invoice == null)
            {
                return BadRequest("Invoice not found");
            }

            invoice.Status = false;
            await _context.SaveChangesAsync();
            return Ok("Factura dada de baja ");


        }

        [HttpDelete("deleteGUID")]
        public async Task<ActionResult> DeleteInvoiceByCode(Guid code)
        {
            var invoice = await _context.Invoices.AsTracking().FirstOrDefaultAsync(i => i.Code == code);
            if (invoice == null)
            {
                return BadRequest("Invoice not found");
            }
            invoice.Status = false;
            await _context.SaveChangesAsync();
            return Ok("Factura dada de baja");
        }

        [HttpGet("getInvoiceDetailsByCode")]
        public async Task<ActionResult<IEnumerable<InvoiceDetailDTO>>> GetInvoiceDetailsByInvoice(Guid invoiceCode)
        {
            var invoice = await _context.Invoices.FirstOrDefaultAsync(i => i.Code == invoiceCode);
            if (invoice == null)
            {
                return BadRequest("Invoice not found");
            }

            var invoiceDetails = await _context.InvoiceDetails.Where(i => i.InvoiceId == invoice.Id).ToListAsync();
            var invoiceDetailsDTO = new List<InvoiceDetailDTO>();
            foreach (InvoiceDetail detail in invoiceDetails)
            {
                invoiceDetailsDTO.Add(_mapper.Map<InvoiceDetailDTO>(detail));
            }
            return Ok(invoiceDetailsDTO);



        }

        [HttpGet("getInvoiceDetailsById")]
        public async Task<ActionResult<IEnumerable<InvoiceDetailDTO>>> GetInvoiceDetailsByInvoice(int invoiceId)
        {
            var invoice = await _context.Invoices.FirstOrDefaultAsync(i => i.Id == invoiceId);
            if (invoice == null)
            {
                return BadRequest("Invoice not found");
            }

            var invoiceDetails = await _context.InvoiceDetails.Where(i => i.InvoiceId == invoice.Id).ToListAsync();
            var invoiceDetailsDTO = new List<InvoiceDetailDTO>();
            foreach (InvoiceDetail detail in invoiceDetails)
            {
                invoiceDetailsDTO.Add(_mapper.Map<InvoiceDetailDTO>(detail));
            }
            return Ok(invoiceDetailsDTO);



        }

        [HttpPost("PostInvoiceWithDetails")]
        public async Task<ActionResult> CreateInvoice(InvoiceDTO invoiceDto)
        {
            var invoice = _mapper.Map<Invoice>(invoiceDto);
            invoice.Code = Guid.NewGuid();
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            var invoiceDetails = invoiceDto.InvoiceDetailsDto.Select(detailDto => new InvoiceDetail
            {
                ProductId = detailDto.ProductId,
                Quantify = detailDto.Quantify,
                InvoiceId = invoice.Id
            }).ToList();

            _context.InvoiceDetails.AddRange(invoiceDetails);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Invoice and its invoiceDetails created successfully" });
        }

        [HttpGet("GetInvoiceAndDetailsById")]
        public async Task<ActionResult<InvoiceDTO>> GetInvoiceAndDetails(int invoiceId)
        {
            var invoice = await _context.Invoices.FirstOrDefaultAsync(i => i.Id == invoiceId);
            if (invoice == null)
            {
                return NotFound($"Invoice {invoiceId} not found");

            }
            var invoiceDetails = await _context.InvoiceDetails
                .Where(det => det.InvoiceId == invoiceId)
                .ToListAsync();

            var invoiceDto = _mapper.Map<InvoiceDTO>(invoice);
            var invoiceDetailsDto = new List<InvoiceDetailDTO>();

            foreach(var invoiceDet in invoiceDetails)
            {
                invoiceDetailsDto.Add(_mapper.Map<InvoiceDetailDTO>(invoiceDet));
            }

            invoiceDto.InvoiceDetailsDto = invoiceDetailsDto;

            return Ok(invoiceDto);
        }

        [HttpGet("GetInvoiceAndDetailsByCode")]
        public async Task<ActionResult<InvoiceDTO>> GetInvoiceAndDetails(Guid invoiceCode)
        {
            var invoice = await _context.Invoices.FirstOrDefaultAsync(i => i.Code == invoiceCode);
            if (invoice == null)
            {
                return NotFound($"Invoice {invoiceCode} not found");

            }
            var invoiceDetails = await _context.InvoiceDetails
                .Where(det => det.InvoiceId == invoice.Id)
                .ToListAsync();

            var invoiceDto = _mapper.Map<InvoiceDTO>(invoice);
            var invoiceDetailsDto = new List<InvoiceDetailDTO>();

            foreach (var invoiceDet in invoiceDetails)
            {
                invoiceDetailsDto.Add(_mapper.Map<InvoiceDetailDTO>(invoiceDet));
            }

            invoiceDto.InvoiceDetailsDto = invoiceDetailsDto;

            return Ok(invoiceDto);
        }


        [HttpGet("GetInvoicesAndDetailsByCustomerDocument")]
        public async Task<ActionResult<IEnumerable<InvoiceDTO>>> GetInvoicesCustomer(long customerDocument)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Document == customerDocument);
            if (customer == null)
            {
                return NotFound("Invoices not found");
            }

            var invoices = await _context.Invoices
                .Where(i => i.Customer.Document == customerDocument)
                .Include(i => i.InvoiceDetails)
                .ToListAsync();

            var invoicesDto=new List<InvoiceDTO>();

            foreach(Invoice invoice in invoices)
            {
                var invoiceDto=_mapper.Map<InvoiceDTO>(invoice);
                var invoiceDetailsDto = new List<InvoiceDetailDTO>();
                foreach(InvoiceDetail detail in invoice.InvoiceDetails)
                {
                    invoiceDetailsDto
                        .Add(_mapper.Map<InvoiceDetailDTO>(detail));
                        
                }
                invoiceDto.InvoiceDetailsDto= invoiceDetailsDto;
                
                invoicesDto.Add(invoiceDto);
            }

            return Ok(invoicesDto);

            
        }


        [HttpGet("GetInvoicesAndDetailsBySellerDocument")]
        public async Task<ActionResult<IEnumerable<InvoiceDTO>>> GetInvoicesSeller(long sellerDocument)
        {
            var seller = await _context.Sellers.FirstOrDefaultAsync(s => s.Document == sellerDocument);
            if (seller == null)
            {
                return NotFound("Invoices not found");
            }

            var invoices = await _context.Invoices
                .Where(i => i.Seller.Document == sellerDocument)
                .Include(i => i.InvoiceDetails)
                .ToListAsync();

            var invoicesDto = new List<InvoiceDTO>();

            foreach (Invoice invoice in invoices)
            {
                var invoiceDto = _mapper.Map<InvoiceDTO>(invoice);
                var invoiceDetailsDto = new List<InvoiceDetailDTO>();
                foreach (InvoiceDetail detail in invoice.InvoiceDetails)
                {
                    invoiceDetailsDto
                        .Add(_mapper.Map<InvoiceDetailDTO>(detail));

                }
                invoiceDto.InvoiceDetailsDto = invoiceDetailsDto;

                invoicesDto.Add(invoiceDto);
            }

            return Ok(invoicesDto);


        }
    }

    
}
