using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleTracking.Persistence.Migrations.Tracking
{
    public partial class modifiedrelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrackingPointSnapshots_TrackingPoints_StartPointId",
                table: "TrackingPointSnapshots");

            migrationBuilder.AddColumn<string>(
                name: "SnapshotId",
                table: "TrackingPoints",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TrackingPoints_SnapshotId",
                table: "TrackingPoints",
                column: "SnapshotId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrackingPoints_TrackingPointSnapshots_SnapshotId",
                table: "TrackingPoints",
                column: "SnapshotId",
                principalTable: "TrackingPointSnapshots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrackingPoints_TrackingPointSnapshots_SnapshotId",
                table: "TrackingPoints");

            migrationBuilder.DropIndex(
                name: "IX_TrackingPoints_SnapshotId",
                table: "TrackingPoints");

            migrationBuilder.DropColumn(
                name: "SnapshotId",
                table: "TrackingPoints");

            migrationBuilder.AddForeignKey(
                name: "FK_TrackingPointSnapshots_TrackingPoints_StartPointId",
                table: "TrackingPointSnapshots",
                column: "StartPointId",
                principalTable: "TrackingPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
