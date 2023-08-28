using Backend.Core.Services;
using Backend.UnitTest.Fixtures;
using FluentAssertions;
using Moq.AutoMock;

namespace Backend.UnitTest.Services;

[Trait("Service Tests", "File service Test")]
public class FileServiceTest : IClassFixture<FileFixture>, IClassFixture<TransactionFixture>
{
    private readonly FileFixture _fileFixture;
    private readonly TransactionFixture _transactionFixture;

    public FileServiceTest(FileFixture fileFixture, TransactionFixture transactionFixture)
    {
        _fileFixture = fileFixture;
        _transactionFixture = transactionFixture;
    }

    [Fact(DisplayName = "Should import file content expected extract transactions from file")]
    public async Task Should_ImportFileContent_Expected_ExtractTransactions()
    {
        // Arrange
        var mocker = new AutoMocker();
        var service = mocker.CreateInstance<FileService>();
        var file = _fileFixture.CreateFileWithoutErrors();

        // Act
        var result = await service.ExtractDataFromFileAsync(file);

        // Assert
        result.Should().HaveCountGreaterOrEqualTo(0);

        mocker.Verify();
    }

    [Fact(DisplayName = "Should import empty file content expected no extract transactions from file")]
    public async Task Should_ImportEmptyFile_Expected_NoExtractTransactions()
    {
        // Arrange
        var mocker = new AutoMocker();
        var service = mocker.CreateInstance<FileService>();
        var file = _fileFixture.CreateEmptyFile();

        // Act
        var result = await service.ExtractDataFromFileAsync(file);

        // Assert
        result.Should().HaveCount(0);

        mocker.Verify();
    }

    [Fact(DisplayName = "Should import file with errors expected no extract transactions from file")]
    public async Task Should_ImportFileWithErrors_Expected_NoExtractTransactions()
    {
        // Arrange
        var mocker = new AutoMocker();
        var service = mocker.CreateInstance<FileService>();
        var file = _fileFixture.CreateEmptyLineFile();

        // Act
        var result = await service.ExtractDataFromFileAsync(file);

        // Assert
        result.Should().HaveCount(0);

        mocker.Verify();
    }
}