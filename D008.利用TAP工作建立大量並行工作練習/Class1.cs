using System;
using System.Threading;
using System.Threading.Tasks;
public class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("1 " + String.Format("{0:D2}", Thread.CurrentThread.ManagedThreadId));
        //Task task = MyPromiseAsync();
        //task.Wait();
        await MyPromiseAsync();
        Console.WriteLine("2 " + String.Format("{0:D2}", Thread.CurrentThread.ManagedThreadId));
    }
    public static async Task MyPromiseAsync()
    {
        Console.WriteLine("3 " + String.Format("{0:D2}", Thread.CurrentThread.ManagedThreadId));
        await Task.Yield();
        Console.WriteLine("4 " + String.Format("{0:D2}", Thread.CurrentThread.ManagedThreadId));
        await Task.Delay(500);
        Console.WriteLine("5 " + String.Format("{0:D2}", Thread.CurrentThread.ManagedThreadId));
    }
}