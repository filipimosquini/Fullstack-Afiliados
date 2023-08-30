using Backend.Core.Bases.Interfaces;
using Backend.Core.Entities;
using Backend.Core.Services;
using Backend.Core.Services.Contracts.Business;
using Backend.Core.Services.Contracts.ViewModels;
using Backend.Core.Services.Interfaces;
using Backend.Core.Services.ViewModels;
using Backend.Infra.CrossCutting.Converters;
using Backend.UnitTest.Fixtures;
using FluentAssertions;
using FluentValidation.TestHelper;
using KellermanSoftware.CompareNetObjects;
using Moq;
using Moq.AutoMock;

namespace Backend.UnitTest.Services;

[Trait("Service Tests", "Financial transaction service Test")]
public class FinancialTransactionServiceTest : IClassFixture<FileFixture>, IClassFixture<TransactionFixture>, IClassFixture<FinancialTransactionFixture>
{
    private readonly FileFixture _fileFixture;
    private readonly TransactionFixture _transactionFixture;
    private readonly FinancialTransactionFixture _financialTransactionFixture;

    private readonly CompareLogic _comparator = new CompareLogic();

    public FinancialTransactionServiceTest(FileFixture fileFixture, TransactionFixture transactionFixture, FinancialTransactionFixture financialTransactionFixture)
    {
        _fileFixture = fileFixture;
        _transactionFixture = transactionFixture;
        _financialTransactionFixture = financialTransactionFixture;
    }

    [Fact(DisplayName = "Should import file with transactions when the informations is valid")]
    public async Task Should_ImportFileWithTransactions_When_TheInformationsIsValid()
    {
        // Arrange
        var mocker = new AutoMocker();
        var fileService = mocker.GetMock<IFileService>();
        var unitOfWork = mocker.GetMock<IUnitOfWork>();
        var businessContract = new FinancialTransactionBusinessContract();
        var viewModelContract = new FinancialTransactionImportFileViewModelContract();
        var validateFileStructureBusinessContract = new FinancialTransactionFileContentBusinessContract();

        var viewModel = new FinancialTransactionImportFileViewModel()
        {
            EncodedFile = _fileFixture.CreateEncodedFileWithoutErrors()
        };

        var file = ConvertBase64ToFormFile.ConvertToFormFile(viewModel.EncodedFile);

        var transactionsDto = _transactionFixture.List();
        fileService
            .Setup(s => s.ExtractDataFromFileAsync(file))
            .ReturnsAsync(transactionsDto);

        var service = new Mock<FinancialTransactionService>(unitOfWork.Object, fileService.Object, businessContract, viewModelContract, validateFileStructureBusinessContract);

        var transactions = _financialTransactionFixture.List();
        service
            .Setup(s => s.CreateFinancialTransactionsAsync(transactionsDto))
            .ReturnsAsync(transactions);

        service
            .Setup(s => s.ValidateFinancialTransactionsAsync(transactions))
            .ReturnsAsync(true);

        unitOfWork
            .Setup(s => s.FinancialTransactionRepository.AddRangeAsync(transactions))
            .Returns(Task.FromResult(transactions));

        unitOfWork
            .Setup(s => s.SaveAsync())
            .Returns(Task.FromResult(true));

        // Act
        var validationModel = viewModelContract.TestValidate(viewModel);
        var validateFileContent= await validateFileStructureBusinessContract.ValidateAsync(file);
        var result = await service.Object.ImportFinancialTransactionFileAsync(viewModel);

        // Assert
        validationModel.IsValid.Should().Be(true);
        validateFileContent.IsValid.Should().Be(true);
        _comparator.Compare(true, result.IsValid).AreEqual.Should().Be(true);

        mocker.Verify();
    }

    [Fact(DisplayName = "Should not import file with transactions when file has errors")]
    public async Task Should_NotImportFileWithTransactions_When_FileHasErrors()
    {
        // Arrange
        var mocker = new AutoMocker();
        var fileService = mocker.GetMock<IFileService>();
        var unitOfWork = mocker.GetMock<IUnitOfWork>();
        var businessContract = new FinancialTransactionBusinessContract();
        var viewModelContract = new FinancialTransactionImportFileViewModelContract();
        var validateFileContentBusinessContract = new FinancialTransactionFileContentBusinessContract();

        var viewModel = new FinancialTransactionImportFileViewModel()
        {
            EncodedFile = _fileFixture.CreateEncodedFileEmpty()
        };

        var service = new Mock<FinancialTransactionService>(unitOfWork.Object, fileService.Object, businessContract, viewModelContract, validateFileContentBusinessContract);

        // Act
        var result = await service.Object.ImportFinancialTransactionFileAsync(viewModel);

        // Assert
        result.Errors.Should().HaveCountGreaterThan(0);

        mocker.Verify();
    }

