using Logic;
using Data;
using System.Threading;
using Xunit;

namespace LogicTests
{
    public class BallLogicTests
    {
        private BallLogic ballLogic;
        private int width = 100;
        private int height = 100;

        public BallLogicTests()
        {
            ballLogic = new BallLogic(width, height);
        }

        [Fact]
        public void AddBall_Test()
        {
            Ball ball = new Ball(0, 0, 10);

            ballLogic.AddBall(ball);
          
            Thread.Sleep(50);
            Assert.True(ball.X >= 0 && ball.X < width);
            Assert.True(ball.Y >= 0 && ball.Y < height);
        }

        [Fact]
        public void IsValidPosition_Test()
        {
            int x1 = 50;
            int x2 = -10;
            int y = 50;
            int radius = 10;

            bool result = ballLogic.IsValidPosition(x1, y, radius);

            Assert.True(result);

            result = ballLogic.IsValidPosition(x2, y, radius);
        }

        [Fact]
        public void MoveBall_Test()
        {
            Ball ball = new Ball(50, 50, 10)
            {
                DeltaX = 1,
                DeltaY = 1
            };

            Thread thread = new Thread(() => ballLogic.MoveBall(ball));
            thread.Start();

            Thread.Sleep(50);

            Assert.NotEqual(50, ball.X);
            Assert.NotEqual(50, ball.Y);

        }
    }
}
