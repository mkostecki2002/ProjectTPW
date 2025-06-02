using Data;
using Common;

namespace Logic
{
    public class BallLogic : ILogicAPI
    {
        private readonly int width;
        private readonly int height;
        private readonly Random random = new();

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

            ball.RequestBounce(velocity);
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

        public void CheckBalls(IEnumerable<Ball> balls)
        {
            balls = balls.ToList();
            foreach (var ballA in balls)
            {
                foreach (var ballB in balls)
                {
                    Vector newPos = ballB.Position + ballB.Velocity;
                    double ballRadius = ballB.Diameter / 2.0;

                    if (newPos.X - ballRadius < 0 || newPos.X + ballRadius >= width)
                        ballB.RequestBounce(new Vector(-ballB.Velocity.X, ballB.Velocity.Y));

                    if (newPos.Y - ballRadius < 0 || newPos.Y + ballRadius >= height)
                        ballB.RequestBounce(new Vector(ballB.Velocity.X, -ballB.Velocity.Y));

                    if (ballA == null || ballB == null || ballA == ballB) continue;
                    if (IsColliding(ballA, ballB))
                        handleCollision(ballA, ballB);
                }
            }
        }


        private void handleCollision(Ball ballA, Ball ballB)
        {
            var (dx, dy, dist) = calculateDistance(ballA.Position, ballB.Position);

            if (dist == 0) return;

            double overlap = ((ballA.Diameter / 2.0) + (ballB.Diameter / 2.0)) - dist;
            Vector n = new Vector(dx, dy) / dist;
            if (overlap > 0)
            {
                ballA.Position += n * (overlap / 2.0);
                ballB.Position -= n * (overlap / 2.0);
            }

            Vector vRelative = ballA.Velocity - ballB.Velocity;
            double vDotN = vRelative.X * n.X + vRelative.Y * n.Y;

            if (vDotN >= 0) return;

            double m1 = ballA.Mass;
            double m2 = ballB.Mass;

            double impulse = (2 * vDotN) / (m1 + m2);

            Vector velA = ballA.Velocity - n * (int)(impulse * m2);
            Vector velB = ballB.Velocity + n * (int)(impulse * m1);

            ballA.RequestBounce(velA);
            ballB.RequestBounce(velB);

        }

        private (double, double, double) calculateDistance(Vector posA, Vector posB)
        {
            double dx = posA.X - posB.X;
            double dy = posA.Y - posB.Y;
            double distance = Math.Sqrt(dx * dx + dy * dy);
            return (dx, dy, distance);
        }

        private bool IsColliding(Ball ballA, Ball ballB)
        {
            var dist = calculateDistance(ballA.Position, ballB.Position);
            double distance = dist.Item3;
            double sumRadius = (ballA.Diameter / 2.0) + (ballB.Diameter / 2.0);
            return distance < sumRadius;
        }


        public (int, int) GetFieldBoundaries()
        {
            return (width, height);
        }
    }
}
