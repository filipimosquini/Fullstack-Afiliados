namespace Backend.Core.Services.DataTransferObjects;

public class ImportedTransactionsDto
{
    public int Id { get; set; }
    public string SellerName { get; set; }
    public double Total { get; set; }
    public IEnumerable<ImportedTransactionsDetailsDto> Details { get; set; }
}