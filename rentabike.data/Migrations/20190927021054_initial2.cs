using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace rentabike.data.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FamilyPriceStrategyRentalTypes_FamilyPriceStrategies_FamilyPriceStrategyId",
                table: "FamilyPriceStrategyRentalTypes");

            migrationBuilder.DropTable(
                name: "FamilyPriceStrategies");

            migrationBuilder.DropIndex(
                name: "IX_FamilyPriceStrategyRentalTypes_FamilyPriceStrategyId",
                table: "FamilyPriceStrategyRentalTypes");

            migrationBuilder.DropColumn(
                name: "FamilyPriceStrategyId",
                table: "FamilyPriceStrategyRentalTypes");

            migrationBuilder.RenameColumn(
                name: "FamilyStrategyId",
                table: "FamilyPriceStrategyRentalTypes",
                newName: "PriceStrategyId");

            migrationBuilder.CreateTable(
                name: "PriceStrategy",
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
                    table.PrimaryKey("PK_PriceStrategy", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PriceStrategy",
                columns: new[] { "Id", "Description", "MaxCompositeSize", "MinCompositeSize" },
                values: new object[] { 1, "Family Strategy", 5, 3 });

            migrationBuilder.UpdateData(
                table: "RentalTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "Rent familiar group (30% discount)");

            migrationBuilder.InsertData(
                table: "RentalTypes",
                columns: new[] { "Id", "Description", "IsComposite" },
                values: new object[] { 5, "Rental group", true });

            migrationBuilder.CreateIndex(
                name: "IX_FamilyPriceStrategyRentalTypes_PriceStrategyId",
                table: "FamilyPriceStrategyRentalTypes",
                column: "PriceStrategyId");

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyPriceStrategyRentalTypes_PriceStrategy_PriceStrategyId",
                table: "FamilyPriceStrategyRentalTypes",
                column: "PriceStrategyId",
                principalTable: "PriceStrategy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FamilyPriceStrategyRentalTypes_PriceStrategy_PriceStrategyId",
                table: "FamilyPriceStrategyRentalTypes");

            migrationBuilder.DropTable(
                name: "PriceStrategy");

            migrationBuilder.DropIndex(
                name: "IX_FamilyPriceStrategyRentalTypes_PriceStrategyId",
                table: "FamilyPriceStrategyRentalTypes");

            migrationBuilder.DeleteData(
                table: "RentalTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.RenameColumn(
                name: "PriceStrategyId",
                table: "FamilyPriceStrategyRentalTypes",
                newName: "FamilyStrategyId");

            migrationBuilder.AddColumn<int>(
                name: "FamilyPriceStrategyId",
                table: "FamilyPriceStrategyRentalTypes",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FamilyPriceStrategies",
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
                    table.PrimaryKey("PK_FamilyPriceStrategies", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "FamilyPriceStrategies",
                columns: new[] { "Id", "Description", "MaxCompositeSize", "MinCompositeSize" },
                values: new object[] { 1, "Family Strategy", 5, 3 });

            migrationBuilder.UpdateData(
                table: "RentalTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "Rent familiar group");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyPriceStrategyRentalTypes_FamilyPriceStrategyId",
                table: "FamilyPriceStrategyRentalTypes",
                column: "FamilyPriceStrategyId");

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyPriceStrategyRentalTypes_FamilyPriceStrategies_FamilyPriceStrategyId",
                table: "FamilyPriceStrategyRentalTypes",
                column: "FamilyPriceStrategyId",
                principalTable: "FamilyPriceStrategies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
