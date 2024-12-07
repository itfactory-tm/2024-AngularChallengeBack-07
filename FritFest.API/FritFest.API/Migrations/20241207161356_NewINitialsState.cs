using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FritFest.API.Migrations
{
    /// <inheritdoc />
    public partial class NewINitialsState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Day",
                columns: table => new
                {
                    DayId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Day", x => x.DayId);
                });

            migrationBuilder.CreateTable(
                name: "Edition",
                columns: table => new
                {
                    EditionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EditionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Municipality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Edition", x => x.EditionId);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "TicketType",
                columns: table => new
                {
                    TicketTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketType", x => x.TicketTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    ArticleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.ArticleId);
                    table.ForeignKey(
                        name: "FK_Article_Edition_EditionId",
                        column: x => x.EditionId,
                        principalTable: "Edition",
                        principalColumn: "EditionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Artist",
                columns: table => new
                {
                    ArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpotifyLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApiCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpotifyPhoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artist", x => x.ArtistId);
                    table.ForeignKey(
                        name: "FK_Artist_Edition_EditionId",
                        column: x => x.EditionId,
                        principalTable: "Edition",
                        principalColumn: "EditionId");
                });

            migrationBuilder.CreateTable(
                name: "Sponsor",
                columns: table => new
                {
                    SponsorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SponsorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    SponsoredItem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SponsorMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SponsorLogo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    EditionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sponsor", x => x.SponsorId);
                    table.ForeignKey(
                        name: "FK_Sponsor_Edition_EditionId",
                        column: x => x.EditionId,
                        principalTable: "Edition",
                        principalColumn: "EditionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodTruck",
                columns: table => new
                {
                    FoodTruckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EditionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodTruck", x => x.FoodTruckId);
                    table.ForeignKey(
                        name: "FK_FoodTruck_Edition_EditionId",
                        column: x => x.EditionId,
                        principalTable: "Edition",
                        principalColumn: "EditionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodTruck_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stage",
                columns: table => new
                {
                    StageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stage", x => x.StageId);
                    table.ForeignKey(
                        name: "FK_Stage_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    TicketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EditionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TicketTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_Ticket_Day_DayId",
                        column: x => x.DayId,
                        principalTable: "Day",
                        principalColumn: "DayId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ticket_Edition_EditionId",
                        column: x => x.EditionId,
                        principalTable: "Edition",
                        principalColumn: "EditionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ticket_TicketType_TicketTypeId",
                        column: x => x.TicketTypeId,
                        principalTable: "TicketType",
                        principalColumn: "TicketTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuItem",
                columns: table => new
                {
                    MenuItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    FoodTruckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItem", x => x.MenuItemId);
                    table.ForeignKey(
                        name: "FK_MenuItem_FoodTruck_FoodTruckId",
                        column: x => x.FoodTruckId,
                        principalTable: "FoodTruck",
                        principalColumn: "FoodTruckId");
                });

            migrationBuilder.CreateTable(
                name: "TimeSlot",
                columns: table => new
                {
                    TimeSlotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlot", x => x.TimeSlotId);
                    table.ForeignKey(
                        name: "FK_TimeSlot_Artist_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artist",
                        principalColumn: "ArtistId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimeSlot_Stage_StageId",
                        column: x => x.StageId,
                        principalTable: "Stage",
                        principalColumn: "StageId");
                });

            migrationBuilder.CreateTable(
                name: "BoughtTicket",
                columns: table => new
                {
                    BoughtTicketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuyerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HolderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HolderMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TicketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Payed = table.Column<bool>(type: "bit", nullable: false),
                    EditionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoughtTicket", x => x.BoughtTicketId);
                    table.ForeignKey(
                        name: "FK_BoughtTicket_Edition_EditionId",
                        column: x => x.EditionId,
                        principalTable: "Edition",
                        principalColumn: "EditionId");
                    table.ForeignKey(
                        name: "FK_BoughtTicket_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Ticket",
                        principalColumn: "TicketId");
                });

            migrationBuilder.CreateTable(
                name: "DayList",
                columns: table => new
                {
                    TicketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayList", x => new { x.TicketId, x.DayId });
                    table.ForeignKey(
                        name: "FK_DayList_Day_DayId",
                        column: x => x.DayId,
                        principalTable: "Day",
                        principalColumn: "DayId");
                    table.ForeignKey(
                        name: "FK_DayList_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Ticket",
                        principalColumn: "TicketId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Article_EditionId",
                table: "Article",
                column: "EditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Artist_EditionId",
                table: "Artist",
                column: "EditionId");

            migrationBuilder.CreateIndex(
                name: "IX_BoughtTicket_EditionId",
                table: "BoughtTicket",
                column: "EditionId");

            migrationBuilder.CreateIndex(
                name: "IX_BoughtTicket_TicketId",
                table: "BoughtTicket",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_DayList_DayId",
                table: "DayList",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodTruck_EditionId",
                table: "FoodTruck",
                column: "EditionId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodTruck_LocationId",
                table: "FoodTruck",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItem_FoodTruckId",
                table: "MenuItem",
                column: "FoodTruckId");

            migrationBuilder.CreateIndex(
                name: "IX_Sponsor_EditionId",
                table: "Sponsor",
                column: "EditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Stage_LocationId",
                table: "Stage",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_DayId",
                table: "Ticket",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_EditionId",
                table: "Ticket",
                column: "EditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_TicketTypeId",
                table: "Ticket",
                column: "TicketTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlot_ArtistId",
                table: "TimeSlot",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlot_StageId",
                table: "TimeSlot",
                column: "StageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "BoughtTicket");

            migrationBuilder.DropTable(
                name: "DayList");

            migrationBuilder.DropTable(
                name: "MenuItem");

            migrationBuilder.DropTable(
                name: "Sponsor");

            migrationBuilder.DropTable(
                name: "TimeSlot");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "FoodTruck");

            migrationBuilder.DropTable(
                name: "Artist");

            migrationBuilder.DropTable(
                name: "Stage");

            migrationBuilder.DropTable(
                name: "Day");

            migrationBuilder.DropTable(
                name: "TicketType");

            migrationBuilder.DropTable(
                name: "Edition");

            migrationBuilder.DropTable(
                name: "Location");
        }
    }
}
