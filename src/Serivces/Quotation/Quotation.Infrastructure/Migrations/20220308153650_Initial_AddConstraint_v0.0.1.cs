using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quotation.Infrastructure.Migrations
{
    public partial class Initial_AddConstraint_v001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Quotation",
                schema: "quotation",
                table: "Quotation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quotation",
                schema: "quotation",
                table: "Quotation",
                columns: new[] { "Id", "Name", "FIGI", "Ticker" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Quotation",
                schema: "quotation",
                table: "Quotation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quotation",
                schema: "quotation",
                table: "Quotation",
                column: "Id");
        }
    }
}
