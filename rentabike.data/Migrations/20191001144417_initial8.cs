using Microsoft.EntityFrameworkCore.Migrations;

namespace rentabike.data.Migrations
{
    public partial class initial8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Rentals_CompositeRentalId1",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_CompositeRentalId1",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "CompositeRentalId1",
                table: "Rentals");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompositeRentalId1",
                table: "Rentals",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_CompositeRentalId1",
                table: "Rentals",
                column: "CompositeRentalId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Rentals_CompositeRentalId1",
                table: "Rentals",
                column: "CompositeRentalId1",
                principalTable: "Rentals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
