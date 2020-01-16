using System;

namespace CustomThreadPoolLibrary.Exceptions
{
    class CustomTaskInvalidStatusOperationException : Exception
    {
        public CustomTaskInvalidStatusOperationException()
        {
        }

        public CustomTaskInvalidStatusOperationException(string message)
            : base(message)
        {
        }

        public CustomTaskInvalidStatusOperationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
