using System;
using CustomThreadPoolLibrary.CancellationToken;
using CustomThreadPoolLibrary.Task;
using CustomThreadPoolLibrary.ThreadPool;
using Examples.Extensions;
using Examples.Helpers;

namespace Examples.Examples.WithoutAwait.Void
{
    public class WithInterruptExample : IExample
    {
        private readonly ICustomCancellationToken cancellationToken = new CustomCancellationToken();

        private readonly Action<ICustomCancellationToken> action = ActionCreator.SpinWaitLoopWithCancellation(5);

        private CustomTask task;

        public int ThreadsCount => 1;

        public void Initialization()
        {
            CustomThreadPool.TrySetMaxThreads(ThreadsCount);
        }

        public void Work()
        {
            task = CustomThreadPool.EnqueueTask(action, cancellationToken);
        }

        public void MenuCommandProcessor(int command)
        {
            switch (command)
            {
                case 1:
                    task.Debug();
                    break;
                case 2:
                    cancellationToken.Cancel();
                    break;
                default:
                    Console.WriteLine("Unknown command");
                    break;
            }
        }
    }
}
