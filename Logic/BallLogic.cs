using Data;

namespace Logic
{
    public class BallLogic : ILogicAPI
    {
        private readonly List<Thread> threads = new List<Thread>();
        private readonly int width;
        private readonly int height;
        private readonly Random random = new Random();
        private bool stopThreads = false;


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
                int newX = ball.X + ball.DeltaX;
                int newY = ball.Y + ball.DeltaY;
                int ballRadius = ball.Diameter / 2;

                bool isWallCollisionX = newX - ballRadius < 0 || newX + ballRadius >= width;
                bool isWallCollisionY = newY - ballRadius < 0 || newY + ballRadius >= height;

                if (isWallCollisionX) ball.DeltaX *= -1;
                if (isWallCollisionY) ball.DeltaY *= -1;

                if (!isWallCollisionX && !isWallCollisionY)
                {
                    lock(balls)
                    {
                        ball.X = newX;
                        ball.Y = newY;
                    }
                }

                foreach (var otherBall in balls)
                {
                    if (otherBall == null || otherBall == ball)
                        continue;

                    double distance = Math.Sqrt(Math.Pow(ball.X - otherBall.X, 2) + Math.Pow(ball.Y - otherBall.Y, 2));
                    if (distance < ballRadius + (otherBall.Diameter / 2))
                    {
                        lock (balls) 
                        { 
                            ball.DeltaX *= -1;
                            ball.DeltaY *= -1;
                            otherBall.DeltaX *= -1;
                            otherBall.DeltaY *= -1;
                            break;
                        }
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
        public Ball CreateBall()
        {
            int x = Random.Shared.Next(0, width);
            int y = Random.Shared.Next(0, height);
            int radius = Random.Shared.Next(10, 50);
            Ball newBall = new Ball(x, y, radius);
            return newBall;
        }
    }
}