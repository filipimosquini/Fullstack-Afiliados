using Backend.Core.Entities;
using Bogus;

namespace Backend.UnitTest.Fixtures;

public class FinancialTransactionFixtureCollection : ICollectionFixture<FinancialTransactionFixture> { }

public class FinancialTransactionFixture : IDisposable
{

    public List<FinancialTransaction> List(int quantity = 3)
    {
        return new Faker<FinancialTransaction>()
            .CustomInstantiator(c => new FinancialTransaction()
            {
                Product = new Product(),
                Seller = new Seller(),
                FinancialTransactionType = new FinancialTransactionType(),
                Date = DateTime.Today.Date,
                Value = 1.0
            }).Generate(quantity);
    }

    public List<FinancialTransaction> ListWithErrors(int quantity = 3)
    {
        return new Faker<FinancialTransaction>()
            .CustomInstantiator(c => new FinancialTransaction()
            {
                Product = null,
                Seller = null,
                FinancialTransactionType = null,
                Date = DateTime.Today.Date,
                Value = -1.0
            }).Generate(quantity);
    }

    public FinancialTransaction CreateObjectWithoutErros()
    {
        return new FinancialTransaction()
        {
            Product = new Product(),
            Seller = new Seller(),
            FinancialTransactionType = new FinancialTransactionType(),
            Date = DateTime.Today.Date,
            Value = 1.0
        };
    }

    public FinancialTransaction CreateObjectWithErros()
    {
        return new FinancialTransaction()
        {
            Product = null,
            Seller = null,
            FinancialTransactionType = null,
            Date = DateTime.Today.Date,
            Value = -1.0
        };
    }

    public void Dispose()
    {
    }
}