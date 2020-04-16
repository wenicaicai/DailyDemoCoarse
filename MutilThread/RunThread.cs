using System;
using System.Diagnostics.Contracts;
using System.Threading;

namespace MutilThread
{
    public class RunThread
    {
        bool isDone = false;
        int val_I = 1, val_II = 10;

        static object locker = new object();
        public void PrintXY()
        {
            Thread thread = new Thread(PrintY_Lock);
            //var threadStateI = thread.ThreadState;
            thread.Start();
            //var threadStateII = thread.ThreadState;
            PrintY_Lock();
            if (!isDone)
            {
                Console.Write("X");
                isDone = true;
            }
        }

        public void PrintY_Lock()
        {
            lock (locker)
            {
                if (!isDone)
                {
                    Thread.Sleep(1000);
                    Console.Write("Y：有锁");
                    isDone = true;
                }
            }
        }

        public void PrintY_UnLock()
        {
            if (!isDone)
            {
                Thread.Sleep(1000);
                Console.Write("\nX：没有锁");
                isDone = true;
            }
        }

        public void PrintX()
        {
            Console.Write("\nX");
        }

        public void PrintX_WithName(object name)
        {
            Console.Write($"\nX,hello {name}");
        }

        public void Use_Join_WaitThread()
        {
            Thread thread = new Thread(PrintY_UnLock);
            thread.Start();
            thread.Join();
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("X");
            }
        }

        //如果工作线程为：前台线程(“被忘记的”)，主线程退出，程序保持
        //如果工作线程为：后台线程，主线程退出，程序退出

        public void Go()
        {

            if (val_I != 0)
            {
                Thread.Sleep(1000);
                Console.WriteLine(val_II / val_I);
            }
            val_I = 0;
        }

        public void MutilThread()
        {
            Thread threadI = new Thread(Print);
            Thread threadII = new Thread(Print);
            Thread threadIII = new Thread(Print);
            Thread threadVI = new Thread(Print);
            Thread threadV = new Thread(Print);


        }

        public void Print(object i)
        {
            Console.WriteLine(i);
        }
    }
}
