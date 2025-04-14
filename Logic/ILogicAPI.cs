using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    interface ILogicAPI
    {
        void InitializeBall(Ball ball, IEnumerable<Ball> balls);
        bool IsValidPosition(int x, int y, int diameter, IEnumerable<Ball> balls);
        void MoveBall(Ball ball, IEnumerable<Ball> balls);
        
    }
}
