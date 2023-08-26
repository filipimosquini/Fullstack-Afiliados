using Backend.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Infra.Repositries.Mappings;

public class AffiliateMap : IEntityTypeConfiguration<Affiliate>
{
    public void Configure(EntityTypeBuilder<Affiliate> builder)
    {
        builder.ToTable("affiliate");
        builder.HasKey(x => x.Id);

        builder.HasData(Seed());
    }

    private static ICollection<Affiliate> Seed()
    {
        return new[]
        {
            new Affiliate { Id = 1, Name = "CARLOS BATISTA" },
            new Affiliate { Id = 2, Name = "CELSO DE MELO" },
            new Affiliate { Id = 3, Name = "CAROLINA MACHADO" },
            new Affiliate { Id = 4, Name = "ELIANA NOGUEIRA" },
            new Affiliate { Id = 5, Name = "JOSE CARLOS" },
            new Affiliate { Id = 6, Name = "MARIA CANDIDA" },
            new Affiliate { Id = 7, Name = "THIAGO OLIVEIRA" },
        };
    }
}