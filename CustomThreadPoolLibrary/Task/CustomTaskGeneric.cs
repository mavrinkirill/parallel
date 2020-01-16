using System.Threading;

namespace CustomThreadPoolLibrary.Task
{
    public class CustomTask<T> : CustomTaskBase
    {
        public T Result { get; private set; }

        public void SetResult(T result)
        {
            Result = result;
        }

        public T AwaitResult()
        {
            lock (Locker)
            {
                while (!ActionCompleted())
                {
                    Monitor.Wait(Locker);
                }

                ExceptionHandler();

                return Result;
            }
        }
    }
}
