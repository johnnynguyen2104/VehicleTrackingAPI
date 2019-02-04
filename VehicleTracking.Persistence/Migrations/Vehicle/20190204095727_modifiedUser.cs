using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleTracking.Persistence.Migrations.Vehicle
{
    public partial class modifiedUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "UsersSystem",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDateTime",
                table: "UsersSystem",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "UsersSystem");

            migrationBuilder.DropColumn(
                name: "UpdatedDateTime",
                table: "UsersSystem");
        }
    }
}
