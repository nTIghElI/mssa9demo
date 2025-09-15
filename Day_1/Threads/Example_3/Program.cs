using System;
using System.Threading;
 
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Sleeping for 10 seconds...");
        Thread.Sleep(10000);
        Console.WriteLine("Sleeping for 5 seconds...");
        Thread.Sleep(5000);
        Console.WriteLine("Returning immediately...");
        Thread.Sleep(0);
        Console.WriteLine("Done");
       
    }
}