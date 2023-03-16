using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project1.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MassEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MassEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    AdditionalPeoples = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    preferred_type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    First_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Last_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rules = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Stars = table.Column<int>(type: "int", nullable: false),
                    MassEventId = table.Column<int>(type: "int", nullable: false),
                    MassEventId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hotels_MassEvents_MassEventId1",
                        column: x => x.MassEventId1,
                        principalTable: "MassEvents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    UserId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MassEventId = table.Column<int>(type: "int", nullable: false),
                    MassEventId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_MassEvents_MassEventId1",
                        column: x => x.MassEventId1,
                        principalTable: "MassEvents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Groups_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DifferenceDataHotel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    HotelId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MassEventId = table.Column<int>(type: "int", nullable: false),
                    MassEventId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DifferenceDataHotel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DifferenceDataHotel_Hotels_HotelId1",
                        column: x => x.HotelId1,
                        principalTable: "Hotels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DifferenceDataHotel_MassEvents_MassEventId1",
                        column: x => x.MassEventId1,
                        principalTable: "MassEvents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DifferenceDataHotel_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EnteredDataHotel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Сapacity = table.Column<int>(type: "int", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    HotelId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    MassEventId = table.Column<int>(type: "int", nullable: false),
                    MassEventId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnteredDataHotel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnteredDataHotel_Hotels_HotelId1",
                        column: x => x.HotelId1,
                        principalTable: "Hotels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EnteredDataHotel_MassEvents_MassEventId1",
                        column: x => x.MassEventId1,
                        principalTable: "MassEvents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EnteredDataHotel_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RecordDataHotel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    HotelId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MassEventId = table.Column<int>(type: "int", nullable: false),
                    MassEventId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordDataHotel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecordDataHotel_Hotels_HotelId1",
                        column: x => x.HotelId1,
                        principalTable: "Hotels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RecordDataHotel_MassEvents_MassEventId1",
                        column: x => x.MassEventId1,
                        principalTable: "MassEvents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserXHotels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    HotelId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserXHotels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserXHotels_Hotels_HotelId1",
                        column: x => x.HotelId1,
                        principalTable: "Hotels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserXHotels_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DifferenceDataHotel_HotelId1",
                table: "DifferenceDataHotel",
                column: "HotelId1");

            migrationBuilder.CreateIndex(
                name: "IX_DifferenceDataHotel_MassEventId1",
                table: "DifferenceDataHotel",
                column: "MassEventId1");

            migrationBuilder.CreateIndex(
                name: "IX_DifferenceDataHotel_UserId1",
                table: "DifferenceDataHotel",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_EnteredDataHotel_HotelId1",
                table: "EnteredDataHotel",
                column: "HotelId1");

            migrationBuilder.CreateIndex(
                name: "IX_EnteredDataHotel_MassEventId1",
                table: "EnteredDataHotel",
                column: "MassEventId1");

            migrationBuilder.CreateIndex(
                name: "IX_EnteredDataHotel_UserId1",
                table: "EnteredDataHotel",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_MassEventId1",
                table: "Groups",
                column: "MassEventId1");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_UserId1",
                table: "Groups",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_MassEventId1",
                table: "Hotels",
                column: "MassEventId1");

            migrationBuilder.CreateIndex(
                name: "IX_RecordDataHotel_HotelId1",
                table: "RecordDataHotel",
                column: "HotelId1");

            migrationBuilder.CreateIndex(
                name: "IX_RecordDataHotel_MassEventId1",
                table: "RecordDataHotel",
                column: "MassEventId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserXHotels_HotelId1",
                table: "UserXHotels",
                column: "HotelId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserXHotels_UserId1",
                table: "UserXHotels",
                column: "UserId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DifferenceDataHotel");

            migrationBuilder.DropTable(
                name: "EnteredDataHotel");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "RecordDataHotel");

            migrationBuilder.DropTable(
                name: "Settler");

            migrationBuilder.DropTable(
                name: "UserXHotels");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "MassEvents");
        }
    }
}
