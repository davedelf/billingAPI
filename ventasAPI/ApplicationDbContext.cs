using Microsoft.EntityFrameworkCore;
using ventasAPI.Models;

namespace ventasAPI
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Admin> Administradores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<Seller>().ToTable("Sellers");

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Seller)
                .WithMany(s => s.Invoices)
                .HasForeignKey(i => i.SellerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Customer)
                .WithMany(c => c.Invoices)
                .HasForeignKey(i => i.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);



            //Data Seeding

            //modelBuilder.Entity<Customer>().HasData(
            //    new Customer { Id = 1, Name = "Ana", LastName = "Leiggener", BornDate = DateTime.Now, Email = "correo@correo.com", ImageURL = "", Gender = Gender.Female },
            //    new Customer { Id = 2, Name = "Pedro", LastName = "Fimba", BornDate = DateTime.Now, Email = "correo@correo.com", ImageURL = "", Gender = Gender.Male },
            //    new Customer { Id = 3, Name = "Maria", LastName = "Cáceres", BornDate = DateTime.Now, Email = "correo@correo.com", ImageURL = "", Gender = Gender.Female },
            //    new Customer { Id = 4, Name = "Luis", LastName = "Vitton", BornDate = DateTime.Now, Email = "correo@correo.com", ImageURL = "", Gender = Gender.Male },
            //    new Customer { Id = 5, Name = "Kamelia", LastName = "Nisrech", BornDate = DateTime.Now, Email = "correo@correo.com", ImageURL = "", Gender = Gender.Female },
            //    new Customer { Id = 6, Name = "Diego", LastName = "Caró", BornDate = DateTime.Now, Email = "correo@correo.com", ImageURL = "", Gender = Gender.Male },
            //    new Customer { Id = 7, Name = "Carina", LastName = "Montagne", BornDate = DateTime.Now, Email = "correo@correo.com", ImageURL = "", Gender = Gender.Female },
            //    new Customer { Id = 8, Name = "Romeo", LastName = "Kombuchop", BornDate = DateTime.Now, Email = "correo@correo.com", ImageURL = "", Gender = Gender.Male },
            //    new Customer { Id = 9, Name = "Silvia", LastName = "Winter", BornDate = DateTime.Now, Email = "correo@correo.com", ImageURL = "", Gender = Gender.Female }

            //);

            // modelBuilder.Entity<Seller>().HasData(
            //    new Seller { Id = 1, Name = "Anahí", LastName = "Olmedo", BornDate = DateTime.Now, Email = "correo@correo.com", ImageURL = "", Gender = Gender.Female },
            //    new Seller { Id = 2, Name = "Pablo", LastName = "Listmer", BornDate = DateTime.Now, Email = "correo@correo.com", ImageURL = "", Gender = Gender.Male },
            //    new Seller { Id = 3, Name = "Macarena", LastName = "Ruthorf", BornDate = DateTime.Now, Email = "correo@correo.com", ImageURL = "", Gender = Gender.Female },
            //    new Seller { Id = 4, Name = "Leandro", LastName = "Leiva", BornDate = DateTime.Now, Email = "correo@correo.com", ImageURL = "", Gender = Gender.Male },
            //    new Seller { Id = 5, Name = "Karina", LastName = "Muller", BornDate = DateTime.Now, Email = "correo@correo.com", ImageURL = "", Gender = Gender.Female },
            //    new Seller { Id = 6, Name = "David", LastName = "Correa", BornDate = DateTime.Now, Email = "correo@correo.com", ImageURL = "", Gender = Gender.Male },
            //    new Seller { Id = 7, Name = "Celeste", LastName = "Gilbert", BornDate = DateTime.Now, Email = "correo@correo.com", ImageURL = "", Gender = Gender.Female },
            //    new Seller { Id = 8, Name = "Roman", LastName = "Bosch", BornDate = DateTime.Now, Email = "correo@correo.com", ImageURL = "", Gender = Gender.Male },
            //    new Seller { Id = 9, Name = "Silvana", LastName = "Becker", BornDate = DateTime.Now, Email = "correo@correo.com", ImageURL = "", Gender = Gender.Female }

            //);

            // modelBuilder.Entity<Product>().HasData(
            //     new Product { Id = 1, Price = 2500, ManufacturingDate = DateTime.Now, ExpiryDate = DateTime.Now, ImageURL = "/", Details = "Aceite", Code = GenerateRandomCode(10),Name= "Aceite" },
            //     new Product { Id = 2, Price = 1000, ManufacturingDate = DateTime.Now, ExpiryDate = DateTime.Now, ImageURL = "/", Details = "Azucar", Code = GenerateRandomCode(10), Name = "Azucar" },
            //     new Product { Id = 3, Price = 6500, ManufacturingDate = DateTime.Now, ExpiryDate = DateTime.Now, ImageURL = "/", Details = "Almendras", Code = GenerateRandomCode(10), Name = "Almendras" },
            //     new Product { Id = 4, Price = 4500, ManufacturingDate = DateTime.Now, ExpiryDate = DateTime.Now, ImageURL = "/", Details = "Café", Code = GenerateRandomCode(10), Name = "Café" },
            //     new Product { Id = 5, Price = 1300, ManufacturingDate = DateTime.Now, ExpiryDate = DateTime.Now, ImageURL = "/", Details = "Tomate", Code = GenerateRandomCode(10), Name = "Tomate" },
            //     new Product { Id = 6, Price = 1900, ManufacturingDate = DateTime.Now, ExpiryDate = DateTime.Now, ImageURL = "/", Details = "Leche", Code = GenerateRandomCode(10), Name = "Leche" },
            //     new Product { Id = 7, Price = 1970, ManufacturingDate = DateTime.Now, ExpiryDate = DateTime.Now, ImageURL = "/", Details = "", Code = GenerateRandomCode(10), Name = "Harina" },
            //     new Product { Id = 8, Price = 2300, ManufacturingDate = DateTime.Now, ExpiryDate = DateTime.Now, ImageURL = "/", Details = "Papa", Code = GenerateRandomCode(10), Name = "Papa" },
            //     new Product { Id = 9, Price = 2300, ManufacturingDate = DateTime.Now, ExpiryDate = DateTime.Now, ImageURL = "/", Details = "Cebolla", Code = GenerateRandomCode(10), Name = "Cebolla" },
            //     new Product { Id = 10, Price = 15000, ManufacturingDate = DateTime.Now, ExpiryDate = DateTime.Now, ImageURL = "/", Details = "Pescado", Code = GenerateRandomCode(10), Name = "Pescado" },
            //     new Product { Id = 11, Price = 900, ManufacturingDate = DateTime.Now, ExpiryDate = DateTime.Now, ImageURL = "/", Details = "Zapallo", Code = GenerateRandomCode(10), Name = "Zapallo" },
            //     new Product { Id = 12, Price = 20500, ManufacturingDate = DateTime.Now, ExpiryDate = DateTime.Now, ImageURL = "/", Details = "Llave Inglesa", Code = GenerateRandomCode(10), Name = "Llave Inglesa" },
            //     new Product { Id = 13, Price = 5700, ManufacturingDate = DateTime.Now, ExpiryDate = DateTime.Now, ImageURL = "/", Details = "Suavizante", Code = GenerateRandomCode(10), Name = "Suavizante" },
            //     new Product { Id = 14, Price = 6500, ManufacturingDate = DateTime.Now, ExpiryDate = DateTime.Now, ImageURL = "/", Details = "Jabón", Code = GenerateRandomCode(10), Name = "Jabón" },
            //     new Product { Id = 15, Price = 4500, ManufacturingDate = DateTime.Now, ExpiryDate = DateTime.Now, ImageURL = "/", Details = "Destornillador", Code = GenerateRandomCode(10), Name = "Destornillador" },
            //     new Product { Id = 16, Price = 40, ManufacturingDate = DateTime.Now, ExpiryDate = DateTime.Now, ImageURL = "/", Details = "Tarugo", Code = GenerateRandomCode(10), Name = "Tarugo" },
            //     new Product { Id = 17, Price = 85000, ManufacturingDate = DateTime.Now, ExpiryDate = DateTime.Now, ImageURL = "/", Details = "Taladro", Code = GenerateRandomCode(10), Name = "" },
            //     new Product { Id = 18, Price = 6980, ManufacturingDate = DateTime.Now, ExpiryDate = DateTime.Now, ImageURL = "/", Details = "Mopa", Code = GenerateRandomCode(10), Name = "Mopa" },
            //     new Product { Id = 19, Price = 2200, ManufacturingDate = DateTime.Now, ExpiryDate = DateTime.Now, ImageURL = "/", Details = "Lavandina", Code = GenerateRandomCode(10), Name = "Lavandina" },
            //     new Product { Id = 20, Price = 1500, ManufacturingDate = DateTime.Now, ExpiryDate = DateTime.Now, ImageURL = "/", Details = "Galletas", Code = GenerateRandomCode(10), Name = "Galletas" }

            //     );

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
