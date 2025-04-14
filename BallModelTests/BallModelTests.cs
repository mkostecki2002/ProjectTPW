using Model;
using Data;
using System.Linq;
using Xunit;

namespace ModelTests
{
    public class BallModelTests
    {
        [Fact]
        public void Constructor_ShouldInitializeBallsCollection()
        {
            // Arrange & Act
            int numOfBalls = 5;
            BallModel ballModel = new BallModel(numOfBalls);

            // Assert
            Assert.NotNull(ballModel.Balls);
            Assert.Equal(numOfBalls, ballModel.Balls.Count);
        }

        [Fact]
        public void AddBall_ShouldAddBallToCollection()
        {
            // Arrange
            BallModel ballModel = new BallModel(0); // Start with no balls

            // Act
            ballModel.AddBall();

            // Assert
            Assert.Single(ballModel.Balls);
            Ball addedBall = ballModel.Balls.First();
            Assert.InRange(addedBall.X, 0, 800);
            Assert.InRange(addedBall.Y, 0, 600);
            Assert.InRange(addedBall.Diameter, 10, 50);
        }

        [Fact]
        public void RemoveBall_ShouldRemoveLastBallFromCollection()
        {
            // Arrange
            BallModel ballModel = new BallModel(3); // Start with 3 balls
            int initialCount = ballModel.Balls.Count;

            // Act
            ballModel.RemoveBall();

            // Assert
            Assert.Equal(initialCount - 1, ballModel.Balls.Count);
        }

        [Fact]
        public void RemoveBall_ShouldDoNothingIfCollectionIsEmpty()
        {
            // Arrange
            BallModel ballModel = new BallModel(0); // Start with no balls

            // Act
            ballModel.RemoveBall();

            // Assert
            Assert.Empty(ballModel.Balls); // Ensure no exception is thrown and collection remains empty
        }
    }
}
