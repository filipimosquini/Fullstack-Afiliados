using Microsoft.AspNetCore.Http;

namespace Backend.UnitTest.Fixtures;

public class FileFixtureCollection : ICollectionFixture<FileFixture> { }

public class FileFixture : IDisposable
{
    public string CreateEncodedFileWithoutErrors()
    {
        var path = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\net6.0\\", "\\Resources\\sales.txt");
        var fileBytes = File.ReadAllBytes(path);
        var stream = new MemoryStream(fileBytes);

        IFormFile file = new FormFile(baseStream: stream, baseStreamOffset: 0, length: stream.Length, name: "file", fileName: "file.txt")
        {
            Headers = new HeaderDictionary(),
        };

        stream = new MemoryStream();
        file.CopyTo(stream);
        return Convert.ToBase64String(stream.ToArray());
    }

    public string CreateEncodedFileWithErrors()
    {
        var path = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\net6.0\\", "\\Resources\\errors.txt");
        var fileBytes = File.ReadAllBytes(path);
        var stream = new MemoryStream(fileBytes);

        var file = new FormFile(baseStream: stream, baseStreamOffset: 0, length: stream.Length, name: "file", fileName: "file.txt")
        {
            Headers = new HeaderDictionary(),
        };

        stream = new MemoryStream();
        file.CopyTo(stream);
        return Convert.ToBase64String(stream.ToArray());
    }

    public string CreateEncodedPdfFile()
    {
        var path = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\net6.0\\", "\\Resources\\test.pdf");
        var fileBytes = File.ReadAllBytes(path);
        var stream = new MemoryStream(fileBytes);

        var file = new FormFile(baseStream: stream, baseStreamOffset: 0, length: stream.Length, name: "file", fileName: "file.pdf")
        {
            Headers = new HeaderDictionary(),
        };

        stream = new MemoryStream();
        file.CopyTo(stream);
        return Convert.ToBase64String(stream.ToArray());
    }

    public string CreateEncodedFileEmpty()
    {
        var path = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\net6.0\\", "\\Resources\\empty.txt");
        var fileBytes = File.ReadAllBytes(path);
        var stream = new MemoryStream(fileBytes);

        var file =  new FormFile(baseStream: stream, baseStreamOffset: 0, length: stream.Length, name: "file", fileName: "file.txt")
        {
            Headers = new HeaderDictionary(),
        };

        stream = new MemoryStream();
        file.CopyTo(stream);
        return Convert.ToBase64String(stream.ToArray());
    }

    public string CreateEncodedFileWithEmptyLines()
    {
        var path = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\net6.0\\", "\\Resources\\empty_line.txt");
        var fileBytes = File.ReadAllBytes(path);
        var stream = new MemoryStream(fileBytes);

        var file =  new FormFile(baseStream: stream, baseStreamOffset: 0, length: stream.Length, name: "file", fileName: "file.txt")
        {
            Headers = new HeaderDictionary(),
        };

        stream = new MemoryStream();
        file.CopyTo(stream);
        return Convert.ToBase64String(stream.ToArray());
    }

    public void Dispose()
    {
    }
}