//using Data;
//using Logic;
//using System.Collections.Generic;

//namespace ModelTests
//{
//    public class TestLogicAPI : ILogicAPI
//    {
//        private readonly List<object> balls = new List<object>();

//        public IEnumerable<object> GetBalls()
//        {
//            return balls;
//        }

//        public void AddBall()
//        {
//            balls.Add(new { X = 100, Y = 100, Diameter = 20 }); // Simulated ball object
//        }

//        public void RemoveBall()
//        {
//            if (balls.Count > 0)
//            {
//                balls.RemoveAt(balls.Count - 1);
//            }
//        }

//        public void InitializeBall(IDataAPI ball, IEnumerable<Ball> balls) { }
//        public bool IsValidPosition(int x, int y, int diameter, IEnumerable<Ball> balls) => true;
//        public void MoveBall(IDataAPI ball, IEnumerable<Ball> balls) { }
//    }
//}
