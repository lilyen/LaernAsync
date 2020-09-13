using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace D008.利用TAP工作建立大量並行工作練習
{
    class Program
    {
        static object __lockObj = new object();

        static void Main(string[] args)
        {
            string URL = "http://mocky.azurewebsites.net/api/delay/2000";

            HttpClient client = new HttpClient();

            for (int i = 0; i < 10; i++)
            {
                var index = string.Format("{0:D2}", (i + 1));

                Task.Run(() =>
                {
                    // 取得當下的 ThreadId
                    var tid = String.Format("{0:D2}", Thread.CurrentThread.ManagedThreadId);

                    ShowDebugInfo(index, 1, tid, ">>>>");
                    var result = client.GetStringAsync(URL).Result;
                    ShowDebugInfo(index, 1, tid, "<<<<", result);

                    ShowDebugInfo(index, 2, tid, ">>>>");
                    result = client.GetStringAsync(URL).Result;
                    ShowDebugInfo(index, 2, tid, "<<<<", result);
                });
            }

            Console.WriteLine("按下任一按鍵，結束處理程序");
            Console.ReadKey();
        }

        static void ShowDebugInfo(string index, int trial, string tid, string sep, string result = null)
        {
            lock (__lockObj)
            {
                ConsoleColor orig = Console.ForegroundColor;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{index}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($" << ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{trial}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($" >> 測試 (TID: ");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{tid}");

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($")");

                if (result != null)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                Console.Write($" {sep} ");

                if (result != null)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                }
                Console.Write($"{DateTime.Now}");

                Console.ForegroundColor = ConsoleColor.Cyan;
                if (result != null)
                {
                    Console.Write($" {result}");
                }
                Console.WriteLine();

                Console.ForegroundColor = orig;
            }
        }
    }
}
