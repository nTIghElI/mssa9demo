using System.Net.Http;
using System.Threading.Tasks;
class Program
{
    static async Task Main(string[] args)
    {



        try
        {
            await DownloadDataAsync(cts.Token);
            Console.WriteLine("Download completed successfully");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Download was cancelled");
        }
 
    }
 
    static async Task DownloadDataAsync(CancellationToken token)
    {
        Console.WriteLine("Starting download...");
        for (int i = 1; i <= 5; i++)
        {
            // simulating download
            await Task.Delay(1000, token);
            Console.WriteLine($"Downloaded chunk {i}/5");
        }
    }
}