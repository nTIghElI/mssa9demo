using System.Net.Http;
using System.Threading.Tasks;
class Program
{
    static async Task Main(string[] args)
    {
        using var client = new HttpClient();
 
        Task<string> google = client.GetStringAsync("https://www.google.com");
        Task<string> bing = client.GetStringAsync("http://www.bing.com");
 
        string[] results = await Task.WhenAll(google, bing);
 
        Console.WriteLine($"Google response length: {results[0].Length}");
        Console.WriteLine($"Bing response length: {results[1].Length}");
    }
}