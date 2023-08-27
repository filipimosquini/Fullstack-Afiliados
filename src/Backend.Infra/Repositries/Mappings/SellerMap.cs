using Backend.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Infra.Repositries.Mappings;

public class SellerMap : IEntityTypeConfiguration<Seller>
{
    public void Configure(EntityTypeBuilder<Seller> builder)
    {
        builder.ToTable("affiliate");
        builder.HasKey(x => x.Id);

        builder.HasData(Seed());
    }

    private static ICollection<Seller> Seed()
    {
        return new[]
        {
            new Seller { Id = 1, Name = "CARLOS BATISTA" },
            new Seller { Id = 2, Name = "CELSO DE MELO" },
            new Seller { Id = 3, Name = "CAROLINA MACHADO" },
            new Seller { Id = 4, Name = "ELIANA NOGUEIRA" },
            new Seller { Id = 5, Name = "JOSE CARLOS" },
            new Seller { Id = 6, Name = "MARIA CANDIDA" },
            new Seller { Id = 7, Name = "THIAGO OLIVEIRA" },
        };
    }
}