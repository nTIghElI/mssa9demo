using System;
using System.Threading;
 
class Program
{
    static void Main(string[] args)
    {
        Thread t = new Thread(LongWork);
        t.Start();
       
        bool finished = t.Join(500);
 
        if (finished)
            Console.WriteLine("Thread completed within timeout");
        else
            Console.WriteLine("Timeout was reached before thread finished");
 
        Console.WriteLine("Main thread ending");
 
    }
 
    static void LongWork()
    {
        for (int i = 0; i < 1000; i++)
        {
            Console.Write("y");
            Thread.Sleep(5);
        }
    }
}