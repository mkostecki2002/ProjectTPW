using Data;
using Common;

namespace Logic
{
    public interface ILogicAPI
    {
        void InitializeBall(Ball ball, IEnumerable<Ball> balls);
        bool IsValidPosition(Vector position, double diameter, IEnumerable<Ball> balls);
        void CheckBalls(IEnumerable<Ball> balls);

        (int, int) GetFieldBoundaries();

    }
}
