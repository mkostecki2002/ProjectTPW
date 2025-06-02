using Common;

namespace Logger
{
    public interface ILogger
    {
        void Log(IDataAPI ball);
        void Log(string message);
        void Dispose();
    }
}
