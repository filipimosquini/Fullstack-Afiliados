using Backend.Core.Services.DataTransferObjects;

namespace Backend.Core.Converters;

public static class FinancialTransactionConverter
{
    public static IEnumerable<TransactionDto> ConvertToTransactionsDto(this IEnumerable<string> lines)
    {
        if (!lines.Any())
        {
            return new List<TransactionDto>();
        }

        return lines.Select(x => x.ConvertToTransactionDto());
    }

    public static TransactionDto ConvertToTransactionDto(this string line)
    {
        return new TransactionDto
        {
            FinancialTransactionType = Int32.Parse(line.Substring(0, 1).TrimEnd()),
            Date = DateTime.Parse(line.Substring(1, 25).TrimEnd()),
            Product = line.Substring(26, 30).TrimEnd(),
            Value = double.Parse(line.Substring(56, 10).TrimEnd()),
            Seller = line.Substring(66, line.Length - 66).TrimEnd()
        };
    }
}