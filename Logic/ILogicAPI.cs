using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface ILogicAPI
    {
        void InitializeBall(IDataAPI ball, IEnumerable<Ball> balls);
        bool IsValidPosition(int x, int y, int diameter, IEnumerable<Ball> balls);
        void MoveBall(IDataAPI ball, IEnumerable<Ball> balls);
        IEnumerable<object> GetBalls();
        void AddBall();
        void RemoveBall();

    }
}
