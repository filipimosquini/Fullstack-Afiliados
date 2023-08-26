using Backend.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Infra.Repositries.Mappings;

public class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("product");
        builder.HasKey(x => x.Id);

        builder.HasData(Seed());
    }

    private static ICollection<Product> Seed()
    {
        return new[]
        {
            new Product { Id = 1, Description = "CURSO DE BEM-ESTAR" },
            new Product { Id = 2, Description = "DOMINANDO INVESTIMENTOS" },
            new Product { Id = 3, Description = "DESENVOLVEDOR FULL STACK" },
        };
    }
}