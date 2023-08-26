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
            .WithMany(x => x.Transactions)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasOne(x => x.Affiliate)
            .WithMany(x => x.Transactions)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasOne(x => x.TransactionType)
            .WithMany(x => x.Transactions)
            .OnDelete(DeleteBehavior.SetNull);
    }
}