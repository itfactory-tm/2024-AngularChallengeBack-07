using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FritFest.API.Migrations
{
    /// <inheritdoc />
    public partial class SponsorLogo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SponsorLogo",
                table: "Sponsor");

            migrationBuilder.AddColumn<string>(
                name: "SponsorLogoUrl",
                table: "Sponsor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SponsorLogoUrl",
                table: "Sponsor");

            migrationBuilder.AddColumn<byte[]>(
                name: "SponsorLogo",
                table: "Sponsor",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
