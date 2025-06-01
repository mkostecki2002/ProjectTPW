using Data;
using Common;
using Logger;
using System.Diagnostics;

namespace Logic
{
    public class BallLogic : ILogicAPI
    {
        private readonly List<Thread> threads = new();
        private readonly int width;
        private readonly int height;
        private readonly Random random = new();
        private bool stopThreads = false;
        private readonly BallLogger ballLogger;

        public BallLogic(int width, int height, BallLogger ballLogger)
        {
            this.width = width;
            this.height = height;
            this.ballLogger = ballLogger;
        }

        public void InitializeBall(Ball ball, IEnumerable<Ball> balls)
        {
            bool positionFound = false;
            while (!positionFound)
            {
                double x = random.Next(50, width - 50);
                double y = random.Next(50, height - 50);
                Vector position = new(x, y);

                if (IsValidPosition(position, ball.Diameter, balls))
                {
                    ball.Position = position;
                    positionFound = true;
                }
            }

            Vector velocity = new(random.Next(-1, 2), random.Next(-1, 2));

            if (velocity.X == 0 && velocity.Y == 0)
            {
                velocity = new Vector(1, 1);
            }

            ball.Velocity = velocity;

            Thread thread = new Thread(() => MoveBall(ball, balls));

            threads.Add(thread);
            thread.Start();
        }

        public bool IsValidPosition(Vector position, double diameter, IEnumerable<Ball> balls)
        {
            double x = position.X;
            double y = position.Y;

            if (x < 0 || x + (diameter / 2) >= width || y < 0 || y + (diameter / 2) >= height)
            {
                return false;
            }

            lock (balls)
            {
                foreach (var otherBall in balls)
                {
                    if (otherBall == null) continue;

                    double distance = Math.Sqrt(
                        Math.Pow(x - otherBall.Position.X, 2) +
                        Math.Pow(y - otherBall.Position.Y, 2)
                    );

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
            Stopwatch sw = Stopwatch.StartNew();
            const int intervalMs = 16;

            while (!stopThreads)
            {
                long startMs = sw.ElapsedMilliseconds;

                double ballRadius = ball.Diameter / 2;

                Vector newPos = ball.Position + ball.Velocity;

                if (newPos.X - ballRadius < 0 || newPos.X + ballRadius >= width)
                    ball.Velocity = new Vector(-ball.Velocity.X, ball.Velocity.Y);

                if (newPos.Y - ballRadius < 0 || newPos.Y + ballRadius >= height)
                    ball.Velocity = new Vector(ball.Velocity.X, -ball.Velocity.Y);

                lock (balls)
                {
                    ball.Position += ball.Velocity;

                    foreach (var other in balls)
                    {
                        if (other == null || other == ball) continue;

                        double dx = ball.Position.X - other.Position.X;
                        double dy = ball.Position.Y - other.Position.Y;
                        double dist = Math.Sqrt(dx * dx + dy * dy);

                        double sumRadius = ball.Diameter / 2.0 + other.Diameter / 2.0;

                        if (dist < sumRadius && dist > 0.1)
                        {
                            Vector n = new Vector(dx, dy) / (int)dist;
                            Vector vRelative = ball.Velocity - other.Velocity;
                            double vDotN = vRelative.X * n.X + vRelative.Y * n.Y;

                            if (vDotN >= 0) continue;

                            double m1 = ball.Mass;
                            double m2 = other.Mass;

                            double impulse = (2 * vDotN) / (m1 + m2);

                            ball.Velocity -= n * (int)(impulse * m2);
                            other.Velocity += n * (int)(impulse * m1);
                        }
                    }
                }

                long endMs = sw.ElapsedMilliseconds;
                int workTime = (int)(endMs - startMs);


                int sleepTime = intervalMs - workTime;


                if (sleepTime > 0)
                {
                    Thread.Sleep(sleepTime);
                }
                else
                { 
                    ballLogger.Log("Time exceeded!");
                }
                ballLogger.Log(ball);
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
            int x = random.Next(0, width);
            int y = random.Next(0, height);
            int radius = random.Next(10, 50);
            Vector position = new Vector(x, y);
            Vector velocity = new Vector(random.Next(-1, 2), random.Next(-1, 2));
            return new Ball(position, velocity,  radius);
        }
    }
}
