using System;
using System.Threading.Tasks;

public class AsyncTeaTime
{
    // This is our asynchronous "worker" method.
    // - 'async' unlocks the 'await' keyword.
    // - 'Task<string>' is the "promise" that it will eventually return a string.
    public static async Task<string> DownloadFileAsync(string fileName)
    {
        Console.WriteLine($"Starting download of '{fileName}'...");

        // await is the "pause and resume" point.
        // It pauses THIS METHOD, but returns control to the caller (Main).
        // The program does NOT freeze here.
        await Task.Delay(3000); // Simulate a 3-second download.

        Console.WriteLine($"...Finished download of '{fileName}'.");
        return "File content goes here.";
    }

    public static async Task Main()
    {
        // Start the download. The code inside DownloadFileAsync will run
        // until it hits the first 'await'.
        Task<string> downloadTask = DownloadFileAsync("MyImportantFile.txt");

        // Because DownloadFileAsync "yielded" control at its await,
        // this code can run immediately without waiting for the download to finish.
        Console.WriteLine("Main thread is now free to do other work!");
        Console.WriteLine("Doing some other work... 1");
        Console.WriteLine("Doing some other work... 2");

        // Now, we actually need the result of the download.
        // We 'await' the task, which will pause the Main method
        // until the download is complete.
        string fileContent = await downloadTask;

        Console.WriteLine($"\nDownload complete. The result is: '{fileContent}'");
    }
}
// ```

// ### Expected Output

// When you run this code, you will see this output, which proves that the `Main` method didn't block:

// ```
// Starting download of 'MyImportantFile.txt'...
// Main thread is now free to do other work!
// Doing some other work... 1
// Doing some other work... 2
// ( ... a 3 second pause happens here ... )
// ...Finished download of 'MyImportantFile.txt'.

// Download complete. The result is: 'File content goes here.'
