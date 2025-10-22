using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend_ZS.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedClients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Address", "Email", "Name", "NationalId", "Number" },
                values: new object[,]
                {
                    { new Guid("1f8a42f5-9e9a-4c1b-bc5a-8d4129fd78c2"), "Cdla. Alborada, Mz. 103, Guayaquil", "ricardo.mendoza@example.com", "Ricardo Mendoza", "1005566778", "0978899001" },
                    { new Guid("3a28b14c-4f9d-4d4c-9021-1894a8b6a2d1"), "Av. de las Américas, Cuenca", "andres.castillo@example.com", "Andrés Castillo", "0911223344", "0945566778" },
                    { new Guid("b02cfb22-4e26-4dd3-8e62-43d29cb86e1e"), "Centro Histórico, Loja", "sofia.ruiz@example.com", "Sofía Ruiz", "1803342211", "0954433221" },
                    { new Guid("b68c1f84-4a8c-4ee3-8a6a-4b35a27ad331"), "Calle 10 y Av. 6 de Diciembre, Quito", "maria.gomez@example.com", "María Gómez", "1102233445", "0987654321" },
                    { new Guid("c8b5c2df-4b91-4239-b134-88a3d52f91d8"), "Cdla. Los Ceibos, Guayaquil", "luis.torres@example.com", "Luis Torres", "0923456789", "0971122334" },
                    { new Guid("d5127f6c-6b5b-4b4a-a9e1-2b8d4f508e45"), "Av. Eloy Alfaro y Portugal, Quito", "gabriela.chavez@example.com", "Gabriela Chávez", "1204433221", "0983344556" },
                    { new Guid("e6ab1df7-b60b-4db9-96f7-4f6cb5f00219"), "Av. Flavio Alfaro y 13 de Abril, Manta", "ana.morales@example.com", "Ana Morales", "1309876543", "0969988776" },
                    { new Guid("e71b9b12-84c3-4c6b-b6e5-5f889b5cf2b7"), "Av. Universitaria, Ambato", "elena.vega@example.com", "Elena Vega", "1509988776", "0932211445" },
                    { new Guid("f7248fc3-2585-4efb-8d1d-1c555f4087f6"), "Av. Amazonas N34-120, Quito", "carlos.perez@example.com", "Carlos Pérez", "0102030405", "0991234567" },
                    { new Guid("f97b5672-00b9-48c9-85b4-3b897e8af8bb"), "La Mariscal, Av. Colón y Reina Victoria, Quito", "jose.herrera@example.com", "José Herrera", "1723345566", "0995566778" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("1f8a42f5-9e9a-4c1b-bc5a-8d4129fd78c2"));

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("3a28b14c-4f9d-4d4c-9021-1894a8b6a2d1"));

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("b02cfb22-4e26-4dd3-8e62-43d29cb86e1e"));

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("b68c1f84-4a8c-4ee3-8a6a-4b35a27ad331"));

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("c8b5c2df-4b91-4239-b134-88a3d52f91d8"));

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("d5127f6c-6b5b-4b4a-a9e1-2b8d4f508e45"));

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("e6ab1df7-b60b-4db9-96f7-4f6cb5f00219"));

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("e71b9b12-84c3-4c6b-b6e5-5f889b5cf2b7"));

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("f7248fc3-2585-4efb-8d1d-1c555f4087f6"));

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("f97b5672-00b9-48c9-85b4-3b897e8af8bb"));
        }
    }
}
