using System.IO;
 
class Program
{
    public static void Main(string[] args)
    {
        string path = Path.Combine(Path.GetTempPath(), "sync-alltext.txt");
        File.WriteAllText(path, "Hello world");
 
        string content = File.ReadAllText(path);
        Console.WriteLine(content);
 
    }
}