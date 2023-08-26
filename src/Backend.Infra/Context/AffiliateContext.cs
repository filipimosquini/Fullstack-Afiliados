using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using Backend.Core.Entities;

namespace Backend.Infra.Context;
public class AffiliateContext : DbContext
{
    private readonly IConfiguration _configuration;

    public AffiliateContext(DbContextOptions<AffiliateContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<FinancialTransaction> FinancialTransactions { get; set; }
    public DbSet<FinancialTransactionType> FinancialTransactionTypes { get; set; }
    public DbSet<Affiliate> Affiliates { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseMySql(_configuration.GetConnectionString(GetType().Name),
                ServerVersion.AutoDetect(_configuration.GetConnectionString(GetType().Name)));

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AffiliateContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public async Task<bool> Commit()
    {
        if (await base.SaveChangesAsync() <= 0)
            return false;

        return true;
    }

    public bool DatabaseExists()
    {
        try
        {
            return Database.GetService<IRelationalDatabaseCreator>().Exists();
        }
        catch (DbException)
        {
            return false;
        }
    }

    public bool MigrateDatabase()
    {
        var idsDasMigrationJaExecutadas = this.GetService<IHistoryRepository>()
            .GetAppliedMigrations()
            .Select(m => m.MigrationId);

        var idsDeTodasAsMigrations = this.GetService<IMigrationsAssembly>()
            .Migrations
            .Select(m => m.Key);

        return !idsDeTodasAsMigrations.Except(idsDasMigrationJaExecutadas).Any();
    }

}