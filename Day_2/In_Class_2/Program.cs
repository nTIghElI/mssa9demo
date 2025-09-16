using System;
using System.Threading;
 
class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Starting background task...");
        var backgroundTask = DoBackgroundWorkAsync();
 
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"Main thread is free to do work...{i}");
            await Task.Delay(1000);
        }
 
        await backgroundTask;
        Console.WriteLine("All work done");
 
    }
 
    static async Task DoBackgroundWorkAsync()
    {
        Console.WriteLine("Background task started...");
        await Task.Delay(5000); // simulate async operation (I/O, network, ect.)
        Console.WriteLine("Background task finished");
    }
}
 