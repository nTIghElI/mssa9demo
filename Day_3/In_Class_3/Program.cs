using System.IO;
using System.Text;
 
 
class Program
{
    public static void Main(string[] args)
    {
        string path = Path.Combine(Path.GetTempPath(), "sync-stream.txt");
 
        string fullText = $"This is the first line{Environment.NewLine}This is the second line";
 
        WriteFullText(path, fullText);
        ReadFullText(path);
        WriteLines(path);
        ReadLines(path);
 
 
    }
 
    static void WriteFullText(string filePath, string text)
    {
        using var writer = new StreamWriter(filePath, false, Encoding.Unicode);
        writer.Write(text);
    }
 
    static void ReadFullText(string filePath)
    {
        using var reader = new StreamReader(filePath, Encoding.Unicode);
        string content = reader.ReadToEnd();
        Console.WriteLine("Full text");
        Console.WriteLine(content);
    }
 
    static void WriteLines(string filePath)
    {
        using var writer = new StreamWriter(filePath, false, Encoding.Unicode);
        writer.WriteLine("Line 1");
        writer.WriteLine("Line 2");
    }
 
    static void ReadLines(string filePath)
    {
        using var reader = new StreamReader(filePath, Encoding.Unicode);
        string line;
        Console.WriteLine("Line by line");
        while ((line = reader.ReadLine()) != null)
        {
            Console.WriteLine(line);
        }
    }
}