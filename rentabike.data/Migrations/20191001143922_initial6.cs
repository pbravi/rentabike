using Microsoft.EntityFrameworkCore.Migrations;

namespace rentabike.data.Migrations
{
    public partial class initial6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompositeRentalId",
                table: "Rentals",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_CompositeRentalId",
                table: "Rentals",
                column: "CompositeRentalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Rentals_CompositeRentalId",
                table: "Rentals",
                column: "CompositeRentalId",
                principalTable: "Rentals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Rentals_CompositeRentalId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_CompositeRentalId",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "CompositeRentalId",
                table: "Rentals");
        }
    }
}
