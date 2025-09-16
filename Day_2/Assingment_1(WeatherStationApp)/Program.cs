using System;
using System.Threading;
using System.Threading.Tasks;

public class WeatherStation
{
    // This is the async method that simulates a sensor.
    public static async Task RunSensorAsync(string sensorName, CancellationToken token)
    {
        var random = new Random();

        // Loop 5 times to get 5 readings.
        for (int i = 1; i <= 5; i++)
        {
            // At the start of each loop, check if cancellation has been requested.
            token.ThrowIfCancellationRequested();

            try
            {
                // Wait for 1 second, passing the token.
                await Task.Delay(1000, token);

                int temp = random.Next(5, 25); // Generate a temp between 5 and 25
                Console.WriteLine($"[{sensorName}] Reading {i}: {temp}°C");
            }
            catch (TaskCanceledException)
            {
                // This allows the main catch block to handle the message.
                throw;
            }
        }
    }

    public static async Task Main()
    {
        // 1. Set up the CancellationTokenSource and the key press listener.
        using var cts = new CancellationTokenSource();
        Console.WriteLine("Press 'c' to cancel sensor readings...\n");
        _ = Task.Run(() =>
        {
            if (Console.ReadKey(true).KeyChar == 'c')
            {
                // This triggers the cancellation on the token.
                cts.Cancel();
            }
        });

        // 2. Create the three sensor tasks. We pass the token to each one.
        Task sensor1 = RunSensorAsync("Sensor1", cts.Token);
        Task sensor2 = RunSensorAsync("Sensor2", cts.Token);
        Task sensor3 = RunSensorAsync("Sensor3", cts.Token);

        // A simple loop to show that the main thread is not blocked while waiting.
        int workCounter = 0;
        while (!sensor1.IsCompleted || !sensor2.IsCompleted || !sensor3.IsCompleted)
        {
            Console.WriteLine($"Main thread is free to do work... {workCounter}");
            workCounter++;
            await Task.Delay(1000); // Wait a second before checking again
        }
        
        // 3. Wrap the await in a try...catch block to handle cancellation.
        try
        {
            // Task.WhenAll creates a single task that completes when all
            // of the provided tasks have completed.
            await Task.WhenAll(sensor1, sensor2, sensor3);
            Console.WriteLine("\nAll sensors finished.");
        }
        catch (OperationCanceledException)
        {
            // This block will execute if cts.Cancel() was called.
            Console.WriteLine("\nSensor readings were canceled.");
        }
    }
}

