using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleTracking.Persistence.Migrations.Vehicle
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(50)", nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(nullable: true),
                    EntitiesName = table.Column<string>(type: "varchar(50)", nullable: false),
                    UpdatedData = table.Column<string>(type: "varchar(max)", nullable: true),
                    OldData = table.Column<string>(type: "varchar(max)", nullable: true),
                    EntityId = table.Column<string>(type: "varchar(50)", nullable: false),
                    Action = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicle",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(50)", nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(nullable: true),
                    DeviceCode = table.Column<string>(type: "varchar(20)", nullable: false),
                    ActivatedCode = table.Column<string>(type: "varchar(20)", nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    RegisteredName = table.Column<string>(nullable: false),
                    RegisteredPhone = table.Column<string>(type: "varchar(50)", nullable: false),
                    DeviceModel = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_ActivatedCode",
                table: "Vehicle",
                column: "ActivatedCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_DeviceCode",
                table: "Vehicle",
                column: "DeviceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_RegisteredPhone",
                table: "Vehicle",
                column: "RegisteredPhone",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "Vehicle");
        }
    }
}
