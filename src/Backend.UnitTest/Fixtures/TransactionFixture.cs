using Backend.Core.Services.DataTransferObjects;
using Bogus;

namespace Backend.UnitTest.Fixtures;

public class TransactionFixture : IDisposable
{
    public List<TransactionDto> List(int quantity = 3)
    {
        return new Faker<TransactionDto>()
            .CustomInstantiator(c => new TransactionDto()
            {
                Date = DateTime.Today.Date,
                FinancialTransactionType = 1,
                Value = 1.0,
                Seller = "SELLER",
                Product = "PRODUCT"
            })
            .Generate(quantity);
    }

    public TransactionDto Create()
    {
        return new Faker<TransactionDto>()
            .CustomInstantiator(c => new TransactionDto()
            {
                Date = DateTime.Today.Date,
                FinancialTransactionType = 1,
                Value = 1.0,
                Seller = "SELLER",
                Product = "PRODUCT"
            })
            .Generate();
    }

    public void Dispose()
    {
    }
}