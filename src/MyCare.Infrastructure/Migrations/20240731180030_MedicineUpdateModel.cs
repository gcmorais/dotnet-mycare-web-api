using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCare.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MedicineUpdateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActivePrinciple",
                table: "Medicines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Medicines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Class",
                table: "Medicines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Manufacturer",
                table: "Medicines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Prescription",
                table: "Medicines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "Medicines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TherapeuticCategory",
                table: "Medicines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivePrinciple",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "Class",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "Manufacturer",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "Prescription",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "Reference",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "TherapeuticCategory",
                table: "Medicines");
        }
    }
}
