using System;

namespace CustomThreadPoolLibrary.Exceptions
{
    class CustomTaskInterruptedException : Exception
    {
        public CustomTaskInterruptedException()
        {
        }

        public CustomTaskInterruptedException(string message)
            : base(message)
        {
        }

        public CustomTaskInterruptedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
