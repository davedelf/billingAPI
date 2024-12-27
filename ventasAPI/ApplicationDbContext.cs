using Microsoft.EntityFrameworkCore;
using ventasAPI.Models;

namespace ventasAPI
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions options):base(options) 
        {
            
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public DbSet<Product> Products { get; set; }
    }


}
