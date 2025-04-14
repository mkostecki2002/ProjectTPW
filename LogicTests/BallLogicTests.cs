using Logic;
using Data;

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
            // Injecting dependencies manually
            ballLogic = new BallLogic(width, height);
            balls = new List<Ball>
            {
                new Ball(10, 10, 10),
                new Ball(30, 30, 10),
                new Ball(50, 50, 10)
            };
        }

        [Fact]
        public void InitializeBall_ShouldSetValidPositionAndStartThread()
        {
            // Arrange
            Ball ball = new Ball(0, 0, 10);

            // Act
            ballLogic.InitializeBall(ball, balls);

            // Allow some time for the thread to start
            Thread.Sleep(50);

            // Assert
            Assert.True(ball.X >= 0 && ball.X < width);
            Assert.True(ball.Y >= 0 && ball.Y < height);
        }

        [Fact]
        public void IsValidPosition_ShouldReturnTrueForValidPosition()
        {
            // Arrange
            int x = 20;
            int y = 20;
            int diameter = 10;

            // Act
            bool result = ballLogic.IsValidPosition(x, y, diameter, balls);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValidPosition_ShouldReturnFalseForInvalidPosition()
        {
            // Arrange
            int x = -10; // Out of bounds
            int y = 20;
            int diameter = 10;

            // Act
            bool result = ballLogic.IsValidPosition(x, y, diameter, balls);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void MoveBall_ShouldUpdateBallPosition()
        {
            // Arrange
            Ball ball = new Ball(50, 50, 10)
            {
                DeltaX = 1,
                DeltaY = 1
            };
            balls.Add(ball);
            // Act
            Thread thread = new Thread(() => ballLogic.MoveBall(ball, balls));
            thread.Start();

            // Allow some time for the ball to move
            Thread.Sleep(50);

            // Assert
            Assert.NotEqual(50, ball.X);
            Assert.NotEqual(50, ball.Y);
        }

        [Fact]
        public void MoveBall_ShouldReverseDirectionOnWallCollision()
        {
            // Arrange
            Ball ball = new Ball(95, 50, 10) // Near the right wall
            {
                DeltaX = 1, // Moving right
                DeltaY = 0  // No vertical movement
            };

            // Act
            Thread thread = new Thread(() => ballLogic.MoveBall(ball, balls));
            thread.Start();

            // Allow some time for the ball to collide with the wall
            Thread.Sleep(50);

            // Assert
            Assert.Equal(-1, ball.DeltaX); // Ball should reverse direction on X-axis
            Assert.Equal(0, ball.DeltaY);  // Y-axis movement should remain unchanged
        }
    }
}
