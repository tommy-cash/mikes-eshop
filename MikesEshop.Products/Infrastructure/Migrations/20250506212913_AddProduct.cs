using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MikesEshop.Products.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StockedQuantity = table.Column<int>(type: "int", nullable: true),
                    Price_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Price_Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dimensions_Width = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Dimensions_Height = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Dimensions_Depth = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
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
