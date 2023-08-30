using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Runtime.InteropServices;

namespace Backend.Infra.CrossCutting.Converters;

public static class ConvertBase64ToFormFile
{
    public static int MimeSampleSize = 256;

    public static string DefaultMimeType = "application/octet-stream";

    [DllImport("urlmon.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = false)]
    static extern int FindMimeFromData(IntPtr pBC,
        [MarshalAs(UnmanagedType.LPWStr)] string pwzUrl,
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeParamIndex = 3)]
        byte[] pBuffer,
        int cbSize,
        [MarshalAs(UnmanagedType.LPWStr)] string pwzMimeProposed,
        int dwMimeFlags,
        out IntPtr ppwzMimeOut,
        int dwReserved);

    /// <summary>
    /// Check the first 256 bytes to determine the MIME type. This function is often used when the file extention name is unknow.
    ///
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    /// <remarks>There's some limitation of the api function, according to http://msdn.microsoft.com/en-us/library/ms775147%28VS.85%29.aspx#Known_MimeTypes
    /// and this implementation will return "application/octet-stream" if the file is smaller than 256 bytes.
    /// </remarks>
    private unsafe static string GetMimeFromBytes(byte[] data)
    {
        if (data == null)
        {
            throw new ArgumentNullException("data", "Hey, data is null.");
        }

        IntPtr mimeTypePointer = IntPtr.Zero;
        try
        {

            FindMimeFromData(IntPtr.Zero, null, data, MimeSampleSize, null, 0, out mimeTypePointer, 0);
            var mime = Marshal.PtrToStringUni(mimeTypePointer);
            return mime ?? DefaultMimeType;
        }
        catch (AccessViolationException e)
        {
            Debug.WriteLine(e.ToString());
            return DefaultMimeType;
        }
        finally
        {
            if (mimeTypePointer != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(mimeTypePointer);
            }
        }
    }

    public static IFormFile ConvertToFormFile(string base64File, string name = "file", string fileName = "file")
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
            ContentType = GetMimeFromBytes(stream.ToArray())
        };

        return file;
    }
}