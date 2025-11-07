using Backend_ZS.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Backend_ZS.API.Data
{
    public class ZsDbContext : DbContext
    {
        public ZsDbContext(DbContextOptions<ZsDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<BarOrder> BarOrders { get; set; }
        public DbSet<BarOrderDetail> BarOrderDetails { get; set; }
        public DbSet<BarProduct> BarProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TransactionItem>(b =>
            {
                b.ToTable("TransactionItems");
                b.HasKey(t => t.Id);
                b.HasDiscriminator<string>("TransactionType")
                 .HasValue<BarOrder>("BarOrder");
            });

            modelBuilder.Entity<Transaction>(b =>
            {
                b.ToTable("Transactions");
                b.HasKey(t => t.Id);
                b.HasOne(t => t.TransactionItem)
                 .WithMany()
                 .HasForeignKey(t => t.TransactionItemId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<BarOrderDetail>()
                .HasKey(d => new { d.BarOrderId, d.BarProductId }); // Composite PK

            modelBuilder.Entity<BarOrderDetail>()
                .HasOne(d => d.BarOrder)
                .WithMany(o => o.Details)
                .HasForeignKey(d => d.BarOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BarOrderDetail>()
                .HasOne(d => d.BarProduct)
                .WithMany()
                .HasForeignKey(d => d.BarProductId);

            // Data Seeding

            var clients = new List<Client>()
            {
                new Client {
                    Id = Guid.Parse("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
                    NationalId = "0102030405",
                    Name = "Carlos Pérez",
                    Email = "carlos.perez@example.com",
                    Address = "Av. Amazonas N34-120, Quito",
                    Number = "0991234567"
                },
                new Client {
                    Id = Guid.Parse("b68c1f84-4a8c-4ee3-8a6a-4b35a27ad331"),
                    NationalId = "1102233445",
                    Name = "María Gómez",
                    Email = "maria.gomez@example.com",
                    Address = "Calle 10 y Av. 6 de Diciembre, Quito",
                    Number = "0987654321"
                },
                new Client {
                    Id = Guid.Parse("c8b5c2df-4b91-4239-b134-88a3d52f91d8"),
                    NationalId = "0923456789",
                    Name = "Luis Torres",
                    Email = "luis.torres@example.com",
                    Address = "Cdla. Los Ceibos, Guayaquil",
                    Number = "0971122334"
                },
                new Client {
                    Id = Guid.Parse("e6ab1df7-b60b-4db9-96f7-4f6cb5f00219"),
                    NationalId = "1309876543",
                    Name = "Ana Morales",
                    Email = "ana.morales@example.com",
                    Address = "Av. Flavio Alfaro y 13 de Abril, Manta",
                    Number = "0969988776"
                },
                new Client {
                    Id = Guid.Parse("f97b5672-00b9-48c9-85b4-3b897e8af8bb"),
                    NationalId = "1723345566",
                    Name = "José Herrera",
                    Email = "jose.herrera@example.com",
                    Address = "La Mariscal, Av. Colón y Reina Victoria, Quito",
                    Number = "0995566778"
                },
                new Client {
                    Id = Guid.Parse("b02cfb22-4e26-4dd3-8e62-43d29cb86e1e"),
                    NationalId = "1803342211",
                    Name = "Sofía Ruiz",
                    Email = "sofia.ruiz@example.com",
                    Address = "Centro Histórico, Loja",
                    Number = "0954433221"
                },
                new Client {
                    Id = Guid.Parse("3a28b14c-4f9d-4d4c-9021-1894a8b6a2d1"),
                    NationalId = "0911223344",
                    Name = "Andrés Castillo",
                    Email = "andres.castillo@example.com",
                    Address = "Av. de las Américas, Cuenca",
                    Number = "0945566778"
                },
                new Client {
                    Id = Guid.Parse("d5127f6c-6b5b-4b4a-a9e1-2b8d4f508e45"),
                    NationalId = "1204433221",
                    Name = "Gabriela Chávez",
                    Email = "gabriela.chavez@example.com",
                    Address = "Av. Eloy Alfaro y Portugal, Quito",
                    Number = "0983344556"
                },
                new Client {
                    Id = Guid.Parse("1f8a42f5-9e9a-4c1b-bc5a-8d4129fd78c2"),
                    NationalId = "1005566778",
                    Name = "Ricardo Mendoza",
                    Email = "ricardo.mendoza@example.com",
                    Address = "Cdla. Alborada, Mz. 103, Guayaquil",
                    Number = "0978899001"
                 },
                new Client {
                    Id = Guid.Parse("e71b9b12-84c3-4c6b-b6e5-5f889b5cf2b7"),
                    NationalId = "1509988776",
                    Name = "Elena Vega",
                    Email = "elena.vega@example.com",
                    Address = "Av. Universitaria, Ambato",
                    Number = "0932211445"
                }
            };

            var barProducts = new List<BarProduct>()
            {
                new BarProduct
                {
                    Id = Guid.Parse("626e7d51-6573-4b46-b789-36756d9cffb5"),
                    Name = "Lager Beer",
                    Qty = 120,
                    UnitPrice = 3.50
                },
                new BarProduct
                {
                    Id = Guid.Parse("66274ceb-b662-4f3e-b413-7ebc64426af7"),
                    Name = "Whiskey Shot",
                    Qty = 80,
                    UnitPrice = 5.75
                },
                new BarProduct
                {
                    Id = Guid.Parse("09df7b74-9cb1-4605-9fc7-5054b109c285"),
                    Name = "Red Wine Glass",
                    Qty = 60,
                    UnitPrice = 6.25
                },
                new BarProduct
                {
                    Id = Guid.Parse("bd68b7a0-bce6-4159-818a-4bc084ae45fe"),
                    Name = "Margarita",
                    Qty = 45,
                    UnitPrice = 7.50
                },
                new BarProduct
                {
                    Id = Guid.Parse("8c758180-2375-4b27-bea4-d25e8a657608"),
                    Name = "Gin Tonic",
                    Qty = 70,
                    UnitPrice = 6.00
                },
                new BarProduct
                {
                    Id = Guid.Parse("ccde323d-2a39-4cd1-96d8-a634d3a87d33"),
                    Name = "Rum & Coke",
                    Qty = 90,
                    UnitPrice = 5.25
                },
                new BarProduct
                {
                    Id = Guid.Parse("afc8b253-4a00-4e0b-8993-5e74605ca49f"),
                    Name = "Tequila Shot",
                    Qty = 100,
                    UnitPrice = 4.75
                },
                new BarProduct
                {
                    Id = Guid.Parse("16a4efed-7353-48ed-b046-2532bdc62c74"),
                    Name = "Craft IPA Beer",
                    Qty = 110,
                    UnitPrice = 4.25
                }
            };

            modelBuilder.Entity<Client>().HasData(clients);
            modelBuilder.Entity<BarProduct>().HasData(barProducts);
        }
    }
}
