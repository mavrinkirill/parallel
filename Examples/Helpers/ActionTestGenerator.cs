using System;
using System.Collections.Generic;

namespace Examples.Helpers
{
    public static class ActionTestGenerator
    {
        private static Random _rnd = new Random();

        public static ICollection<Action> SpinWaitLoop(int count, int minLoopCountValue, int maxLoopCountValue)
        {
            var result = new List<Action>();

            for (var i = 0; i < count; i++)
            {
                result.Add(ActionCreator.SpinWaitLoop(Random(minLoopCountValue, maxLoopCountValue)));
            }

            return result;
        }

        private static int Random(int minValue, int maxValue)
        {
            return _rnd.Next(minValue, maxValue);
        }
    }
}
