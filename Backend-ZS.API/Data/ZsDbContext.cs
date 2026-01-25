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
        public DbSet<AccessCard> AccessCards { get; set; }
        public DbSet<Key> Keys { get; set; }
        public DbSet<Parking> Parkings { get; set; }
        public DbSet<EntranceTransaction> EntranceTransactions { get; set; }
        public DbSet<EntranceAccessCard> EntranceAccessCards { get; set; }
        public DbSet<CashBox> CashBoxes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // CashBox configuration
            modelBuilder.Entity<CashBox>(b =>
            {
                b.ToTable("CashBoxes");
                b.HasKey(x => x.Id);

                b.Property(x => x.Status).HasConversion<string>();
            });


            modelBuilder.Entity<Transaction>(b =>
            {
                b.ToTable("Transactions");
                b.HasKey(t => t.Id);

                b.Property(x => x.Status).HasConversion<string>();

                // Client (1) <---> (0..*) Transaction
                b.HasOne(t => t.Client)
                 .WithMany(c => c.Transactions)
                 .HasForeignKey(t => t.ClientId)
                 .OnDelete(DeleteBehavior.Restrict);

                // ✅ CashBox (1) <---> (0..*) Transaction
                b.HasOne(t => t.CashBox)
                 .WithMany(cb => cb.Transactions)
                 .HasForeignKey(t => t.CashBoxId)
                 .OnDelete(DeleteBehavior.Restrict);

                // Payments (1) <---> (0..*) Payment
                b.HasMany(t => t.Payments)
                 .WithOne(p => p.Transaction)
                 .HasForeignKey(p => p.TransactionId)
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

            // Guarda tipo de pago en columna como string
            modelBuilder.Entity<Payment>()
                .Property(p => p.Type)
                .HasConversion<string>();

            // Data Seeding

            var clients = new List<Client>()
            {
                new Client {
                    Id = Guid.Parse("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
                    DocumentNumber = "0102030405",
                    Name = "Carlos Pérez",
                    Email = "carlos.perez@example.com",
                    Address = "Av. Amazonas N34-120, Quito",
                    Number = "0991234567"
                },
                new Client {
                    Id = Guid.Parse("b68c1f84-4a8c-4ee3-8a6a-4b35a27ad331"),
                    DocumentNumber = "1102233445",
                    Name = "María Gómez",
                    Email = "maria.gomez@example.com",
                    Address = "Calle 10 y Av. 6 de Diciembre, Quito",
                    Number = "0987654321"
                },
                new Client {
                    Id = Guid.Parse("c8b5c2df-4b91-4239-b134-88a3d52f91d8"),
                    DocumentNumber = "0923456789",
                    Name = "Luis Torres",
                    Email = "luis.torres@example.com",
                    Address = "Cdla. Los Ceibos, Guayaquil",
                    Number = "0971122334"
                },
                new Client {
                    Id = Guid.Parse("e6ab1df7-b60b-4db9-96f7-4f6cb5f00219"),
                    DocumentNumber = "1309876543",
                    Name = "Ana Morales",
                    Email = "ana.morales@example.com",
                    Address = "Av. Flavio Alfaro y 13 de Abril, Manta",
                    Number = "0969988776"
                },
                new Client {
                    Id = Guid.Parse("f97b5672-00b9-48c9-85b4-3b897e8af8bb"),
                    DocumentNumber = "1723345566",
                    Name = "José Herrera",
                    Email = "jose.herrera@example.com",
                    Address = "La Mariscal, Av. Colón y Reina Victoria, Quito",
                    Number = "0995566778"
                },
                new Client {
                    Id = Guid.Parse("b02cfb22-4e26-4dd3-8e62-43d29cb86e1e"),
                    DocumentNumber = "1803342211",
                    Name = "Sofía Ruiz",
                    Email = "sofia.ruiz@example.com",
                    Address = "Centro Histórico, Loja",
                    Number = "0954433221"
                },
                new Client {
                    Id = Guid.Parse("3a28b14c-4f9d-4d4c-9021-1894a8b6a2d1"),
                    DocumentNumber = "0911223344",
                    Name = "Andrés Castillo",
                    Email = "andres.castillo@example.com",
                    Address = "Av. de las Américas, Cuenca",
                    Number = "0945566778"
                },
                new Client {
                    Id = Guid.Parse("d5127f6c-6b5b-4b4a-a9e1-2b8d4f508e45"),
                    DocumentNumber = "1204433221",
                    Name = "Gabriela Chávez",
                    Email = "gabriela.chavez@example.com",
                    Address = "Av. Eloy Alfaro y Portugal, Quito",
                    Number = "0983344556"
                },
                new Client {
                    Id = Guid.Parse("1f8a42f5-9e9a-4c1b-bc5a-8d4129fd78c2"),
                    DocumentNumber = "1005566778",
                    Name = "Ricardo Mendoza",
                    Email = "ricardo.mendoza@example.com",
                    Address = "Cdla. Alborada, Mz. 103, Guayaquil",
                    Number = "0978899001"
                 },
                new Client {
                    Id = Guid.Parse("e71b9b12-84c3-4c6b-b6e5-5f889b5cf2b7"),
                    DocumentNumber = "1509988776",
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

            var keys = new List<Key>()
            {
                new Key { Id = Guid.Parse("45444a34-3c82-479c-8508-6a2bc2bc62a3"), KeyCode = "1H", TransactionId=null, Available=true, Notes=null },
                new Key { Id = Guid.Parse("cae24c39-7da2-44da-b7be-541f4d1249e1"), KeyCode = "2H", TransactionId=null, Available=true, Notes=null },
                new Key {Id = Guid.Parse("8b894d60-ec69-4ce4-8f4c-a356876e6663"), KeyCode = "3H", TransactionId = null, Available = true, Notes = null},
                new Key {Id = Guid.Parse("30c6f6a7-4f56-4eed-bb46-a8e842f1530e"), KeyCode = "4H", TransactionId = null, Available = true, Notes = null},
                new Key {Id = Guid.Parse("ee58c343-f78d-4f7c-8666-43f10e7cc3e6"), KeyCode = "5H", TransactionId = null, Available = true, Notes = null},
                new Key {Id = Guid.Parse("8f39c2ce-900a-4f7a-87f9-185883dcd676"), KeyCode = "6H", TransactionId = null, Available = true, Notes = null},
                new Key {Id = Guid.Parse("3d0b9380-2ecd-4b59-806b-d3a492b3fd23"), KeyCode = "7H", TransactionId = null, Available = true, Notes = null},
                new Key {Id = Guid.Parse("c42c5738-e341-4271-837f-fe14027a1629"), KeyCode = "8H", TransactionId = null, Available = true, Notes = null},
                new Key {Id = Guid.Parse("3f0b2c42-d013-43cc-b9a0-31624540de66"), KeyCode = "9H", TransactionId = null, Available = true, Notes = null},
                new Key {Id = Guid.Parse("18135631-9634-4394-8e02-edb76898fc33"), KeyCode = "10H", TransactionId = null, Available = true, Notes = null},
                new Key {Id = Guid.Parse("4f0bf30c-4196-4583-9467-a87ac7b6f7b5"), KeyCode = "11H", TransactionId = null, Available = true, Notes = null},
                new Key {Id = Guid.Parse("0f0c3028-0ab8-49fb-9ac5-b159180301a3"), KeyCode = "12H", TransactionId = null, Available = true, Notes = null},
                new Key {Id = Guid.Parse("7e67dc08-3dc4-48fe-914f-056bb32b5708"), KeyCode = "13H", TransactionId = null, Available = true, Notes = null},
                new Key {Id = Guid.Parse("e4f24e5b-91c5-48f6-92fc-9987ae7944fb"), KeyCode = "14H", TransactionId = null, Available = true, Notes = null},
                new Key {Id = Guid.Parse("6cfd2ac6-97e4-4655-97f4-f25b59ec02b2"), KeyCode = "15H", TransactionId = null, Available = true, Notes = null},
                new Key {Id = Guid.Parse("ab87b21d-fcc7-476f-97fa-f7122ffcc7b4"), KeyCode = "16H", TransactionId = null, Available = true, Notes = null},

                new Key {Id = Guid.Parse("9a2288a1-4efe-41ed-996b-8dbf42f276eb"), KeyCode = "1M", TransactionId = null, Available = true, Notes = null},
                new Key {Id = Guid.Parse("77325ac7-b5e7-4ef3-85d3-508f35f5a6a3"), KeyCode = "2M", TransactionId = null, Available = true, Notes = null},
                new Key {Id = Guid.Parse("872ce271-7889-43f5-9026-be8fba6f09e2"), KeyCode = "3M", TransactionId = null, Available = true, Notes = null},
                new Key {Id = Guid.Parse("e0178342-b601-49f5-aa8b-dc586d6c2bde"), KeyCode = "4M", TransactionId = null, Available = true, Notes = null},
                new Key {Id = Guid.Parse("413c9a8b-4357-4c46-b763-8858c71acee9"), KeyCode = "5M", TransactionId = null, Available = true, Notes = null},
                new Key {Id = Guid.Parse("b5cc7d05-0a2a-4e21-8093-a43b6567f764"), KeyCode = "6M", TransactionId = null, Available = true, Notes = null},
                new Key {Id = Guid.Parse("85c4a0eb-1bf1-4f4b-82be-1866cbd81452"), KeyCode = "7M", TransactionId = null, Available = true, Notes = null},
                new Key {Id = Guid.Parse("59fa5bbe-058e-491e-bc68-d8447c0ff854"), KeyCode = "8M", TransactionId = null, Available = true, Notes = null},
                new Key {Id = Guid.Parse("2338b9bd-ee2a-4af3-8c75-00b43598b75d"), KeyCode = "9M", TransactionId = null, Available = true, Notes = null},
                new Key {Id = Guid.Parse("e2ce6866-d740-4eea-8abc-ebd5ff126f71"), KeyCode = "10M", TransactionId = null, Available = true, Notes = null},
                new Key {Id = Guid.Parse("c21eb091-27f3-4238-ac95-f3597ac99a6b"), KeyCode = "11M", TransactionId = null, Available = true, Notes = null},
                new Key {Id = Guid.Parse("4d03a50b-5e50-4348-9b06-9a783413a01b"), KeyCode = "12M", TransactionId = null, Available = true, Notes = null},
                new Key {Id = Guid.Parse("0e6ec433-ce0b-41eb-b3c8-c3f643bb4c8e"), KeyCode = "13M", TransactionId = null, Available = true, Notes = null},
                new Key {Id = Guid.Parse("ba54bb28-0887-4583-bd32-26d65b56283f"), KeyCode = "14M", TransactionId = null, Available = true, Notes = null},
                new Key {Id = Guid.Parse("4d818dc0-7822-4758-98a7-7cf2959e3b00"), KeyCode = "15M", TransactionId = null, Available = true, Notes = null},
                new Key {Id = Guid.Parse("ddf2eaa4-251e-428e-b1cc-6f65b40af74a"), KeyCode = "16M", TransactionId = null, Available = true, Notes = null},
            };

            modelBuilder.Entity<Client>().HasData(clients);
            modelBuilder.Entity<BarProduct>().HasData(barProducts);
            modelBuilder.Entity<Key>().HasData(keys);

            // Configure shared column names for Parking and EntranceTransaction
            // Both implement IEntrance and share the same EntryTime/ExitTime columns
            modelBuilder.Entity<Parking>()
                .Property(p => p.EntryTime)
                .HasColumnName("EntryTime");
            modelBuilder.Entity<Parking>()
                .Property(p => p.ExitTime)
                .HasColumnName("ExitTime");

            modelBuilder.Entity<EntranceTransaction>()
                .Property(e => e.EntryTime)
                .HasColumnName("EntryTime");
            modelBuilder.Entity<EntranceTransaction>()
                .Property(e => e.ExitTime)
                .HasColumnName("ExitTime");

            // Configure TransactionItem to use TransactionType as discriminator
            modelBuilder.Entity<TransactionItem>(b =>
            {
                b.HasDiscriminator<string>("TransactionType")
                    .HasValue<AccessCard>("AccessCard")
                    .HasValue<BarOrder>("BarOrder")
                    .HasValue<EntranceTransaction>("EntranceTransaction")
                    .HasValue<Parking>("Parking");

                b.Property(x => x.TransactionType)
                    .HasMaxLength(21)
                    .IsRequired();
            });
        }
    }
}
