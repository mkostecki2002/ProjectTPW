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
        List<Ball> balls = new List<Ball>();

        public BallLogicTests()
        {
            ballLogic = new BallLogic(width, height);
            for (int i = 0; i < 3; i++)
            {
                balls.Add(new Ball(i*2+1, i*2+1, 1));
            }
        }

        //[Fact]
        //public void AddBall_Test()
        //{
        //    Ball ball = new Ball(0, 0, 10);

        //    ballLogic.AddBall(ball);
          
        //    Thread.Sleep(50);
        //    Assert.True(ball.X >= 0 && ball.X < width);
        //    Assert.True(ball.Y >= 0 && ball.Y < height);
        //}

        //[Fact]
        //public void IsValidPosition_Test()
        //{
        //    int x1 = 50;
        //    int x2 = -10;
        //    int y = 50;
        //    int radius = 10;

        //    bool result = ballLogic.IsValidPosition(x1, y, radius);

        //    Assert.True(result);

        //    result = ballLogic.IsValidPosition(x2, y, radius);

        //    Assert.False(result);
        //}

        [Fact]
        public void MoveBall_Test()
        {
            Ball ball = new Ball(795, 50, 10)
            {
                DeltaX = 1,
                DeltaY = 1
            };

            Thread thread = new Thread(() => ballLogic.MoveBall(ball, balls));
            thread.Start();

            Thread.Sleep(50);

            Assert.NotEqual(50, ball.X);
            Assert.NotEqual(50, ball.Y);

        }
        [Fact]
        public void MoveBall_WallCollision_Test()
        {
            // Arrange: Create a ball near the right wall
            Ball ball = new Ball(95, 50, 10) // Ball's diameter is 10, so radius is 5
            {
                DeltaX = 1, // Moving right
                DeltaY = 0  // No vertical movement
            };

            // Act: Start the MoveBall method in a separate thread
            Thread thread = new Thread(() => ballLogic.MoveBall(ball, balls));
            thread.Start();

            // Allow some time for the ball to move and collide
            Thread.Sleep(50);

            // Assert: Check if the ball reversed its horizontal direction
            Assert.Equal(-1, ball.DeltaX); // Ball should reverse direction on X-axis
            Assert.Equal(0, ball.DeltaY);  // Y-axis movement should remain unchanged

        }

    }
}
