namespace MyNamespace
{
    using System;

    // Define the delegate outside the class
    public class Publisher_1
    {
        // Define a public delegate
        public delegate void MyDelegate(string message);

        // Method that uses the delegate
        public void PublishMessage(MyDelegate del)
        {
            del("Hello from Publisher!");
        }
    }

    public class Subscriber_1
    {
        public void Subscribe()
        {
            // Create an instance of the Publisher class
            Publisher_1 publisher = new Publisher_1();

            // Create an instance of the delegate and pass a method to it
            // MyDelegate del = new MyDelegate(PrintMessage); // This line is commented out because the delegate is defined inside the Publisher_1 class
            Publisher_1.MyDelegate del = new Publisher_1.MyDelegate(PrintMessage);
            // Call the method of the Publisher class and pass the delegate
            publisher.PublishMessage(del);
        }

        // Method that matches the delegate signature
        public void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class Program_1
    {
        public static void Main(string[] args)
        { 
            new Subscriber_1().Subscribe();
        }
    }
}