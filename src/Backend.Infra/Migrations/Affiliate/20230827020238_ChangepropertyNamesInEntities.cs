using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Infra.Migrations.Affiliate
{
    public partial class ChangepropertyNamesInEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_financial_transaction_financial_transaction_type_Transaction~",
                table: "financial_transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_financial_transaction_seller_AffiliateId",
                table: "financial_transaction");

            migrationBuilder.RenameColumn(
                name: "TransactionTypeId",
                table: "financial_transaction",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "AffiliateId",
                table: "financial_transaction",
                newName: "FinancialTransactionTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_financial_transaction_TransactionTypeId",
                table: "financial_transaction",
                newName: "IX_financial_transaction_SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_financial_transaction_AffiliateId",
                table: "financial_transaction",
                newName: "IX_financial_transaction_FinancialTransactionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_financial_transaction_financial_transaction_type_FinancialTr~",
                table: "financial_transaction",
                column: "FinancialTransactionTypeId",
                principalTable: "financial_transaction_type",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_financial_transaction_seller_SellerId",
                table: "financial_transaction",
                column: "SellerId",
                principalTable: "seller",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_financial_transaction_financial_transaction_type_FinancialTr~",
                table: "financial_transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_financial_transaction_seller_SellerId",
                table: "financial_transaction");

            migrationBuilder.RenameColumn(
                name: "SellerId",
                table: "financial_transaction",
                newName: "TransactionTypeId");

            migrationBuilder.RenameColumn(
                name: "FinancialTransactionTypeId",
                table: "financial_transaction",
                newName: "AffiliateId");

            migrationBuilder.RenameIndex(
                name: "IX_financial_transaction_SellerId",
                table: "financial_transaction",
                newName: "IX_financial_transaction_TransactionTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_financial_transaction_FinancialTransactionTypeId",
                table: "financial_transaction",
                newName: "IX_financial_transaction_AffiliateId");

            migrationBuilder.AddForeignKey(
                name: "FK_financial_transaction_financial_transaction_type_Transaction~",
                table: "financial_transaction",
                column: "TransactionTypeId",
                principalTable: "financial_transaction_type",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_financial_transaction_seller_AffiliateId",
                table: "financial_transaction",
                column: "AffiliateId",
                principalTable: "seller",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
