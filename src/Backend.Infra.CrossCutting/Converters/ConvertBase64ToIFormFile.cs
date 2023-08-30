using Microsoft.AspNetCore.Http;

namespace Backend.Infra.CrossCutting.Converters;

public static class ConvertBase64ToFormFile
{
    public static IFormFile ConvertToFormFile(string base64File, string contentType, string name = "file", string fileName = "file")
    {
        if (string.IsNullOrWhiteSpace(base64File))
        {
            return null;
        }

        byte[] bytes = Convert.FromBase64String(base64File);
        MemoryStream stream = new MemoryStream(bytes);

        IFormFile file = new FormFile(stream, 0, bytes.Length, name, fileName)
        {
            Headers = new HeaderDictionary(),
            ContentType = contentType
        };

        return file;
    }
}