using System;
using System.Collections.Generic;
using CustomThreadPoolLibrary.ThreadPool;
using Examples.Helpers;

namespace Examples.Highload
{
    public class ListActions : IExample
    {
        private readonly ICollection<Action> actions = ActionTestGenerator.SpinWaitLoop(50, 1, 2);

        public int ThreadsCount => 10;

        public void Work()
        {
            foreach (var action in actions)
            {
                CustomThreadPool.EnqueueTask(action);
            }
        }

        public void MenuCommandProcessor(int command)
        {
            switch (command)
            {
                case 1:
                    CustomThreadPoolHelper.TaskQueueCurrentSize();
                    break;
                case 2:
                    CustomThreadPoolHelper.WorkersStatus();
                    break;
                default:
                    Console.WriteLine("Unknown command");
                    break;
            }
        }
    }
}
