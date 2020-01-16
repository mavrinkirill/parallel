using System.Collections.Generic;

namespace CustomThreadPoolLibrary.Task
{
    public static class CustomTaskSettings
    {
        public static HashSet<CustomTaskStatus> CompletedStatuses => new HashSet<CustomTaskStatus>() { CustomTaskStatus.Completed, CustomTaskStatus.Exception, CustomTaskStatus.Interrupted };
    }
}
