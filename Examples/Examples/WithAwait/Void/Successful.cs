using System;
using CustomThreadPoolLibrary.Task;
using CustomThreadPoolLibrary.ThreadPool;
using Examples.Extensions;
using Examples.Helpers;

namespace Examples.Examples.WithAwait.Void
{
    public class Successful : IExample
    {
        private readonly Action action = ActionCreator.SpinWait(3);

        private CustomTask task;

        public int ThreadsCount => 1;

        public void Initialization()
        {
            CustomThreadPool.TrySetMaxThreads(ThreadsCount);
        }

        public void Work()
        {
            task = CustomThreadPool.EnqueueTask(action);
            Console.WriteLine("Before await");
            task.Await();
            Console.WriteLine("After await");
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
