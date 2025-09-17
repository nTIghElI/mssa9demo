using System.Threading;
using System.Threading.Tasks;
 
class Program
{
    public static async Task Main(string[] args)
    {
        using var cts = new CancellationTokenSource();
        Console.WriteLine("Press 'c' to cancel the quiz\n");
 
        _ = Task.Run(() =>
        {
            if (Console.ReadKey(true).KeyChar == 'c')
            {
                cts.Cancel();
                Console.WriteLine("\nQuiz cancellation requested");
            }
        });
        try
        {
            await AskQuestionAsync("What is 2 + 2?", cts.Token);
            await AskQuestionAsync("Name a color?", cts.Token);
            await AskQuestionAsync("What is the capital of France?", cts.Token);
 
            Console.WriteLine("\nQuiz is finished");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("\nQuiz cancelled");
        }
 
    }
 
    static async Task AskQuestionAsync(string question, CancellationToken token)
    {
        Console.WriteLine(question);
        var inputTask = Task.Run(() => Console.ReadLine(), token);
        var delayTask = Task.Delay(5000, token);
 
        var finished = await Task.WhenAny(inputTask, delayTask);
        if (finished == inputTask)
        {
            string answer = await inputTask;
            Console.WriteLine($"You answered: {answer}");
        }
        else
        {
            Console.WriteLine("Time is up");
        }
    }
}