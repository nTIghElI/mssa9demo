# C# Asynchronous Programming and Concurrency Demo

This repository contains a collection of C# console applications that demonstrate fundamental concepts of asynchronous programming, concurrency, and modern .NET features. Each project focuses on a specific pattern or problem, from making web requests to handling user input and managing long-running tasks.

## Key Concepts Demonstrated

This collection illustrates the evolution from inefficient blocking code to highly efficient, responsive, and cancellable asynchronous operations.

### 1. Blocking vs. Non-Blocking I/O (`Day_4\In_Class_2`)

This project directly compares two ways of handling multiple web requests.

*   **Blocking Operations**: The initial code (`In_Class_2\Program.cs`) demonstrates a common but inefficient pattern. It uses `HttpClient` to make several API calls inside a `foreach` loop. By calling `.Result` on the asynchronous methods (`client.GetAsync(url).Result`), it forces the main thread to **stop and wait** for each network request to complete before starting the next one. The total execution time is the sum of all individual request times.

*   **Asynchronous (Non-Blocking) Operations**: The more efficient approach is to start all the web requests concurrently and then wait for them all to finish. This is achieved with `async`/`await` and `Task.WhenAll`. The program initiates all downloads without waiting, allowing them to run in parallel. The total execution time is determined by the *longest* single request, not the sum of all of them, which is a significant performance improvement.

### 2. The `async` and `await` Keywords (`Day_3\Async_Example`)

This is the foundation of modern C# asynchronous programming.

*   **`async`**: A method modifier that allows the use of the `await` keyword within it. It changes the method's signature to return a `Task` or `Task<T>`.
*   **`await`**: The "pause and resume" operator. When `await` is used on a `Task`, it pauses the execution of the *current method* and returns control to its caller. The program's main thread is **not blocked** and can continue doing other work. Once the awaited task completes, execution resumes at the line after the `await`.

The `AsyncTeaTime` example shows how the `Main` method can continue to execute other code while a simulated file download happens in the background.

### 3. Task Parallelism with `Task.WhenAll` (`Day_2\Assingment_1(WeatherStationApp)`)

When you have multiple independent asynchronous operations, you don't need to wait for them one by one.

*   **`Task.WhenAll`**: This method takes a collection of `Task` objects and creates a single `Task` that completes only when *all* of the input tasks have successfully completed.

The `WeatherStation` example demonstrates this perfectly. It starts three separate "sensor" tasks that run concurrently. Instead of `await`ing each one sequentially, it uses `await Task.WhenAll(...)` to wait for all three to finish. This is the key to achieving true parallelism for I/O-bound operations.

### 4. Cancellation with `CancellationToken` (`Day_3\Two_Listener_Solution`, `Day_2\Assignment_2_Quiz_App`, `Day_2\Assingment_1(WeatherStationApp)`)

Long-running operations should be cancellable. The .NET `CancellationToken` pattern provides a cooperative, standardized way to do this.

*   **`CancellationTokenSource` (CTS)**: The "fire alarm button." You create an instance of this and call its `.Cancel()` method to signal that an operation should stop.
*   **`CancellationToken`**: The "fire alarm bell." You get this from the CTS's `.Token` property and pass it to your asynchronous methods.
*   **Listening for Cancellation**: A method can "listen" for the cancellation signal in a few ways:
    *   Passing the token to `async` methods that support it (e.g., `await Task.Delay(1000, token)`). These methods will throw an `OperationCanceledException` automatically if cancellation is requested.
    *   Periodically checking the token's state with `token.IsCancellationRequested`.
    *   Calling `token.ThrowIfCancellationRequested()` to manually throw the exception if cancellation has been signaled.

The `QuizApp` and `WeatherStation` examples show how to create a `CancellationTokenSource` tied to a key press (`'c'`) and pass its token to worker tasks, which can then be gracefully stopped.

### 5. Advanced Cancellation Scenarios (`Day_2\Assignment_2_Quiz_App`)

Cancellation tokens can be combined for more complex logic.

*   **Timeouts**: `CancellationTokenSource` has a `CancelAfter(TimeSpan)` method that automatically signals cancellation after a specified duration. This is perfect for implementing timeouts.
*   **`CancellationTokenSource.CreateLinkedTokenSource`**: This powerful feature allows you to create a new `CancellationTokenSource` that is linked to one or more existing tokens. The new source will be canceled if *any* of its parent tokens are canceled.

The `QuizApp` uses this to create a per-question timer. Each question needs to be answered within 5 seconds **OR** before the user cancels the entire quiz by pressing 'c'. A linked token source elegantly handles this "either/or" cancellation logic.

### 6. Asynchronous File I/O and Data Processing (`Day_1\LP6SampleApps\Delegates\Solution`)

This example demonstrates a more complex, real-world workflow involving asynchronous file operations and data generation.

*   **`async` All the Way**: The entire call stack, from reading a list of names from a file (`LoadApprovedNamesAsync`) to saving generated customer data to JSON (`SaveBankCustomerAsync`), is asynchronous. This ensures the application remains responsive even while performing disk I/O.
*   **Data Simulation**: It shows how to structure a larger operation where data is loaded, processed in a loop (simulating transactions), and then written back out, all without blocking the UI or main thread.

## How to Run

1.  Clone the repository.
2.  Navigate to a specific project folder (e.g., `cd Day_4/In_Class_2`).
3.  Run the application using the .NET CLI:

    ```bash
    dotnet run
    ```

4.  Follow the on-screen instructions for each application. For example, some apps may ask you to press 'c' to test the cancellation feature.