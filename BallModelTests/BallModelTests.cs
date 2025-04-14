//using Model;
//using Logic;
//using System.Linq;
//using Xunit;

//namespace ModelTests
//{
//    public class BallModelTests
//    {
//        [Fact]
//        public void Constructor_ShouldInitializeBallsCollection()
//        {
//            // Arrange
//            int numOfBalls = 5;
//            ILogicAPI ILogicAPITests = new iLogicAPITests();

//            // Act
//            BallModel ballModel = new BallModel(numOfBalls, testLogicAPI);

//            // Assert
//            Assert.NotNull(ballModel.Balls);
//            Assert.Equal(numOfBalls, ballModel.Balls.Count);
//        }

//        [Fact]
//        public void AddBall_ShouldAddBallToCollection()
//        {
//            // Arrange
//            ILogicAPI testLogicAPI = new TestLogicAPI();
//            BallModel ballModel = new BallModel(0, testLogicAPI); // Start with no balls

//            // Act
//            ballModel.AddBall();

//            // Assert
//            Assert.Single(ballModel.Balls);
//        }

//        [Fact]
//        public void RemoveBall_ShouldRemoveLastBallFromCollection()
//        {
//            // Arrange
//            ILogicAPI testLogicAPI = new TestLogicAPI();
//            BallModel ballModel = new BallModel(3, testLogicAPI); // Start with 3 balls
//            int initialCount = ballModel.Balls.Count;

//            // Act
//            ballModel.RemoveBall();

//            // Assert
//            Assert.Equal(initialCount - 1, ballModel.Balls.Count);
//        }

//        [Fact]
//        public void RemoveBall_ShouldDoNothingIfCollectionIsEmpty()
//        {
//            // Arrange
//            ILogicAPI testLogicAPI = new TestLogicAPI();
//            BallModel ballModel = new BallModel(0, testLogicAPI); // Start with no balls

//            // Act
//            ballModel.RemoveBall();

//            // Assert
//            Assert.Empty(ballModel.Balls); // Ensure no exception is thrown and collection remains empty
//        }
//    }
//}
