using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Infra.Migrations.Affiliate
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "affiliate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_affiliate", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "financial_transaction_type",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nature = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Signal = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_financial_transaction_type", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "financial_transaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Value = table.Column<double>(type: "double", nullable: false),
                    AffiliateId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    TransactionTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_financial_transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_financial_transaction_affiliate_AffiliateId",
                        column: x => x.AffiliateId,
                        principalTable: "affiliate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_financial_transaction_financial_transaction_type_Transaction~",
                        column: x => x.TransactionTypeId,
                        principalTable: "financial_transaction_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_financial_transaction_product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "affiliate",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "CARLOS BATISTA" },
                    { 2, "CELSO DE MELO" },
                    { 3, "CAROLINA MACHADO" },
                    { 4, "ELIANA NOGUEIRA" },
                    { 5, "JOSE CARLOS" },
                    { 6, "MARIA CANDIDA" },
                    { 7, "THIAGO OLIVEIRA" }
                });

            migrationBuilder.InsertData(
                table: "financial_transaction_type",
                columns: new[] { "Id", "Description", "Nature", "Signal" },
                values: new object[,]
                {
                    { 1, "Venda produtor", "Entrada", "+" },
                    { 2, "Venda afiliado", "Entrada", "+" },
                    { 3, "Comissão paga", "Saída", "-" },
                    { 4, "Comissão recebida", "Entrada", "+" }
                });

            migrationBuilder.InsertData(
                table: "product",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1, "CURSO DE BEM-ESTAR" },
                    { 2, "DOMINANDO INVESTIMENTOS" },
                    { 3, "DESENVOLVEDOR FULL STACK" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_financial_transaction_AffiliateId",
                table: "financial_transaction",
                column: "AffiliateId");

            migrationBuilder.CreateIndex(
                name: "IX_financial_transaction_ProductId",
                table: "financial_transaction",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_financial_transaction_TransactionTypeId",
                table: "financial_transaction",
                column: "TransactionTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "financial_transaction");

            migrationBuilder.DropTable(
                name: "affiliate");

            migrationBuilder.DropTable(
                name: "financial_transaction_type");

            migrationBuilder.DropTable(
                name: "product");
        }
    }
}
