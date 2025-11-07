using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend_ZS.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Keys",
                columns: new[] { "Id", "Available", "KeyCode", "LastAssignedTo", "Notes" },
                values: new object[,]
                {
                    { new Guid("0e6ec433-ce0b-41eb-b3c8-c3f643bb4c8e"), true, "13M", null, null },
                    { new Guid("0f0c3028-0ab8-49fb-9ac5-b159180301a3"), true, "12H", null, null },
                    { new Guid("18135631-9634-4394-8e02-edb76898fc33"), true, "10H", null, null },
                    { new Guid("2338b9bd-ee2a-4af3-8c75-00b43598b75d"), true, "9M", null, null },
                    { new Guid("30c6f6a7-4f56-4eed-bb46-a8e842f1530e"), true, "4H", null, null },
                    { new Guid("3d0b9380-2ecd-4b59-806b-d3a492b3fd23"), true, "7H", null, null },
                    { new Guid("3f0b2c42-d013-43cc-b9a0-31624540de66"), true, "9H", null, null },
                    { new Guid("413c9a8b-4357-4c46-b763-8858c71acee9"), true, "5M", null, null },
                    { new Guid("45444a34-3c82-479c-8508-6a2bc2bc62a3"), true, "1H", null, null },
                    { new Guid("4d03a50b-5e50-4348-9b06-9a783413a01b"), true, "12M", null, null },
                    { new Guid("4d818dc0-7822-4758-98a7-7cf2959e3b00"), true, "15M", null, null },
                    { new Guid("4f0bf30c-4196-4583-9467-a87ac7b6f7b5"), true, "11H", null, null },
                    { new Guid("59fa5bbe-058e-491e-bc68-d8447c0ff854"), true, "8M", null, null },
                    { new Guid("6cfd2ac6-97e4-4655-97f4-f25b59ec02b2"), true, "15H", null, null },
                    { new Guid("77325ac7-b5e7-4ef3-85d3-508f35f5a6a3"), true, "2M", null, null },
                    { new Guid("7e67dc08-3dc4-48fe-914f-056bb32b5708"), true, "13H", null, null },
                    { new Guid("85c4a0eb-1bf1-4f4b-82be-1866cbd81452"), true, "7M", null, null },
                    { new Guid("872ce271-7889-43f5-9026-be8fba6f09e2"), true, "3M", null, null },
                    { new Guid("8b894d60-ec69-4ce4-8f4c-a356876e6663"), true, "3H", null, null },
                    { new Guid("8f39c2ce-900a-4f7a-87f9-185883dcd676"), true, "6H", null, null },
                    { new Guid("9a2288a1-4efe-41ed-996b-8dbf42f276eb"), true, "1M", null, null },
                    { new Guid("ab87b21d-fcc7-476f-97fa-f7122ffcc7b4"), true, "16H", null, null },
                    { new Guid("b5cc7d05-0a2a-4e21-8093-a43b6567f764"), true, "6M", null, null },
                    { new Guid("ba54bb28-0887-4583-bd32-26d65b56283f"), true, "14M", null, null },
                    { new Guid("c21eb091-27f3-4238-ac95-f3597ac99a6b"), true, "11M", null, null },
                    { new Guid("c42c5738-e341-4271-837f-fe14027a1629"), true, "8H", null, null },
                    { new Guid("cae24c39-7da2-44da-b7be-541f4d1249e1"), true, "2H", null, null },
                    { new Guid("ddf2eaa4-251e-428e-b1cc-6f65b40af74a"), true, "16M", null, null },
                    { new Guid("e0178342-b601-49f5-aa8b-dc586d6c2bde"), true, "4M", null, null },
                    { new Guid("e2ce6866-d740-4eea-8abc-ebd5ff126f71"), true, "10M", null, null },
                    { new Guid("e4f24e5b-91c5-48f6-92fc-9987ae7944fb"), true, "14H", null, null },
                    { new Guid("ee58c343-f78d-4f7c-8666-43f10e7cc3e6"), true, "5H", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Keys",
                keyColumn: "Id",
                keyValue: new Guid("0e6ec433-ce0b-41eb-b3c8-c3f643bb4c8e"));

            migrationBuilder.DeleteData(
                table: "Keys",
                keyColumn: "Id",
                keyValue: new Guid("0f0c3028-0ab8-49fb-9ac5-b159180301a3"));

            migrationBuilder.DeleteData(
                table: "Keys",
                keyColumn: "Id",
                keyValue: new Guid("18135631-9634-4394-8e02-edb76898fc33"));

            migrationBuilder.DeleteData(
                table: "Keys",
                keyColumn: "Id",
                keyValue: new Guid("2338b9bd-ee2a-4af3-8c75-00b43598b75d"));

            migrationBuilder.DeleteData(
                table: "Keys",
                keyColumn: "Id",
                keyValue: new Guid("30c6f6a7-4f56-4eed-bb46-a8e842f1530e"));

            migrationBuilder.DeleteData(
                table: "Keys",
                keyColumn: "Id",
                keyValue: new Guid("3d0b9380-2ecd-4b59-806b-d3a492b3fd23"));

            migrationBuilder.DeleteData(
                table: "Keys",
                keyColumn: "Id",
                keyValue: new Guid("3f0b2c42-d013-43cc-b9a0-31624540de66"));

            migrationBuilder.DeleteData(
                table: "Keys",
                keyColumn: "Id",
                keyValue: new Guid("413c9a8b-4357-4c46-b763-8858c71acee9"));

            migrationBuilder.DeleteData(
                table: "Keys",
                keyColumn: "Id",
                keyValue: new Guid("45444a34-3c82-479c-8508-6a2bc2bc62a3"));

            migrationBuilder.DeleteData(
                table: "Keys",
                keyColumn: "Id",
                keyValue: new Guid("4d03a50b-5e50-4348-9b06-9a783413a01b"));

            migrationBuilder.DeleteData(
                table: "Keys",
                keyColumn: "Id",
                keyValue: new Guid("4d818dc0-7822-4758-98a7-7cf2959e3b00"));

            migrationBuilder.DeleteData(
                table: "Keys",
                keyColumn: "Id",
                keyValue: new Guid("4f0bf30c-4196-4583-9467-a87ac7b6f7b5"));

            migrationBuilder.DeleteData(
                table: "Keys",
                keyColumn: "Id",
                keyValue: new Guid("59fa5bbe-058e-491e-bc68-d8447c0ff854"));

            migrationBuilder.DeleteData(
                table: "Keys",
                keyColumn: "Id",
                keyValue: new Guid("6cfd2ac6-97e4-4655-97f4-f25b59ec02b2"));

            migrationBuilder.DeleteData(
                table: "Keys",
                keyColumn: "Id",
                keyValue: new Guid("77325ac7-b5e7-4ef3-85d3-508f35f5a6a3"));

            migrationBuilder.DeleteData(
                table: "Keys",
                keyColumn: "Id",
                keyValue: new Guid("7e67dc08-3dc4-48fe-914f-056bb32b5708"));

            migrationBuilder.DeleteData(
                table: "Keys",
                keyColumn: "Id",
                keyValue: new Guid("85c4a0eb-1bf1-4f4b-82be-1866cbd81452"));

            migrationBuilder.DeleteData(
                table: "Keys",
                keyColumn: "Id",
                keyValue: new Guid("872ce271-7889-43f5-9026-be8fba6f09e2"));

            migrationBuilder.DeleteData(
                table: "Keys",
                keyColumn: "Id",
                keyValue: new Guid("8b894d60-ec69-4ce4-8f4c-a356876e6663"));

            migrationBuilder.DeleteData(
                table: "Keys",
                keyColumn: "Id",
                keyValue: new Guid("8f39c2ce-900a-4f7a-87f9-185883dcd676"));

            migrationBuilder.DeleteData(
                table: "Keys",
                keyColumn: "Id",
                keyValue: new Guid("9a2288a1-4efe-41ed-996b-8dbf42f276eb"));

            migrationBuilder.DeleteData(
                table: "Keys",
                keyColumn: "Id",
                keyValue: new Guid("ab87b21d-fcc7-476f-97fa-f7122ffcc7b4"));

            migrationBuilder.DeleteData(
                table: "Keys",
                keyColumn: "Id",
                keyValue: new Guid("b5cc7d05-0a2a-4e21-8093-a43b6567f764"));

            migrationBuilder.DeleteData(
                table: "Keys",
                keyColumn: "Id",
                keyValue: new Guid("ba54bb28-0887-4583-bd32-26d65b56283f"));

            migrationBuilder.DeleteData(
                table: "Keys",
                keyColumn: "Id",
                keyValue: new Guid("c21eb091-27f3-4238-ac95-f3597ac99a6b"));

            migrationBuilder.DeleteData(
                table: "Keys",
                keyColumn: "Id",
                keyValue: new Guid("c42c5738-e341-4271-837f-fe14027a1629"));

            migrationBuilder.DeleteData(
                table: "Keys",
                keyColumn: "Id",
                keyValue: new Guid("cae24c39-7da2-44da-b7be-541f4d1249e1"));

            migrationBuilder.DeleteData(
                table: "Keys",
                keyColumn: "Id",
                keyValue: new Guid("ddf2eaa4-251e-428e-b1cc-6f65b40af74a"));

            migrationBuilder.DeleteData(
                table: "Keys",
                keyColumn: "Id",
                keyValue: new Guid("e0178342-b601-49f5-aa8b-dc586d6c2bde"));

            migrationBuilder.DeleteData(
                table: "Keys",
                keyColumn: "Id",
                keyValue: new Guid("e2ce6866-d740-4eea-8abc-ebd5ff126f71"));

            migrationBuilder.DeleteData(
                table: "Keys",
                keyColumn: "Id",
                keyValue: new Guid("e4f24e5b-91c5-48f6-92fc-9987ae7944fb"));

            migrationBuilder.DeleteData(
                table: "Keys",
                keyColumn: "Id",
                keyValue: new Guid("ee58c343-f78d-4f7c-8666-43f10e7cc3e6"));
        }
    }
}
