using System;
using System.Diagnostics;
using System.Threading;

namespace LimitService
{
    /// <summary>
    /// 限流Demo,时间段限定数量
    /// </summary>
    public class LimitServiceDemo
    {
        int CurrentIndex = 0;
        DateTime[] RequestRing = null;
        int LimitSecond = 1;
        object Locker = new object();

        public LimitServiceDemo(int countPerSecond, int limitSecond)
        {
            RequestRing = new DateTime[countPerSecond];
            LimitSecond = limitSecond;
        }

        public bool IsContinue()
        {
            lock (Locker)
            {
                var currentNode = RequestRing[CurrentIndex];
                if (currentNode != null && currentNode.AddSeconds(LimitSecond) > DateTime.Now)
                {
                    return false;
                }
                RequestRing[CurrentIndex] = DateTime.Now;
                MoveNextIndex(ref CurrentIndex);
            }
            return true;
        }

        public bool ChangeCountPerSecond(int countPerSecond)
        {
            lock (Locker)
            {
                RequestRing = new DateTime[countPerSecond];
                CurrentIndex = 0;
            }
            return true;
        }

        //指针往后移动
        public void MoveNextIndex(ref int currentIndex)
        {
            var len = RequestRing.Length;
            if (currentIndex != len - 1)
            {
                currentIndex++;
            }
            else
                currentIndex = 0;

        }
    }

    public class LimitServiceDemoUse
    {
        LimitServiceDemo limitService = new LimitServiceDemo(10, 1);
        int ThreadCount = 10;

        public void RunThread()
        {
            while (ThreadCount > 0)
            {
                Thread thread = new Thread(Limit);
                thread.Start();
                ThreadCount--;
            }
        }

        public void Limit()
        {
            int runTimes = 100;
            int successCount = 0;
            int failedCount = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (runTimes > 0)
            {
                if (limitService.IsContinue())
                    successCount++;
                else
                    failedCount++;
                runTimes--;
            }
            stopwatch.Stop();
            Console.WriteLine($"耗时：{stopwatch.ElapsedMilliseconds},成功数：{successCount}，失败数：{failedCount}");
        }
    }
}
