using Backend.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Infra.Repositries.Mappings;

public class FinancialTransactionTypeMap : IEntityTypeConfiguration<FinancialTransactionType>
{
    public void Configure(EntityTypeBuilder<FinancialTransactionType> builder)
    {
        builder.ToTable("financial_transaction_type");
        builder.HasKey(x => x.Id);

        builder.HasData(Seed());
    }

    private static ICollection<FinancialTransactionType> Seed()
    {
        return new[]
        {
            new FinancialTransactionType { Id = 1, Description = "Venda produtor", Nature = "Entrada", Signal = "+" },
            new FinancialTransactionType { Id = 2, Description = "Venda afiliado", Nature = "Entrada", Signal = "+" },
            new FinancialTransactionType { Id = 3, Description = "Comissão paga", Nature = "Saída", Signal = "-" },
            new FinancialTransactionType { Id = 4, Description = "Comissão recebida", Nature = "Entrada", Signal = "+" },
        };
    }
}