using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quotation.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "quotation");

            migrationBuilder.CreateSequence(
                name: "quotationprofitseq",
                schema: "quotation",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "quotationseq",
                schema: "quotation",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Quotation",
                schema: "quotation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    FIGI = table.Column<string>(type: "text", nullable: false),
                    Ticker = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuotationProfit",
                schema: "quotation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    FIGI = table.Column<string>(type: "text", nullable: false),
                    Ticker = table.Column<string>(type: "text", nullable: false),
                    InvestedAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    CountBuyQuotationPossible = table.Column<int>(type: "integer", nullable: false),
                    PriceAvg = table.Column<decimal>(type: "numeric", nullable: false),
                    QuantityPaymentsAvg = table.Column<decimal>(type: "numeric", nullable: false),
                    PayoutAvg = table.Column<decimal>(type: "numeric", nullable: false),
                    PayoutsYieldAvg = table.Column<decimal>(type: "numeric", nullable: false),
                    PossibleProfitSpeculation = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationProfit", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quotation",
                schema: "quotation");

            migrationBuilder.DropTable(
                name: "QuotationProfit",
                schema: "quotation");

            migrationBuilder.DropSequence(
                name: "quotationprofitseq",
                schema: "quotation");

            migrationBuilder.DropSequence(
                name: "quotationseq",
                schema: "quotation");
        }
    }
}
