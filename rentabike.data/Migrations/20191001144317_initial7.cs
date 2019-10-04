using Microsoft.EntityFrameworkCore.Migrations;

namespace rentabike.data.Migrations
{
    public partial class initial7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Rentals_ParentId",
                table: "Rentals");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "Rentals",
                newName: "CompositeRentalId1");

            migrationBuilder.RenameIndex(
                name: "IX_Rentals_ParentId",
                table: "Rentals",
                newName: "IX_Rentals_CompositeRentalId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Rentals_CompositeRentalId1",
                table: "Rentals",
                column: "CompositeRentalId1",
                principalTable: "Rentals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Rentals_CompositeRentalId1",
                table: "Rentals");

            migrationBuilder.RenameColumn(
                name: "CompositeRentalId1",
                table: "Rentals",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Rentals_CompositeRentalId1",
                table: "Rentals",
                newName: "IX_Rentals_ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Rentals_ParentId",
                table: "Rentals",
                column: "ParentId",
                principalTable: "Rentals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
