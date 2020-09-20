using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace D004.同步設計披薩製作
{
    class Program
    {
        static void Main(string[] args)
        {
            var watch = Stopwatch.StartNew();
            Console.WriteLine("開始進行製作披薩...");
            var 烤箱Task = Task.Run(() =>烤箱預熱());
            
            var 麵團Task = Task.Run(() =>
            {
                製作麵團();
                發麵();
            });
            var 醬料Task = Task.Run(()=> 準備醬料());
            var 配料Task = Task.Run(()=> 準備配料());
            var 生PizzaTask = Task.Run(() =>
            {
                Task.WaitAll(麵團Task, 醬料Task, 配料Task);
                製作披薩餅皮與塗抹醬料和放置配料();
            });

            var 熟PizzaTask = Task.Run(() =>
            {
                Task.WaitAll(烤箱Task, 生PizzaTask);
                烤製披薩();
            });
            var 餐具Task = Task.Run(() => 準備餐具與飲料());
            Task.WaitAll(熟PizzaTask, 餐具Task);
            披薩完成_開始食用();
            watch.Stop();

            Console.WriteLine($"同步設計披薩製作共花費:{watch.Elapsed.TotalMilliseconds} 毫秒");
        }

        static void 烤箱預熱()
        {
            Console.WriteLine($"烤箱預熱中，預計 3 秒鐘 [Thread:{Thread.CurrentThread.ManagedThreadId}]");
            Thread.Sleep(3000);
            Console.WriteLine($"烤箱預熱 完成");
        }

        static void 製作麵團()
        {
            Console.WriteLine($"製作麵團中，預計 0.3 秒鐘 [Thread:{Thread.CurrentThread.ManagedThreadId}]");
            Thread.Sleep(300);
            Console.WriteLine($"麵團製作 完成");
        }

        static void 發麵()
        {
            Console.WriteLine($"麵團發麵中，預計 0.8 秒鐘 [Thread:{Thread.CurrentThread.ManagedThreadId}]");
            Thread.Sleep(800);
            Console.WriteLine($"麵團發麵 完成");
        }

        static void 準備醬料()
        {
            Console.WriteLine($"準備醬料中，預計 0.2 秒鐘 [Thread:{Thread.CurrentThread.ManagedThreadId}]");
            Thread.Sleep(200);
            Console.WriteLine($"準備醬料 完成");
        }

        static void 準備配料()
        {
            Console.WriteLine($"準備配料中，預計 0.2 秒鐘 [Thread:{Thread.CurrentThread.ManagedThreadId}]");
            Thread.Sleep(200);
            Console.WriteLine($"準備配料 完成");
        }

        static void 製作披薩餅皮與塗抹醬料和放置配料()
        {
            Console.WriteLine($"製作披薩餅皮與塗抹醬料和放置配料中，預計 0.3 秒鐘 [Thread:{Thread.CurrentThread.ManagedThreadId}]");
            Thread.Sleep(300);
            Console.WriteLine($"製作披薩餅皮與塗抹醬料和放置配料 完成");
        }

        static void 烤製披薩()
        {
            Console.WriteLine($"烤製披薩中，預計 0.6 秒鐘 [Thread:{Thread.CurrentThread.ManagedThreadId}]");
            Thread.Sleep(600);
            Console.WriteLine($"烤製披薩 完成");
        }

        static void 準備餐具與飲料()
        {
            Console.WriteLine($"準備餐具與飲料中，預計 0.2 秒鐘 [Thread:{Thread.CurrentThread.ManagedThreadId}]");
            Thread.Sleep(200);
            Console.WriteLine($"準備餐具與飲料 完成");
        }

        static void 披薩完成_開始食用()
        {
            Console.WriteLine($"披薩完成_開始食用，預計 1 秒鐘 [Thread:{Thread.CurrentThread.ManagedThreadId}]");
            Thread.Sleep(100);
            Console.WriteLine($"披薩完成_開始食用 完成");
        }
    }
}
