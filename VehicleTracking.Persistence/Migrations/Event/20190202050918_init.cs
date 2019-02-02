using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleTracking.Persistence.Migrations.Event
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrackingPoints",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(50)", nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(nullable: true),
                    Latitude = table.Column<string>(type: "varchar(20)", nullable: false),
                    Longitude = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackingPoints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrackingPointSnapshots",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(50)", nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(nullable: true),
                    VehicleReferencedCode = table.Column<string>(type: "varchar(50)", nullable: false),
                    StartPointId = table.Column<string>(type: "varchar(50)", nullable: false),
                    Note = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackingPointSnapshots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrackingPointSnapshots_TrackingPoints_StartPointId",
                        column: x => x.StartPointId,
                        principalTable: "TrackingPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrackingPointSnapshots_StartPointId",
                table: "TrackingPointSnapshots",
                column: "StartPointId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrackingPointSnapshots");

            migrationBuilder.DropTable(
                name: "TrackingPoints");
        }
    }
}
