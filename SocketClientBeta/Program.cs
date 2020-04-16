using LimitService;
using MutilThread;
using System;
using System.Threading;

namespace SocketClientBeta
{
    class Program
    {
        static RunThread thread = new RunThread();
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!Client is running . .  .    .       .            .");
            
            Console.WriteLine($"本机芯片数量：{Environment.ProcessorCount}");

            RunIII();
        }

        private static void RunI()
        {
            thread.PrintXY();

            //Thread t = new Thread(new ThreadStart(thread.PrintX));
            Thread t = new Thread(thread.PrintX);
            t.Start();
            thread.PrintX();
            Console.WriteLine("线程是否后台线程：{0}", t.IsBackground);

            var personName = "John";
            Thread t_I = new Thread(thread.PrintX_WithName);
            //方式一：线程传参 (ParameterizedThreadStart)
            //方式二：
            //方式三：
            t_I.Start(personName);
            thread.PrintX_WithName(personName);

            //thread.Use_Join_WaitThread();
        }

        private static void RunII()
        {
            Thread t = new Thread(thread.PrintY_UnLock);
            t.Start();
            thread.PrintY_UnLock();
        }

        private static void RunIII()
        {
            LimitServiceDemoUse limitServiceDemoUse = new LimitServiceDemoUse();
            limitServiceDemoUse.RunThread();
        }
    }
}
