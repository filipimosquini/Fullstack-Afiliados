﻿// <auto-generated />
using System;
using Backend.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Backend.Infra.Migrations.Affiliate
{
    [DbContext(typeof(SellerContext))]
    [Migration("20230827004352_AlteraNomeTabelaAffiliate")]
    partial class AlteraNomeTabelaAffiliate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Backend.Core.Entities.FinancialTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AffiliateId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("TransactionTypeId")
                        .HasColumnType("int");

                    b.Property<double>("Value")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("AffiliateId");

                    b.HasIndex("ProductId");

                    b.HasIndex("TransactionTypeId");

                    b.ToTable("financial_transaction", (string)null);
                });

            modelBuilder.Entity("Backend.Core.Entities.FinancialTransactionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nature")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Signal")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("financial_transaction_type", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Venda produtor",
                            Nature = "Entrada",
                            Signal = "+"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Venda afiliado",
                            Nature = "Entrada",
                            Signal = "+"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Comissão paga",
                            Nature = "Saída",
                            Signal = "-"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Comissão recebida",
                            Nature = "Entrada",
                            Signal = "+"
                        });
                });

            modelBuilder.Entity("Backend.Core.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("product", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "CURSO DE BEM-ESTAR"
                        },
                        new
                        {
                            Id = 2,
                            Description = "DOMINANDO INVESTIMENTOS"
                        },
                        new
                        {
                            Id = 3,
                            Description = "DESENVOLVEDOR FULL STACK"
                        });
                });

            modelBuilder.Entity("Backend.Core.Entities.Seller", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("seller", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "CARLOS BATISTA"
                        },
                        new
                        {
                            Id = 2,
                            Name = "CELSO DE MELO"
                        },
                        new
                        {
                            Id = 3,
                            Name = "CAROLINA MACHADO"
                        },
                        new
                        {
                            Id = 4,
                            Name = "ELIANA NOGUEIRA"
                        },
                        new
                        {
                            Id = 5,
                            Name = "JOSE CARLOS"
                        },
                        new
                        {
                            Id = 6,
                            Name = "MARIA CANDIDA"
                        },
                        new
                        {
                            Id = 7,
                            Name = "THIAGO OLIVEIRA"
                        });
                });

            modelBuilder.Entity("Backend.Core.Entities.FinancialTransaction", b =>
                {
                    b.HasOne("Backend.Core.Entities.Seller", "Affiliate")
                        .WithMany("Transactions")
                        .HasForeignKey("AffiliateId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Backend.Core.Entities.Product", "Product")
                        .WithMany("Transactions")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Backend.Core.Entities.FinancialTransactionType", "TransactionType")
                        .WithMany("Transactions")
                        .HasForeignKey("TransactionTypeId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Affiliate");

                    b.Navigation("Product");

                    b.Navigation("TransactionType");
                });

            modelBuilder.Entity("Backend.Core.Entities.FinancialTransactionType", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Backend.Core.Entities.Product", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Backend.Core.Entities.Seller", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
