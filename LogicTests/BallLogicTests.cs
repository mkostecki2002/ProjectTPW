using Logic;
using Data;
using Common;

namespace LogicTests
{
    public class BallLogicTests
    {
        private readonly BallLogic ballLogic;
        private readonly int width = 100;
        private readonly int height = 100;
        private readonly List<Ball> balls;

        public BallLogicTests()
        {
            ballLogic = new BallLogic(width, height);
            balls = new List<Ball>
            {
                new Ball(new Vector(10, 10), new Vector(1, 1), 10),
                new Ball(new Vector(30, 30), new Vector(1, 1), 10),
                new Ball(new Vector(50, 50), new Vector(1, 1), 10)
            };
        }

        [Fact]
        public void IsValidPosition_True()
        {
            var position = new Vector(20, 20);
            double diameter = 10;

            bool result = ballLogic.IsValidPosition(position, diameter, balls);

            Assert.True(result);
        }

        [Fact]
        public void IsValidPosition_False()
        {
            var position = new Vector(-10, 20); 
            double diameter = 10;

            bool result = ballLogic.IsValidPosition(position, diameter, balls);

            Assert.False(result);
        }

        [Fact]
        public void MoveBall()
        {
            Ball ball = new Ball(new Vector(50, 50), new Vector(1, 1), 10);
            balls.Add(ball);

            Thread thread = new Thread(() => ballLogic.MoveBall(ball, balls));
            thread.Start();

            Thread.Sleep(50);

            Assert.NotEqual(50, ball.Position.X);
            Assert.NotEqual(50, ball.Position.Y);
        }

        [Fact]
        public void MoveBall_Collision()
        {
            Ball ball = new Ball(new Vector(95, 50), new Vector(1, 0), 10); 

            Thread thread = new Thread(() => ballLogic.MoveBall(ball, balls));
            thread.Start();

            Thread.Sleep(50);

            Assert.True(ball.Velocity.X == -1); 
            Assert.True(ball.Velocity.Y == 0);  
        }
    }
}
