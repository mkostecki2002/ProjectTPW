using Data;
using Logic;
using Logger;
using Common;
using System;
using System.Collections.ObjectModel;

namespace Model
{
    public class BallModel : IModelAPI
    {
        public ObservableCollection<Ball> Balls { get; set; }
        private ILogicAPI ballLogic;
        private Random random = new Random();
        private ILogger logger;
        private readonly Timer _timerBalls;
        private readonly Timer _timerCollisions;

        public BallModel(ILogicAPI ballLogic, int numOfBalls, ILogger logger)
        {
            this.ballLogic = ballLogic;
            Balls = new ObservableCollection<Ball>();
            this.logger = logger;
            for (int i = 0; i < numOfBalls; i++)
            {
                AddBall();
            }

            _timerCollisions = new Timer(_ => ballLogic.CheckBalls(Balls), null, 0, 16);
        }

        public void AddBall()
        {
            int width, height;
            (width, height) = ballLogic.GetFieldBoundaries();
            int x = random.Next(0, width);
            int y = random.Next(0, height);
            int radius = random.Next(10, 50);
            Vector position = new Vector(x, y);
            Vector velocity = new Vector(random.Next(-1, 2), random.Next(-1, 2));
            Ball newBall = new Ball(position, velocity, radius, logger);
            Balls.Add(newBall);
            ballLogic.InitializeBall(newBall, Balls);
            newBall.Start();
        }

        public void StopAllThreads()
        {
            for (int i = 0; i < Balls.Count; i++)
            {
                Balls[i].Stop();
                Balls.RemoveAt(i);
            }
        }
    }
}
