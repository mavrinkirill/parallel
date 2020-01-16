using System;
using System.Threading;
using CustomThreadPoolLibrary.CancellationToken;
using CustomThreadPoolLibrary.ThreadPool;

namespace Examples.Helpers
{
    public static class FuncCreator
    {
        private static int SpinWaitTick = 30000000;

        public static Func<T> SpinWait<T>(int tickCount, T result)
        {
            return () =>
            {
                Thread.SpinWait(tickCount * SpinWaitTick);
                return result;
            };
        }

        public static Func<ICustomCancellationToken, T> SpinWaitLoopWithCancellation<T>(int loopCount, T result)
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

                return result;
            };
        }

        public static Func<T> SpinWaitWithException<T>(int tickCount, T result)
        {
            return () =>
            {
                Thread.SpinWait(tickCount * SpinWaitTick);
                throw new InvalidOperationException();
            };
        }
    }
}
