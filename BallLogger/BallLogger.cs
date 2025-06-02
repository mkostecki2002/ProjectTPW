using System.Collections.Concurrent;
using System.IO;
using System.Text;
using Common;

namespace Logger
{
    public class BallLogger : IDisposable, ILogger
    {
        private readonly BlockingCollection<string> _logQueue = new();
        private readonly Thread _logThread;
        private readonly string _filePath;
        private volatile bool _running = true;

        public BallLogger(string filePath)
        {
            _filePath = filePath;
            _logThread = new Thread(ProcessQueue) { IsBackground = true };
            _logThread.Start();
        }

        public void Log(IDataAPI ball)
        {
            string logEntry = $"{DateTime.Now:O};Position X = {ball.Position.X};Position Y = {ball.Position.Y};Velocity X = {ball.Velocity.X};Velocity Y = {ball.Velocity.Y};Diameter = {ball.Diameter}";
            _logQueue.Add(logEntry);
        }

        public void Log(string message)
        {
            string logEntry = $"{DateTime.Now:O};{message}";
            _logQueue.Add(logEntry);
        }

        private void ProcessQueue()
        {
            while (_running || !_logQueue.IsCompleted)
            {
                try
                {
                    if (_logQueue.TryTake(out var entry, Timeout.Infinite))
                    {
                        try
                        {
                            File.AppendAllText(_filePath, entry + Environment.NewLine, Encoding.ASCII);
                        }
                        catch (IOException)
                        {
                            _logQueue.Add(entry);
                            Thread.Sleep(100);
                        }
                    }
                }
                catch {  }
            }
        }

        public void Dispose()
        {
            _running = false;
            _logQueue.CompleteAdding();
            _logThread.Join();
        }
    }
}