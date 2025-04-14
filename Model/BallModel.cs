using Data;
using Logic;
using System.Collections.ObjectModel;

namespace Model
{
    public class BallModel
    {
        public ObservableCollection<Ball> Balls { get; set; }
        private BallLogic ballLogic;

        public BallModel(int numOfBalls)
        {
            ballLogic = new BallLogic(800, 600);
            Balls = new ObservableCollection<Ball>();
            for (int i = 0; i < numOfBalls; i++)
            {
                AddBall();
            }
        }

        public void AddBall()
        {
            int x = Random.Shared.Next(0, 800);
            int y = Random.Shared.Next(0, 600);
            int radius = Random.Shared.Next(10, 50);
            Ball newBall = new Ball(x, y, radius);
            Balls.Add(newBall);
            ballLogic.InitializeBall(newBall, Balls);
        }

        public void RemoveBall()
        {
            if (Balls.Count > 0)
            {
                Ball ballToRemove = Balls.Last();
                Balls.Remove(ballToRemove);
                // Optionally, stop the thread for the removed ball
            }
        }
    }
}
