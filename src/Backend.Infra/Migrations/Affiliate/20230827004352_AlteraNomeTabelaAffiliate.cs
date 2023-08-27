using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Infra.Migrations.Affiliate
{
    public partial class AlteraNomeTabelaAffiliate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_financial_transaction_affiliate_AffiliateId",
                table: "financial_transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_affiliate",
                table: "affiliate");

            migrationBuilder.RenameTable(
                name: "affiliate",
                newName: "seller");

            migrationBuilder.AddPrimaryKey(
                name: "PK_seller",
                table: "seller",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_financial_transaction_seller_AffiliateId",
                table: "financial_transaction",
                column: "AffiliateId",
                principalTable: "seller",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_financial_transaction_seller_AffiliateId",
                table: "financial_transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_seller",
                table: "seller");

            migrationBuilder.RenameTable(
                name: "seller",
                newName: "affiliate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_affiliate",
                table: "affiliate",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_financial_transaction_affiliate_AffiliateId",
                table: "financial_transaction",
                column: "AffiliateId",
                principalTable: "affiliate",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
