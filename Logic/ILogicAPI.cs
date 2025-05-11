using Data;

namespace Logic
{
    public interface ILogicAPI
    {
        void InitializeBall(Ball ball, IEnumerable<Ball> balls);
        bool IsValidPosition(int x, int y, int diameter, IEnumerable<Ball> balls);
        void MoveBall(Ball ball, IEnumerable<Ball> balls);

        Ball CreateBall();

        void StopAllThreads();

    }
}
