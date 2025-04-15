using System.Collections.ObjectModel;
using System.Diagnostics;
using Data;

namespace Logic
{
    public class BallLogic : ILogicAPI
    {
        public ObservableCollection<Ball> Balls = new ObservableCollection<Ball>();
        private readonly List<Thread> threads = new List<Thread>();
        private readonly int width;
        private readonly int height;
        private readonly Random random = new Random();
        private bool stopThreads = false; // Flag to stop threads


        public BallLogic(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void InitializeBall(Ball ball, IEnumerable<Ball> balls)
        {
            bool positionFound = false;
            while (!positionFound)
            {
                int x = random.Next(50, width-50);
                int y = random.Next(50, height-50);

                if (IsValidPosition(x, y, ball.Diameter, balls))
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

            Thread thread = new Thread(() => MoveBall(ball, balls));
            threads.Add(thread);
            thread.Start();
        }

        public bool IsValidPosition(int x, int y, int diameter, IEnumerable<Ball> balls)
        {
            if (x < 0 || x + (diameter / 2) >= width || y < 0 || y + (diameter / 2) >= height)
            {
                return false;
            }

            lock (balls)
            {
                foreach (var otherBall in balls)
                {
                    if (otherBall == null) continue;

                    double distance = Math.Sqrt(Math.Pow(x - otherBall.X, 2) + Math.Pow(y - otherBall.Y, 2));
                    if (distance < (diameter / 2) + (otherBall.Diameter / 2))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void MoveBall(Ball ball, IEnumerable<Ball> balls)
        {
            while (!stopThreads)
            {
                int newX, newY, ballRadius;
                List<Ball> ballsSnapshot;

                lock (balls)
                {
                    newX = ball.X + ball.DeltaX;
                    newY = ball.Y + ball.DeltaY;
                    ballRadius = ball.Diameter / 2;

                    bool isWallCollisionX = newX - ballRadius < 0 || newX + ballRadius >= width;
                    bool isWallCollisionY = newY - ballRadius < 0 || newY + ballRadius >= height;

                    if (isWallCollisionX || isWallCollisionY)
                    {
                        if (isWallCollisionX)
                            ball.DeltaX *= -1;
                        if (isWallCollisionY)
                            ball.DeltaY *= -1;
                    }
                    else
                    {
                        ball.X = newX;
                        ball.Y = newY;
                    }

                    ballsSnapshot = balls.ToList();
                }

                foreach (var otherBall in ballsSnapshot)
                {
                    if (otherBall == null || otherBall == ball)
                        continue;

                    double distance = Math.Sqrt(Math.Pow(newX - otherBall.X, 2) + Math.Pow(newY - otherBall.Y, 2));
                    if (distance < ballRadius + (otherBall.Diameter / 2))
                    {
                        ball.DeltaX *= -1;
                        ball.DeltaY *= -1;

                        lock (balls)
                        {
                            otherBall.DeltaX *= -1;
                            otherBall.DeltaY *= -1;
                        }
                        break;
                    }
                }
                Thread.Sleep(16);
            }
        }
        public void StopAllThreads()
        {
            stopThreads = true; 
            foreach (var thread in threads)
            {
                if (thread.IsAlive)
                {
                    thread.Join();
                }
            }
            threads.Clear(); 
            stopThreads = false; 
        }
        public void AddBall()
        {
            int x = Random.Shared.Next(0, 800);
            int y = Random.Shared.Next(0, 600);
            int radius = Random.Shared.Next(10, 50);
            Ball newBall = new Ball(x, y, radius);
            Balls.Add(newBall);
            InitializeBall(newBall, Balls);
        }


    }
}