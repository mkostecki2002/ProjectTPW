using Data;

namespace Logic
{
    public class BallLogic
    {
        List<Ball> balls = new List<Ball>();
        List<Thread> threads = new List<Thread>();

        public void AddBall(Ball ball)
        {
            balls.Add(ball);
            Thread thread = new Thread(() => InitializeBall(ball));
            threads.Add(thread);
            thread.Start();
        }

        private void InitializeBall(Ball ball)
        {
            ball.X = new Random().Next(0, 100);
            ball.Y = new Random().Next(0, 100);
        }
    }
}