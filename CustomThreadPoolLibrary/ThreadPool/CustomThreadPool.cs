using System;
using System.Collections.Generic;
using System.Threading;
using CustomThreadPoolLibrary.CancellationToken;
using CustomThreadPoolLibrary.Task;
using Infrastructure;

namespace CustomThreadPoolLibrary.ThreadPool
{
    public static class CustomThreadPool
    {
        private static readonly object Locker = new object();

        private static Thread[] _workers;

        private static readonly Queue<CustomTaskBase> TaskQueue = new Queue<CustomTaskBase>();

        public static int TaskQueueCount => TaskQueue.Count;

        public static Thread[] Workers => _workers;

        public static bool TrySetMaxThreads(int maxThreads)
        {
            var success = false;

            lock (Locker)
            {
                if (_workers == null || _workers.Length == 0)
                {
                    _workers = new Thread[maxThreads];
                    success = true;
                }
            }

            if (success)
            {
                for (var i = 0; i < _workers.Length; i++)
                {
                    if (_workers[i] == null)
                    {
                        var threadId = i;
                        _workers[i] = new Thread(() => { Consume(threadId); }) { IsBackground = true };
                        _workers[i].Start();
                    }
                }
            }

            return success;
        }

        public static CustomTask EnqueueTask(Action action)
        {
            var task = new CustomTask();
            task.SetAction(action);

            lock (Locker)
            {
                TaskQueue.Enqueue(task);
                Monitor.PulseAll(Locker);
            }

            return task;
        }

        public static CustomTask<T> EnqueueTask<T>(Func<T> func)
        {
            var task = new CustomTask<T>();

            Action action = () =>
            {
                task.SetResult(func());
            };

            task.SetAction(action);

            lock (Locker)
            {
                TaskQueue.Enqueue(task);
                Monitor.PulseAll(Locker);
            }

            return task;
        }

        public static CustomTask EnqueueTask(Action<ICustomCancellationToken> action, ICustomCancellationToken cancellationToken)
        {
            return EnqueueTask(() => action(cancellationToken));
        }

        public static CustomTask<T> EnqueueTask<T>(Func<ICustomCancellationToken, T> func, ICustomCancellationToken cancellationToken)
        {
            return EnqueueTask(() => func(cancellationToken));
        }

        public static void InterruptCurrentTask()
        {
            throw new ThreadInterruptedException();
        }

        private static void Consume(int threadId)
        {
            while (true)
            {
                CustomTaskBase task;
                lock (Locker)
                {
                    while (TaskQueue.Count == 0)
                    {
                        ConsoleHelper.WriteCyanLine($"Thread #{threadId} doesn't take new task. TaskQueue empty");
                        Monitor.Wait(Locker);
                    }
                    task = TaskQueue.Dequeue();
                }

                if (task == null)
                {
                    return;
                }

                ConsoleHelper.WriteGreenLine($"Thread #{threadId} take new task.");

                try
                {
                    task.RunTask();
                    task.SetCompletedStatus();
                }
                catch (ThreadInterruptedException)
                {
                    task.SetInterruptedStatus();
                }
                catch (Exception e)
                {
                    task.SetExceptionStatus(e);
                }
            }
        }
    }
}
