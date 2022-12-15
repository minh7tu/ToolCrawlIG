using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VCCorp.IG.Core.Helper
{
    public static class ThreadHelper
    {
        public static async Task StartWithItemsPerThread_Async<T>(List<T> list, int itemPerThread, Func<int, int, List<T>, Task> action)
        {
            var tasks = new List<Task>();
            for (int i = 0; i <= list.Count / itemPerThread; i++)
            {
                var data = GetStartEndThread(i, itemPerThread, list.Count);
                tasks.Add(action(data.Item1, data.Item2, list));
            }

            if (tasks.Count > 0)
            {
                await Task.WhenAll(tasks);
            }
        }
        // call async in not async: disk_free = Nito.AsyncEx.AsyncContext.Run(GetDisplayFreeSpace),

        public static void StartWithItemsPerThread<T>(List<T> list, int itemPerThread, Action<int, int, List<T>> action)
        {
            for (int i = 0; i <= list.Count / itemPerThread; i++)
            {
                var data = GetStartEndThread(i, itemPerThread, list.Count);
                new Thread(() => action(data.Item1, data.Item2, list)).Start();
            }
        }

        public static async Task RunAllTasks(IEnumerable<Task> tasks)
        {
            await Task.WhenAll(tasks);
        }

        //public static T RunAsyncTaskNotAsync<T>(Func<Task<T>> task)
        //{
        //    return Nito.AsyncEx.AsyncContext.Run(task);
        //}

        //public static void RunAsyncTaskNotAsync(Func<Task> task)
        //{
        //    Nito.AsyncEx.AsyncContext.Run(task);
        //}        //public static T RunAsyncTaskNotAsync<T>(Func<Task<T>> task)
        //{
        //    return Nito.AsyncEx.AsyncContext.Run(task);
        //}

        //public static void RunAsyncTaskNotAsync(Func<Task> task)
        //{
        //    Nito.AsyncEx.AsyncContext.Run(task);
        //}

        public static void StartWithThreadsCount<T>(List<T> list, int threadsCount, Action<int, int, List<T>> action)
        {
            //ThreadHelper.StartWithThreadsCount(list, 10, Run);
            //static async void Run(int start, int end, List<TikiProduct> list)

            for (int i = 0; i < threadsCount; i++)
            {
                //var data = GetStartEndThreadWithThreadsCount(i, threadsCount, list.Count);
                // new Thread(() => action(data.start, data.end, list)).Start();
            }
        }

        public static Tuple<int, int> GetStartEndThread(int index, int itemsPerThread, int max)
        {
            var start = index * itemsPerThread;
            var end = (index + 1) * itemsPerThread - 1;
            if (end >= max)
            {
                end = max - 1;
            }
            return new Tuple<int, int>(start, end);
        }

        //public static (int start, int end, int count) GetStartEndThreadWithThreadsCount(int index, int threadsCount, int total)
        //{
        //    int itemsPerThread = total / threadsCount + 1;
        //    var start = index * itemsPerThread;
        //    var end = (index + 1) * itemsPerThread - 1;
        //    if (end >= total)
        //        end = total - 1;
        //    $"{start} => {end}".WriteToDebug();
        //    return (start, end, end - start + 1); // end - start + 1  = itemsPerThread
        //    // error when total < threadCount
        //}

        // code sample for multi thread running
        static void RunInThreads<T>(int start, int end, List<T> list)
        {
            //for (int i = 0; i <= list.Count / ITEMS_PER_THREAD; i++)
            //{
            //    var data = GetStartEndThread(i, ITEMS_PER_THREAD);
            //    new Thread(() => RunInThreads(data.Item1, data.Item2, list)).Start();
            //}

            //UpdateText($"START FROM {start} TO {end}");
            for (int i = start; i <= end; i++)
            {
                try
                {

                }
                catch (Exception)
                {
                }
            }
            //UpdateText($"END FROM {start} TO {end}");
        }

        public static async Task Wait(int ms)
        {
            await Task.Delay(ms);
        }

        public static async Task<T> RunTaskWithTimeout<T>(this Task<T> task, int timeOutMs = 7000)
        {
            try
            {
                await Task.WhenAny(task, Wait(timeOutMs));
                if (task.IsCompleted)
                {
                    return task.Result;
                }
            }
            catch (Exception ex)
            {
                //ex.Message.WriteToDebug();
            }

            //$"RunTaskWithTimeout: TIMEOUT ".WriteToDebug();
            return default(T);
        }


        public static void RunAfter(double seconds, Action action, bool isOneTime = true, bool isRunImmediately = false, CancellationTokenSource cancelTokenSource = null)
        {
            int time = 0;
            try
            {
                time = Convert.ToInt32(seconds * 1000);
            }
            catch (OverflowException)
            {
                time = int.MaxValue;
            }

            Timer timer = null;
            timer = new Timer((o) =>
            {
                if (cancelTokenSource != null && cancelTokenSource.IsCancellationRequested)
                {
                    timer.Dispose();
                    return;
                }
                action();

                if (isOneTime)
                {
                    timer.Dispose();
                    //$"Timer stopped after {seconds}s".WriteToDebug();
                }

            }, null, isRunImmediately ? 0 : time, time);

            #region Repeat Timer Sample Code
            //public static void StartP2PPriceScanner()
            //{
            //    ThreadHelper.RunAfter(TimeSpan.FromMinutes(30), async () =>
            //    {
            //        await Run();
            //    }, false, true);

            //    async Task Run()
            //    {

            //    }
            //} 
            #endregion
        }

        public static void RunAfter(TimeSpan interval, Action action, bool isOneTime = true, bool isRunImmediately = false, CancellationTokenSource cancelTokenSource = null) => RunAfter(interval.TotalSeconds, action, isOneTime, isRunImmediately, cancelTokenSource);

        public static CancellationToken GetCancellationToken(TimeSpan timeSpan)
        {
            return new CancellationTokenSource(timeSpan).Token;
        }

        public static CancellationToken GetCancellationToken(int timeOutMs)
        {
            return new CancellationTokenSource(timeOutMs).Token;
        }
    }
}
