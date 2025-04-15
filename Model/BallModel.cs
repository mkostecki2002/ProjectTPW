using Data;
using Logic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Model
{
    public class BallModel
    {
        public ObservableCollection<Ball> Balls;
        private BallLogic ballLogic;


        public BallModel(int numOfBalls)
        {
            ballLogic = new BallLogic(800, 600);
            Balls = ballLogic.Balls;
            for (int i = 0; i < numOfBalls; i++)
            {
                ballLogic.AddBall();
            }
        }

        public void AddBall()
        {
            ballLogic.AddBall();
        }

        public void RemoveBall()
        {
            if (Balls.Count > 0)
            {
                Ball ballToRemove = Balls.Last();
                Balls.Remove(ballToRemove);
            }
        }
        public void StopAllThreads()
        {
            ballLogic.StopAllThreads();
        }
    }
}
