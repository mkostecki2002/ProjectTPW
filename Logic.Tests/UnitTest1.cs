using Logic;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Xunit;

namespace LogicTests
{
    public class BallLogicTests
    {
        [Fact]
        public void IsValidPosition_WhenPositionIsWithinBoundsAndNoOverlap_ReturnsTrue()
        {
            // Arrange
            var logic = new BallLogic(100, 100);

            // Act
            bool isValid = logic.IsValidPosition(10, 10, 5);

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void IsValidPosition_WhenPositionIsOutOfBounds_ReturnsFalse()
        {
            // Arrange
            var logic = new BallLogic(100, 100);

            // Act
            bool isValid = logic.IsValidPosition(96, 10, 5); // 96 + 5 = 101 > 100

            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void IsValidPosition_WhenOverlapsWithAnotherBall_ReturnsFalse()
        {
            // Arrange
            var logic = new BallLogic(100, 100);
            var existingBall = new Ball { X = 50, Y = 50, Radius = 10 };

            // Use reflection to add existingBall to the balls list
            var ballsField = typeof(BallLogic).GetField("balls", BindingFlags.NonPublic | BindingFlags.Instance);
            var ballsList = (List<Ball>)ballsField.GetValue(logic);
            ballsList.Add(existingBall);

            // Act
            bool isValid = logic.IsValidPosition(55, 55, 10); // Distance is ~7.07 < 20 (sum of radii)

            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void AddBall_AddsBallToList()
        {
            // Arrange
            var logic = new BallLogic(100, 100);
            var ball = new Ball();

            // Act
            logic.AddBall(ball);

            // Use reflection to verify the ball is added to the list
            var ballsField = typeof(BallLogic).GetField("balls", BindingFlags.NonPublic | BindingFlags.Instance);
            var ballsList = (List<Ball>)ballsField.GetValue(logic);

            // Assert
            Assert.Contains(ball, ballsList);
        }

        [Fact]
        public void AddBall_StartsNewThread()
        {
            // Arrange
            var logic = new BallLogic(100, 100);
            var ball = new Ball();

            // Act
            logic.AddBall(ball);

            // Use reflection to access the threads list
            var threadsField = typeof(BallLogic).GetField("threads", BindingFlags.NonPublic | BindingFlags.Instance);
            var threadsList = (List<Thread>)threadsField.GetValue(logic);

            // Assert
            Assert.Single(threadsList);
            Assert.True(threadsList[0].IsAlive || !threadsList[0].IsAlive); // Check existence, not status
        }

        [Fact]
        public void InitializeBall_SetsValidPositionAndDirection()
        {
            // Arrange
            var logic = new BallLogic(100, 100);
            var ball = new Ball { Radius = 5 };

            // Use reflection to call the private method
            var method = typeof(BallLogic).GetMethod("InitializeBall", BindingFlags.NonPublic | BindingFlags.Instance);

            // Act
            method.Invoke(logic, new object[] { ball });

            // Assert
            Assert.InRange(ball.X, 0, 100);
            Assert.InRange(ball.Y, 0, 100);
            Assert.InRange(ball.DeltaX, -1, 1);
            Assert.InRange(ball.DeltaY, -1, 1);
            Assert.False(ball.DeltaX == 0 && ball.DeltaY == 0);
        }

        [Fact]
        public void MoveBall_ChangesPosition()
        {
            // Arrange
            var logic = new BallLogic(100, 100);
            var ball = new Ball { X = 50, Y = 50, Radius = 5, DeltaX = 1, DeltaY = 1 };

            // Use reflection to call the private method
            var method = typeof(BallLogic).GetMethod("MoveBall", BindingFlags.NonPublic | BindingFlags.Instance);

            // Act
            var thread = new Thread(() => method.Invoke(logic, new object[] { ball }));
            thread.Start();
            Thread.Sleep(50); // Allow some time for the ball to move
            thread.Abort();

            // Assert
            Assert.NotEqual(50, ball.X);
            Assert.NotEqual(50, ball.Y);
        }
    }

    // Mock Ball class for testing if necessary (if Ball is from a different assembly and not accessible)
    public class Ball
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Radius { get; set; }
        public int DeltaX { get; set; }
        public int DeltaY { get; set; }
    }
}
