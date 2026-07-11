using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductManagementWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class addProductTableWithSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedDate", "Description", "Name", "Price", "Quantity", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dell Inspiron 15, Intel Core i5, 16GB RAM, 512GB SSD", "Laptop", 75000.00m, 10, null },
                    { 2, new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Samsung Galaxy A55, 8GB RAM, 256GB Storage", "Smartphone", 45000.00m, 20, null },
                    { 3, new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Logitech M331 Silent Plus Wireless Mouse", "Wireless Mouse", 1500.00m, 50, null },
                    { 4, new DateTime(2025, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Redragon K552 RGB Mechanical Gaming Keyboard", "Mechanical Keyboard", 3500.00m, 30, null },
                    { 5, new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "LG 24-inch Full HD IPS Monitor", "Monitor", 18000.00m, 15, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
