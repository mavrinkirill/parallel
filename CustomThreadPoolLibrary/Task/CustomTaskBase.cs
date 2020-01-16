using System;
using System.Threading;
using CustomThreadPoolLibrary.Exceptions;

namespace CustomThreadPoolLibrary.Task
{
    public abstract class CustomTaskBase
    {
        public CustomTaskStatus Status { get; private set; }

        public Exception Exception { get; private set; }

        protected Action Action;

        protected readonly object Locker = new object();

        protected CustomTaskBase()
        {
            Status = CustomTaskStatus.Created;
        }

        public void SetAction(Action action)
        {
            lock (Locker)
            {
                if (Status == CustomTaskStatus.Created)
                {
                    Action = action;
                    Status = CustomTaskStatus.TaskAdded;
                }
                else
                {
                    throw new CustomTaskInvalidStatusOperationException();
                }
            }
        }

        public void RunTask()
        {
            lock (Locker)
            {
                if (Status == CustomTaskStatus.TaskAdded)
                {
                    Status = CustomTaskStatus.Running;
                }
                else
                {
                    throw new CustomTaskInvalidStatusOperationException();
                }
            }

            Action.Invoke();
        }

        public void SetCompletedStatus()
        {
            lock (Locker)
            {
                if (Status == CustomTaskStatus.Running)
                {
                    Status = CustomTaskStatus.Completed;
                    Monitor.PulseAll(Locker);
                }
                else
                {
                    throw new CustomTaskInvalidStatusOperationException();
                }
            }
        }

        public void SetInterruptedStatus()
        {
            lock (Locker)
            {
                if (Status == CustomTaskStatus.Running)
                {
                    Status = CustomTaskStatus.Interrupted;
                    Exception = new CustomTaskInterruptedException();
                    Monitor.PulseAll(Locker);
                }
                else
                {
                    throw new CustomTaskInvalidStatusOperationException();
                }
            }
        }

        public void SetExceptionStatus(Exception innerException)
        {
            lock (Locker)
            {
                if (Status == CustomTaskStatus.Running)
                {
                    Status = CustomTaskStatus.Exception;
                    Exception = innerException;
                    Monitor.PulseAll(Locker);
                }
                else
                {
                    throw new CustomTaskInvalidStatusOperationException();
                }
            }
        }

        public void Await()
        {
            lock (Locker)
            {
                while (!ActionCompleted())
                {
                    Monitor.Wait(Locker);
                }

                ExceptionHandler();
            }
        }

        protected bool ActionCompleted()
        {
            return CustomTaskSettings.CompletedStatuses.Contains(Status);
        }

        protected void ExceptionHandler()
        {
            switch (Status)
            {
                case CustomTaskStatus.Interrupted:
                    throw new CustomTaskInterruptedException();
                case CustomTaskStatus.Exception:
                    throw new CustomTaskInnerException(string.Empty, Exception);
            }
        }
    }
}
