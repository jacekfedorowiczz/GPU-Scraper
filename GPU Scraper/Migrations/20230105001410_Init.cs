using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GPUScraper.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GPUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LowestPrice = table.Column<double>(type: "float(7)", precision: 7, scale: 2, nullable: false),
                    LowestPriceShop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HighestPrice = table.Column<double>(type: "float(7)", precision: 7, scale: 2, nullable: true),
                    HighestPriceShop = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GPUs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GPUs");
        }
    }
}
