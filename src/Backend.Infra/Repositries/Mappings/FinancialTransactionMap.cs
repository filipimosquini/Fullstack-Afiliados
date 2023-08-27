using Backend.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Infra.Repositries.Mappings;

public class FinancialTransactionMap : IEntityTypeConfiguration<FinancialTransaction>
{
    public void Configure(EntityTypeBuilder<FinancialTransaction> builder)
    {
        builder.ToTable("financial_transaction");
        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.Product)
            .WithMany(x => x.FinancialTransaction)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasOne(x => x.Seller)
            .WithMany(x => x.FinancialTransaction)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasOne(x => x.FinancialTransactionType)
            .WithMany(x => x.FinancialTransaction)
            .OnDelete(DeleteBehavior.SetNull);
    }
}