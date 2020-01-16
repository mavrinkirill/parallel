using System;

namespace CustomThreadPoolLibrary.Exceptions
{
    class CustomTaskInnerException : Exception
    {
        public CustomTaskInnerException()
        {
        }

        public CustomTaskInnerException(string message)
            : base(message)
        {
        }

        public CustomTaskInnerException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
