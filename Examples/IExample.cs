namespace Examples
{
    public interface IExample
    {
        int ThreadsCount { get; }

        void Work();

        void MenuCommandProcessor(int command);
    }
}
