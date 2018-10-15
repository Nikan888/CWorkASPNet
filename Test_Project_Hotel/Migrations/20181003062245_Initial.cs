using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Test_Project_Hotel.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClientFIO = table.Column<string>(nullable: true),
                    ClientPassportData = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientID);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoomCapacity = table.Column<int>(nullable: false),
                    RoomDescription = table.Column<string>(nullable: true),
                    RoomPrice = table.Column<decimal>(nullable: false),
                    RoomType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomID);
                });

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    WorkerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WorkerFIO = table.Column<string>(nullable: true),
                    WorkerPost = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.WorkerID);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ServiceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClientID = table.Column<int>(nullable: true),
                    DepartureDate = table.Column<DateTime>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    RoomID = table.Column<int>(nullable: true),
                    ServiceDescription = table.Column<string>(nullable: true),
                    ServiceName = table.Column<string>(nullable: true),
                    WorkerID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceID);
                    table.ForeignKey(
                        name: "FK_Services_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Services_Rooms_RoomID",
                        column: x => x.RoomID,
                        principalTable: "Rooms",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Services_Workers_WorkerID",
                        column: x => x.WorkerID,
                        principalTable: "Workers",
                        principalColumn: "WorkerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Services_ClientID",
                table: "Services",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Services_RoomID",
                table: "Services",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_Services_WorkerID",
                table: "Services",
                column: "WorkerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Workers");
        }
    }
}
