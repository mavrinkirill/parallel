using System;
using System.Threading;
using CustomThreadPoolLibrary.CancellationToken;
using CustomThreadPoolLibrary.ThreadPool;

namespace Examples.Helpers
{
    public static class ActionCreator
    {
        private static int SpinWaitTick = 30000000;

        public static Action Sleep(int seconds)
        {
            return () => { Thread.Sleep(seconds * 1000); };
        }

        public static Action SleepLoop(int loopCount)
        {
            return () =>
            {
                for (var i = 0; i < loopCount; i++)
                {
                    Thread.Sleep(1000);
                }
            };
        }

        public static Action SleepWithAddingToThreadPool(int seconds)
        {
            return () =>
            {
                CustomThreadPool.EnqueueTask(Sleep(seconds));
                Thread.Sleep(seconds * 1000);
                CustomThreadPool.EnqueueTask(Sleep(seconds));
            };
        }

        public static Action SpinWait(int tickCount)
        {
            return () => { Thread.SpinWait(tickCount * SpinWaitTick); };
        }

        public static Action SpinWaitLoop(int loopCount)
        {
            return () =>
            {
                for (var i = 0; i < loopCount; i++)
                {
                    Thread.SpinWait(SpinWaitTick);
                }
            };
        }

        public static Action<ICustomCancellationToken> SpinWaitLoopWithCancellation(int loopCount)
        {
            return token =>
            {
                for (var i = 0; i < loopCount; i++)
                {
                    if (token.IsCancellationRequested())
                    {
                        CustomThreadPool.InterruptCurrentTask();
                    }

                    Thread.SpinWait(SpinWaitTick);
                }
            };
        }

        public static Action SpinWaitWithException(int tickCount)
        {
            return () =>
            {
                Thread.SpinWait(tickCount * SpinWaitTick);
                throw new InvalidOperationException();
            };
        }
    }
}