    [Fact(DisplayName = "Should not import file with transactions when file content has errors")]
    public async Task Should_ImportFileWithTransactions_When_FileContentHasErrors()
    {
        // Arrange
        var mocker = new AutoMocker();
        var fileService = mocker.GetMock<IFileService>();
        var unitOfWork = mocker.GetMock<IUnitOfWork>();
        var businessContract = new FinancialTransactionBusinessContract();
        var viewModelContract = new FinancialTransactionImportFileViewModelContract();
        var validateFileContentBusinessContract = new FinancialTransactionFileContentBusinessContract();

        var viewModel = new FinancialTransactionImportFileViewModel()
        {
            EncodedFile = _fileFixture.CreateEncodedFileWithErrors()
        };

        var service = new Mock<FinancialTransactionService>(unitOfWork.Object, fileService.Object, businessContract, viewModelContract, validateFileContentBusinessContract);

        // Act
        var result = await service.Object.ImportFinancialTransactionFileAsync(viewModel);

        // Assert
        result.Errors.Should().HaveCountGreaterThan(0);

        mocker.Verify();
    }

    [Fact(DisplayName = "Should not import file with transactions when the extracted transactions is invalid")]
    public async Task Should_NoImportFileWithTransactions_When_ExtractedTransactionsIsInvalid()
    {
        // Arrange
        var mocker = new AutoMocker();
        var fileService = mocker.GetMock<IFileService>();
        var unitOfWork = mocker.GetMock<IUnitOfWork>();
        var businessContract = new FinancialTransactionBusinessContract();
        var viewModelContract = new FinancialTransactionImportFileViewModelContract();
        var validateFileContentBusinessContract = new FinancialTransactionFileContentBusinessContract();

        var viewModel = new FinancialTransactionImportFileViewModel()
        {
            EncodedFile = _fileFixture.CreateEncodedFileWithoutErrors()
        };

        var service = new Mock<FinancialTransactionService>(unitOfWork.Object, fileService.Object, businessContract, viewModelContract, validateFileContentBusinessContract);

        var transactions = _financialTransactionFixture.List();
        service
            .Setup(s => s.ValidateFinancialTransactionsAsync(transactions))
            .ReturnsAsync(false);

        // Act
        var result = await service.Object.ImportFinancialTransactionFileAsync(viewModel);

        // Assert
        result.Errors.Should().HaveCountGreaterOrEqualTo(0);

        mocker.Verify();
    }

    [Fact(DisplayName = "Should create financial transactions when the extracted transactions is valid")]
    public async Task Should_CreateFinancialTransactions_When_ExtractedTransactionsIsValid()
    {
        // Arrange
        var mocker = new AutoMocker();
        var unitOfWork = new Mock<IUnitOfWork>();
        var fileService = new Mock<IFileService>();
        var businessContract = new FinancialTransactionBusinessContract();
        var importFileContract = new FinancialTransactionImportFileViewModelContract();
        var fileContentContract = new FinancialTransactionFileContentBusinessContract();

        var service = new FinancialTransactionService(unitOfWork.Object, fileService.Object, businessContract, importFileContract, fileContentContract);

        var transactionsDto = _transactionFixture.List();
        var transaction = _transactionFixture.Create();

        unitOfWork
            .Setup(s => s.ProductRepository.FindByDescription(transaction.Product))
            .ReturnsAsync(It.IsAny<Product>());

        unitOfWork
            .Setup(s => s.SellerRepository.FindByName(transaction.Seller))
            .ReturnsAsync(It.IsAny<Seller>());

        unitOfWork
            .Setup(s => s.FinancialTransactionTypeRepository.FindById(transaction.FinancialTransactionType))
            .ReturnsAsync(It.IsAny<FinancialTransactionType>());

        // Act
        var result = await service.CreateFinancialTransactionsAsync(transactionsDto);

        // Assert
        result.Should().HaveCountGreaterThan(0);

        mocker.Verify();
    }

    [Fact(DisplayName = "Should validate financial transactions when the transactions is valid")]
    public async Task Should_ValidateFinancialTransactions_When_TransactionsIsValid()
    {
        // Arrange
        var mocker = new AutoMocker();
        var unitOfWork = new Mock<IUnitOfWork>();
        var fileService = new Mock<IFileService>();
        var businessContract = new FinancialTransactionBusinessContract();
        var importFileContract = new FinancialTransactionImportFileViewModelContract();
        var fileContentContract = new FinancialTransactionFileContentBusinessContract();

        var service = new FinancialTransactionService(unitOfWork.Object, fileService.Object, businessContract, importFileContract, fileContentContract);
        
        var financialTransactions = _financialTransactionFixture.List();

        // Act
        var result = await service.ValidateFinancialTransactionsAsync(financialTransactions);

        // Assert
        result.Should().Be(true);

        mocker.Verify();
    }

    [Fact(DisplayName = "Should not validate financial transactions when the transactions is invalid")]
    public async Task Should_NotValidateFinancialTransactions_When_TransactionsIsInValid()
    {
        // Arrange
        var mocker = new AutoMocker();
        var unitOfWork = new Mock<IUnitOfWork>();
        var fileService = new Mock<IFileService>();
        var businessContract = new FinancialTransactionBusinessContract();
        var importFileContract = new FinancialTransactionImportFileViewModelContract();
        var fileContentContract = new FinancialTransactionFileContentBusinessContract();

        var service = new FinancialTransactionService(unitOfWork.Object, fileService.Object, businessContract, importFileContract, fileContentContract);

        var financialTransactions = _financialTransactionFixture.ListWithErrors();

        // Act
        var result = await service.ValidateFinancialTransactionsAsync(financialTransactions);

        // Assert
        result.Should().Be(false);

        mocker.Verify();
    }
}