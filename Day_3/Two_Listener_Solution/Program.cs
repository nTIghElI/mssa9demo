using System;
using System.Threading.Tasks;


// The "fire alarm button"
using var cts = new CancellationTokenSource();

Console.WriteLine("Press 'c' to cancel the operation...\n");

// A background task that just waits to push the button
_ = Task.Run(() =>
{
    if (Console.ReadKey(true).KeyChar == 'c')
    {
        cts.Cancel(); // PUSH THE BUTTON!
    }
});

static async Task DownloadDataAsync(CancellationToken token)
{
    Console.WriteLine("Starting download...");
    for (int i = 1; i <= 5; i++)
    {
        // This is the most efficient way to listen.
        // Task.Delay will stop immediately if the alarm rings.
        await Task.Delay(1000, token);

        // You can also check manually, which is good for long CPU-bound loops.
        // token.ThrowIfCancellationRequested();

        Console.WriteLine($"Downloaded chunk {i}/5");
    }
}

try
{
    // We give the "bell" to our worker method.
    await DownloadDataAsync(cts.Token);
    Console.WriteLine("Download completed successfully.");
}
catch (OperationCanceledException)
{
    // This code only runs if the alarm was triggered.
    Console.WriteLine("Download was canceled.");
}


static async Task Main()
{
    // ... setup code ...

    while (true) // The main application loop
    {
        Console.Write("> "); // Show a prompt
        string userInput = await Console.ReadLineAsync(); // Asynchronously wait for input

        if (userInput == "c")
        {
            // Trigger cancellation and exit the loop
            cts.Cancel();
            break;
        }
        else if (userInput.StartsWith("download"))
        {
            // Start a download based on the input
            // e.g., download http://example.com
        }
        else
        {
            // Process other commands
            Console.WriteLine($"You entered: {userInput}");
        }
    }
}