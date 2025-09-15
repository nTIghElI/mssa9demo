using System;
using System.Threading;
 
class Program
{
    static void Main(string[] args)
    {
        Thread t = new Thread(WriteY);
        t.Start();
 
         for (int i = 0; i < 10000; i++)
            Console.Write("x");
 
    }
 
    static void WriteY()
    {
        for (int i = 0; i < 10000; i++)
            Console.Write("y");
    }
}