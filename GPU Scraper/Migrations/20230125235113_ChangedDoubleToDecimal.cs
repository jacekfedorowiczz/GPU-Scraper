using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GPUScraper.Migrations
{
    /// <inheritdoc />
    public partial class ChangedDoubleToDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "LowestPrice",
                table: "GPUs",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(7)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "HighestPrice",
                table: "GPUs",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(7)",
                oldPrecision: 7,
                oldScale: 2,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "LowestPrice",
                table: "GPUs",
                type: "float(7)",
                precision: 7,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2);

            migrationBuilder.AlterColumn<double>(
                name: "HighestPrice",
                table: "GPUs",
                type: "float(7)",
                precision: 7,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7,
                oldScale: 2,
                oldNullable: true);
        }
    }
}
