using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace rentabike.data.Migrations
{
    public partial class initial5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FamilyPriceStrategyRentalTypes_PriceStrategy_PriceStrategyId",
                table: "FamilyPriceStrategyRentalTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Rentals_CompositeRentalId",
                table: "Rentals");

            migrationBuilder.DropTable(
                name: "PriceStrategy");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_CompositeRentalId",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "CompositeRentalId",
                table: "Rentals");

            migrationBuilder.CreateTable(
                name: "Strategy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    MinCompositeSize = table.Column<int>(nullable: false),
                    MaxCompositeSize = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Strategy", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Strategy",
                columns: new[] { "Id", "Description", "MaxCompositeSize", "MinCompositeSize" },
                values: new object[] { 1, "Family Strategy", 5, 3 });

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyPriceStrategyRentalTypes_Strategy_PriceStrategyId",
                table: "FamilyPriceStrategyRentalTypes",
                column: "PriceStrategyId",
                principalTable: "Strategy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FamilyPriceStrategyRentalTypes_Strategy_PriceStrategyId",
                table: "FamilyPriceStrategyRentalTypes");

            migrationBuilder.DropTable(
                name: "Strategy");

            migrationBuilder.AddColumn<int>(
                name: "CompositeRentalId",
                table: "Rentals",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PriceStrategy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    MaxCompositeSize = table.Column<int>(nullable: false),
                    MinCompositeSize = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceStrategy", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PriceStrategy",
                columns: new[] { "Id", "Description", "MaxCompositeSize", "MinCompositeSize" },
                values: new object[] { 1, "Family Strategy", 5, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_CompositeRentalId",
                table: "Rentals",
                column: "CompositeRentalId");

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyPriceStrategyRentalTypes_PriceStrategy_PriceStrategyId",
                table: "FamilyPriceStrategyRentalTypes",
                column: "PriceStrategyId",
                principalTable: "PriceStrategy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Rentals_CompositeRentalId",
                table: "Rentals",
                column: "CompositeRentalId",
                principalTable: "Rentals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
