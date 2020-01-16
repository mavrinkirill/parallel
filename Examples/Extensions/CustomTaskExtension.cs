using System;
using CustomThreadPoolLibrary.Task;

namespace Examples.Extensions
{
    public static class CustomTaskExtension
    {
        public static void Debug(this CustomTask customTask)
        {
            Console.WriteLine($"Status: {customTask.Status}");
            Console.WriteLine($"Exception: {(customTask.Exception == null ? string.Empty : customTask.Exception.Message)}");
        }

        public static void Debug<T>(this CustomTask<T> customTask)
        {
            Console.WriteLine($"Status: {customTask.Status}");
            Console.WriteLine($"Exception: {(customTask.Exception == null ? string.Empty : customTask.Exception.Message)}");
            Console.WriteLine($"Result: {customTask.Result}");
        }
    }
}
