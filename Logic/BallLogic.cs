using Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Logic
{
    public class BallLogic
    {
        private readonly List<Ball> balls = new List<Ball>();
        private readonly List<Thread> threads = new List<Thread>();
        private readonly int width;
        private readonly int height;
        private readonly Random random = new Random();

        public BallLogic(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void AddBall(Ball ball)
        {
            lock (balls)
            {
                balls.Add(ball);
            }
            Thread thread = new Thread(() => InitializeBall(ball));
            threads.Add(thread);
            thread.Start();
        }

        private void InitializeBall(Ball ball)
        {
            bool positionFound = false;
            while (!positionFound)
            {
                int x = random.Next(0, width);
                int y = random.Next(0, height);

                if (IsValidPosition(x, y, ball.Radius))
                {
                    ball.X = x;
                    ball.Y = y;
                    positionFound = true;
                }
            }

            ball.DeltaX = random.Next(-1, 2);
            ball.DeltaY = random.Next(-1, 2);

            if (ball.DeltaX == 0 && ball.DeltaY == 0)
            {
                ball.DeltaX = 1;
            }

            MoveBall(ball);
        }

        public bool IsValidPosition(int x, int y, int radius)
        {
            if (x < 0 || x + radius >= width || y < 0 || y + radius >= height)
            {
                return false;
            }

            lock (balls)
            {
                foreach (var otherBall in balls)
                {
                    if (otherBall == null || otherBall == balls[balls.Count - 1]) continue;

                    double distance = Math.Sqrt(Math.Pow(x - otherBall.X, 2) + Math.Pow(y - otherBall.Y, 2));
                    if (distance < radius + otherBall.Radius)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void MoveBall(Ball ball)
        {
            while (true)
            {
                lock (balls)
                {
                    int newX = ball.X + ball.DeltaX;
                    int newY = ball.Y + ball.DeltaY;

                    bool isWallCollisionX = newX - ball.Radius < 0 || newX + ball.Radius > width;
                    bool isWallCollisionY = newY - ball.Radius < 0 || newY + ball.Radius > height;

                    if (isWallCollisionX || isWallCollisionY)
                    {
                        if (isWallCollisionX)
                            ball.DeltaX *= -1;
                        if (isWallCollisionY)
                            ball.DeltaY *= -1;
                    }
                    else
                    {
                        bool collisionDetected = false;
                        foreach (var otherBall in balls)
                        {
                            if (otherBall == null || otherBall == ball)
                                continue;

                            double distance = Math.Sqrt(Math.Pow(newX - otherBall.X, 2) + Math.Pow(newY - otherBall.Y, 2));
                            if (distance < ball.Radius + otherBall.Radius)
                            {
                                collisionDetected = true;

                                ball.DeltaX *= -1;
                                ball.DeltaY *= -1;

                                otherBall.DeltaX *= -1;
                                otherBall.DeltaY *= -1;
                                break;
                            }
                        }

                        if (!collisionDetected)
                        {
                            ball.X = newX;
                            ball.Y = newY;
                        }
                    }

                    //Debug.WriteLine($"Moved Ball: X={ball.X}, Y={ball.Y}, DeltaX={ball.DeltaX}, DeltaY={ball.DeltaY}");
                    Thread.Sleep(5);
                }
            }
        }
    }
}