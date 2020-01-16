using System;
using CustomThreadPoolLibrary.ThreadPool;

namespace Examples.Helpers
{
    public static class CustomThreadPoolHelper
    {
        public static void TaskQueueCurrentSize()
        {
            Console.WriteLine($"TaskQueue count: {CustomThreadPool.TaskQueueCount}");
        }

        public static void WorkersStatus()
        {
            var workers = CustomThreadPool.Workers;
            for (var i = 0; i < workers.Length; i++)
            {
                if (workers[i] == null)
                {
                    Console.WriteLine($"Thread #{i} is NULL.");
                }
                else
                {
                    Console.WriteLine($"Thread #{i}. ThreadState: {workers[i].ThreadState}");
                }
            }
        }
    }
}
