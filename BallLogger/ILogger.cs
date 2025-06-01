using Common;

namespace Logger
{
    interface ILogger
    {
        void Log(IDataAPI ball);
        void Dispose();

    }
}
