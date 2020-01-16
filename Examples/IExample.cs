namespace Examples
{
    public interface IExample
    {
        int ThreadsCount { get; }

        void Initialization();

        void Work();

        void MenuCommandProcessor(int command);
    }
}
