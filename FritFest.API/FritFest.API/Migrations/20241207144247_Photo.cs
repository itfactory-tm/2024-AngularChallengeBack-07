using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FritFest.API.Migrations
{
    /// <inheritdoc />
    public partial class Photo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_Stage_StageId",
                table: "Photo");

            migrationBuilder.DropIndex(
                name: "IX_Photo_StageId",
                table: "Photo");

            migrationBuilder.DropColumn(
                name: "StageId",
                table: "Photo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StageId",
                table: "Photo",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Photo_StageId",
                table: "Photo",
                column: "StageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_Stage_StageId",
                table: "Photo",
                column: "StageId",
                principalTable: "Stage",
                principalColumn: "StageId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
