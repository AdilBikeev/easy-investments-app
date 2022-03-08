using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quotation.Infrastructure.Migrations
{
    public partial class Initial_AddForeignKeyQuotation_v002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Quotation",
                schema: "quotation",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "FIGI",
                schema: "quotation",
                table: "QuotationProfit");

            migrationBuilder.DropColumn(
                name: "FullName",
                schema: "quotation",
                table: "QuotationProfit");

            migrationBuilder.DropColumn(
                name: "Ticker",
                schema: "quotation",
                table: "QuotationProfit");

            migrationBuilder.AddColumn<int>(
                name: "QuotationId",
                schema: "quotation",
                table: "QuotationProfit",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quotation",
                schema: "quotation",
                table: "Quotation",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationProfit_QuotationId",
                schema: "quotation",
                table: "QuotationProfit",
                column: "QuotationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_Name_FIGI_Ticker",
                schema: "quotation",
                table: "Quotation",
                columns: new[] { "Name", "FIGI", "Ticker" });

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationProfit_Quotation_QuotationId",
                schema: "quotation",
                table: "QuotationProfit",
                column: "QuotationId",
                principalSchema: "quotation",
                principalTable: "Quotation",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuotationProfit_Quotation_QuotationId",
                schema: "quotation",
                table: "QuotationProfit");

            migrationBuilder.DropIndex(
                name: "IX_QuotationProfit_QuotationId",
                schema: "quotation",
                table: "QuotationProfit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Quotation",
                schema: "quotation",
                table: "Quotation");

            migrationBuilder.DropIndex(
                name: "IX_Quotation_Name_FIGI_Ticker",
                schema: "quotation",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "QuotationId",
                schema: "quotation",
                table: "QuotationProfit");

            migrationBuilder.AddColumn<string>(
                name: "FIGI",
                schema: "quotation",
                table: "QuotationProfit",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                schema: "quotation",
                table: "QuotationProfit",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Ticker",
                schema: "quotation",
                table: "QuotationProfit",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quotation",
                schema: "quotation",
                table: "Quotation",
                columns: new[] { "Id", "Name", "FIGI", "Ticker" });
        }
    }
}
