using System;
using System.Threading;
using System.Threading.Tasks;

public class QuizApp
{
    // A helper method to read a line from the console asynchronously and make it cancellable.
    public static Task<string> ReadLineAsync(CancellationToken token)
    {
        return Task.Run(() =>
        {
            // This loop checks for cancellation while waiting for the user to press Enter.
            while (!Console.KeyAvailable)
            {
                // If cancellation is requested, stop waiting and throw.
                if (token.IsCancellationRequested)
                {
                    token.ThrowIfCancellationRequested();
                }
                // A short delay to prevent this loop from using 100% CPU.
                Task.Delay(50, token).Wait(token);
            }
            return Console.ReadLine();
        }, token);
    }

    public static async Task Main()
    {
        // The main CancellationTokenSource for the 'c' key.
        using var mainCts = new CancellationTokenSource();

        Console.WriteLine("Press 'c' at any time to cancel the quiz.\n");

        // A background task to listen for the 'c' key.
        _ = Task.Run(() =>
        {
            if (Console.ReadKey(true).KeyChar == 'c')
            {
                mainCts.Cancel();
            }
        });

        // The list of questions for our quiz.
        string[] questions = {
            "What is 2 + 2?",
            "Name a color:",
            "What is the capital of France?"
        };

        try
        {
            // This is your excellent loop logic.
            foreach (var question in questions)
            {
                Console.WriteLine(question);
                Console.WriteLine("(5 seconds to answer)");

                // Create a token source that is linked to the main one and has a 5-second timer.
                using var questionCts = CancellationTokenSource.CreateLinkedTokenSource(mainCts.Token);
                questionCts.CancelAfter(TimeSpan.FromSeconds(5));

                try
                {
                    // "Race" the user input against the 5-second timer.
                    string answer = await ReadLineAsync(questionCts.Token);
                    Console.WriteLine($"You answered: {answer}\n");
                }
                catch (OperationCanceledException)
                {
                    // This is your logic to check why it was canceled.
                    if (mainCts.IsCancellationRequested)
                    {
                        // The user pressed 'c'.
                        throw; // Re-throw to be caught by the outer catch block.
                    }
                    else
                    {
                        // The 5-second timer ran out.
                        Console.WriteLine("Time's up!\n");
                    }
                }
            }
        }
        catch (OperationCanceledException)
        {
            // This outer catch block handles the quiz being canceled by the user.
            Console.WriteLine("\nQuiz canceled.");
        }
    }
}

