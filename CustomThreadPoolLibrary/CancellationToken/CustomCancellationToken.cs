namespace CustomThreadPoolLibrary.CancellationToken
{
    public class CustomCancellationToken : ICustomCancellationToken
    {
        private bool isCanceled;

        public CustomCancellationToken()
        {
            isCanceled = false;
        }

        public void Cancel()
        {
            isCanceled = true;
        }

        public bool IsCancellationRequested()
        {
            return isCanceled;
        }
    }
}
