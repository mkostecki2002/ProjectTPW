using Logic;
using Data;
using Common;
using Logger;

namespace LogicTests
{
    public class BallLogicTests : IDisposable
    {
        private readonly BallLogic ballLogic;
        private readonly int width = 100;
        private readonly int height = 100;
        private readonly string _logFilePath = "logtest.txt";
        private readonly BallLogger _logger = new BallLogger("logtest.txt");

        public BallLogicTests()
        {
            ballLogic = new BallLogic(width, height);
        }

        [Fact]
        public void IsValidPosition_True()
        {
            var balls = new List<Ball>
            {
                new Ball(new Vector(10, 10), new Vector(1, 1), 10, _logger)
            };
            var position = new Vector(30, 30);
            double diameter = 10;

            bool result = ballLogic.IsValidPosition(position, diameter, balls);

            Assert.True(result);
        }

        [Fact]
        public void IsValidPosition_False()
        {
            var balls = new List<Ball>
            {
                new Ball(new Vector(10, 10), new Vector(1, 1), 10, _logger)
            };
            var position = new Vector(10, 10);
            double diameter = 10;

            bool result = ballLogic.IsValidPosition(position, diameter, balls);

            Assert.False(result);
        }

        [Fact]
        public void MoveBall_BouncesOnWall()
        {
            var balls = new List<Ball>
            {
                new Ball(new Vector(95, 50), new Vector(10, 0), 10, _logger)
            };

            ballLogic.CheckBalls(balls);

            Assert.Equal(-10, balls[0].Velocity.X, 1);
            Assert.Equal(0, balls[0].Velocity.Y, 1);
        }

        [Fact]
        public void CheckBalls_TwoBalls_CollideAndChangeVelocity()
        {
            var ballA = new Ball(new Vector(50, 50), new Vector(1, 0), 10, _logger);
            var ballB = new Ball(new Vector(60, 50), new Vector(-1, 0), 10, _logger);
            var balls = new List<Ball> { ballA, ballB };

            ballA.Start();
            ballB.Start();

            ballLogic.CheckBalls(balls);

            Assert.Equal(-1, ballA.Velocity.X, 1);
            Assert.Equal(1, ballB.Velocity.X, 1);
            Assert.Equal(0, ballA.Velocity.Y, 1);
            Assert.Equal(0, ballB.Velocity.Y, 1);

        }


        public void Dispose()
        {
            _logger.Dispose();
            if (File.Exists(_logFilePath))
            {
                File.Delete(_logFilePath);
            }
        }
    }
}
