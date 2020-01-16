using System;
using CustomThreadPoolLibrary.Task;
using CustomThreadPoolLibrary.ThreadPool;
using Examples.Extensions;
using Examples.Helpers;

namespace Examples.Examples.WithoutAwait.Generic
{
    public class Successful : IExample
    {
        private readonly Func<int> func = FuncCreator.SpinWait(2, 3);

        private CustomTask<int> task;

        public int ThreadsCount => 1;

        public void Initialization()
        {
            CustomThreadPool.TrySetMaxThreads(ThreadsCount);
        }

        public void Work()
        {
            task = CustomThreadPool.EnqueueTask(func);
        }

        public void MenuCommandProcessor(int command)
        {
            switch (command)
            {
                case 1:
                    task.Debug();
                    break;
                default:
                    Console.WriteLine("Unknown command");
                    break;
            }
        }
    }
}
