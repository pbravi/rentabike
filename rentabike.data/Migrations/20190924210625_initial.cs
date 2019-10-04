using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace rentabike.data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FamilyPriceStrategies",
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
                    table.PrimaryKey("PK_FamilyPriceStrategies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RentalTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    IsComposite = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FamilyPriceStrategyRentalTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FamilyStrategyId = table.Column<int>(nullable: false),
                    FamilyPriceStrategyId = table.Column<int>(nullable: true),
                    RentalTypeId = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Discount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyPriceStrategyRentalTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FamilyPriceStrategyRentalTypes_FamilyPriceStrategies_FamilyPriceStrategyId",
                        column: x => x.FamilyPriceStrategyId,
                        principalTable: "FamilyPriceStrategies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FamilyPriceStrategyRentalTypes_RentalTypes_RentalTypeId",
                        column: x => x.RentalTypeId,
                        principalTable: "RentalTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RentalTypeId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    CompositeRentalId = table.Column<int>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    ParentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rentals_Rentals_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Rentals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rentals_Rentals_CompositeRentalId",
                        column: x => x.CompositeRentalId,
                        principalTable: "Rentals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rentals_RentalTypes_RentalTypeId",
                        column: x => x.RentalTypeId,
                        principalTable: "RentalTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RentalId = table.Column<int>(nullable: true),
                    Client = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Rentals_RentalId",
                        column: x => x.RentalId,
                        principalTable: "Rentals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "FamilyPriceStrategies",
                columns: new[] { "Id", "Description", "MaxCompositeSize", "MinCompositeSize" },
                values: new object[] { 1, "Family Strategy", 5, 3 });

            migrationBuilder.InsertData(
                table: "RentalTypes",
                columns: new[] { "Id", "Description", "IsComposite" },
                values: new object[,]
                {
                    { 1, "Rent by hour", false },
                    { 2, "Rent by day", false },
                    { 3, "Rent by week", false },
                    { 4, "Rent familiar group", true }
                });

            migrationBuilder.InsertData(
                table: "FamilyPriceStrategyRentalTypes",
                columns: new[] { "Id", "Discount", "FamilyPriceStrategyId", "FamilyStrategyId", "Price", "RentalTypeId" },
                values: new object[,]
                {
                    { 1, 0.0, null, 1, 5.0, 1 },
                    { 2, 0.0, null, 1, 20.0, 2 },
                    { 3, 0.0, null, 1, 60.0, 3 },
                    { 4, 30.0, null, 1, 0.0, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FamilyPriceStrategyRentalTypes_FamilyPriceStrategyId",
                table: "FamilyPriceStrategyRentalTypes",
                column: "FamilyPriceStrategyId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyPriceStrategyRentalTypes_RentalTypeId",
                table: "FamilyPriceStrategyRentalTypes",
                column: "RentalTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_RentalId",
                table: "Orders",
                column: "RentalId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_ParentId",
                table: "Rentals",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_CompositeRentalId",
                table: "Rentals",
                column: "CompositeRentalId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_RentalTypeId",
                table: "Rentals",
                column: "RentalTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FamilyPriceStrategyRentalTypes");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "FamilyPriceStrategies");

            migrationBuilder.DropTable(
                name: "Rentals");

            migrationBuilder.DropTable(
                name: "RentalTypes");
        }
    }
}
