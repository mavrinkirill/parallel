namespace CustomThreadPoolLibrary.CancellationToken
{
    public interface ICustomCancellationToken
    {
        void Cancel();

        bool IsCancellationRequested();
    }
}