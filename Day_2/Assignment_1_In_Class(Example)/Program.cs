using System.Threading.Tasks;
class Program
{
    static async Task Main(string[] args)
    {
        using var cts = new CancellationTokenSource();
 
        Console.WriteLine("Press 'c' to cancel the operation...\n");
 
        _ = Task.Run(() =>
        {
            if (Console.ReadKey(true).KeyChar == 'c')
            {
                cts.Cancel();
                Console.WriteLine("\nCancellation requested");
            }
        });
 
        var sensor1 = ReadSensorAsync("Sensor1", cts.Token);
        var sensor2 = ReadSensorAsync("Sensor2", cts.Token);
        var sensor3 = ReadSensorAsync("Sensor3", cts.Token);
 
        try
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Main thread is free to do work... {i}");
                await Task.Delay(1000, cts.Token);
            }
 
            await Task.WhenAll(sensor1, sensor2, sensor3);
            Console.WriteLine("\nAll sensors finished");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("\nOperation cancelled");
        }
 
    }
 
    static async Task ReadSensorAsync(string name, CancellationToken token)
    {
        var random = new Random();
 
        for (int i = 1; i <= 5; i++)
        {            
            await Task.Delay(1000, token);
            int temperature = random.Next(0, 140);
            Console.WriteLine($"[{name}] reading {i}: {temperature}F");
        }
    }
}
 