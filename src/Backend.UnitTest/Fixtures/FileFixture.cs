using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace Backend.UnitTest.Fixtures;

public class FileFixtureCollection : ICollectionFixture<FileFixture> { }

public class FileFixture : IDisposable
{
    public IFormFile CreateFileWithoutErrors()
    {
        var path = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\net6.0\\", "\\Resources\\sales.txt");
        var fileBytes = File.ReadAllBytes(path);
        var stream = new MemoryStream(fileBytes);

        return new FormFile(baseStream: stream, baseStreamOffset: 0, length: stream.Length, name: "file", fileName: "file.txt")
        {
            Headers = new HeaderDictionary(),
            ContentType = "text/plain"
        };
    }

    public IFormFile CreateFileWithErrors(string contentType = "text/plain")
    {
        var path = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\net6.0\\", "\\Resources\\errors.txt");
        var fileBytes = File.ReadAllBytes(path);
        var stream = new MemoryStream(fileBytes);

        return new FormFile(baseStream: stream, baseStreamOffset: 0, length: stream.Length, name: "file", fileName: "file.txt")
        {
            Headers = new HeaderDictionary(),
            ContentType = contentType
        };
    }

    public IFormFile CreateEmptyFile()
    {
        var path = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\net6.0\\", "\\Resources\\empty.txt");
        var fileBytes = File.ReadAllBytes(path);
        var stream = new MemoryStream(fileBytes);

        return new FormFile(baseStream: stream, baseStreamOffset: 0, length: stream.Length, name: "file", fileName: "file.txt")
        {
            Headers = new HeaderDictionary(),
            ContentType = "text/plain"
        };
    }

    public IFormFile CreateEmptyLineFile()
    {
        var path = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\net6.0\\", "\\Resources\\empty_line.txt");
        var fileBytes = File.ReadAllBytes(path);
        var stream = new MemoryStream(fileBytes);

        return new FormFile(baseStream: stream, baseStreamOffset: 0, length: stream.Length, name: "file", fileName: "file.txt")
        {
            Headers = new HeaderDictionary(),
            ContentType = "text/plain"
        };
    }

    public void Dispose()
    {
    }
}