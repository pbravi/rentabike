using Microsoft.EntityFrameworkCore.Migrations;

namespace rentabike.data.Migrations
{
    public partial class initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FamilyPriceStrategyRentalTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "Discount",
                value: 0.3);

            migrationBuilder.InsertData(
                table: "FamilyPriceStrategyRentalTypes",
                columns: new[] { "Id", "Discount", "Price", "PriceStrategyId", "RentalTypeId" },
                values: new object[] { 5, 0.0, 0.0, 1, 5 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FamilyPriceStrategyRentalTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "FamilyPriceStrategyRentalTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "Discount",
                value: 30.0);
        }
    }
}
