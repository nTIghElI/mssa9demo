using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
class Program
{
    public static async Task Main(string[] args)
    {
        string filePath = Path.Combine(Path.GetTempPath(), "async-filestream.txt");
        string text = $"This is the first line{Environment.NewLine}This is the second line";
 
        await WriteTextAsync(filePath, text);
        await ReadTextAsync(filePath);
    }
 
    static async Task WriteTextAsync(string filePath, string text)
    {
        byte[] encodedText = Encoding.Unicode.GetBytes(text);
        using var sourceStream = new FileStream(
            filePath,
            FileMode.Create, FileAccess.Write, FileShare.None,
            bufferSize: 4096, useAsync: true);
 
        await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
    }
 
    static async Task ReadTextAsync(string filePath)
    {
        using var sourceStream = new FileStream(
            filePath,
            FileMode.Open, FileAccess.Read, FileShare.Read,
            bufferSize: 4096, useAsync: true);
 
        byte[] buffer = new byte[sourceStream.Length];
        int bytesRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length);
        string decodedText = Encoding.Unicode.GetString(buffer, 0, bytesRead);
        Console.WriteLine(decodedText);
    }
 
}