using System;
using CustomThreadPoolLibrary.Task;
using CustomThreadPoolLibrary.ThreadPool;
using Examples.Extensions;
using Examples.Helpers;

namespace Examples.Examples.Generic.WithoutAwait
{
    public class WithExceptionExample : IExample
    {
        private readonly Func<int> func = FuncCreator.SpinWaitWithException(2, 4);

        private CustomTask<int> task;

        public int ThreadsCount => 1;

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
