using System;
using System.Threading;

class Program
{
    [ThreadStatic] static int counter = 0;
    [ThreadStatic] static int threadVal1 = 40; //S2996 warns me that threadVal1 should be lazy initialized, it's not and in SecondarThread it will default to 0 so very useful
    [ThreadStatic] public static int threadVal2 = 50; //S2223 suggests to make it const or readonly, but this will make compilation fail, shouldn't S2996 be raised instead?


    static void Main()
    {
        Thread workerThread = new Thread(new ThreadStart(Print));
        workerThread.Name = "SecondaryThread";
        workerThread.Start();

        Thread.CurrentThread.Name = "MainThread";
        Print();

        Console.ReadKey();
    }


    static void Print()
    {
        for (; counter < 10; counter++)
        {
            threadVal1++;
            threadVal2++;

            Console.WriteLine($"{Thread.CurrentThread.Name}, iteration {counter}: threadVal1: {threadVal1}, threadVal2: {threadVal2}");
            Thread.Sleep(100);
        }
    }
}