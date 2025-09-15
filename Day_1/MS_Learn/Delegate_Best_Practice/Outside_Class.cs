namespace MyNamespace
{
    using System;

    // Define the delegate outside the class
    public delegate void MyDelegate(string message);

    public class Publisher
    {
        // Method that uses the delegate
        public void PublishMessage(MyDelegate del)
        {
            del("Hello from Publisher!");
        }
    }

    public class Subscriber
    {
        public void Subscribe()
        {
            // Create an instance of the Publisher class
            Publisher publisher = new Publisher();

            // Create an instance of the delegate and pass a method to it
            MyDelegate del = new MyDelegate(PrintMessage);

            // Call the method of the Publisher class and pass the delegate
            publisher.PublishMessage(del);
        }

        // Method that matches the delegate signature
        public void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            new Subscriber().Subscribe();
        }
    }
}