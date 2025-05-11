using Data;
using Logic;
using System.Collections.ObjectModel;

namespace Model
{
    public class BallModel : IModelAPI
    {
        public ObservableCollection<Ball> Balls { get; set; } 
        private ILogicAPI ballLogic;

        public BallModel(ILogicAPI ballLogic, int numOfBalls)
        {
            this.ballLogic = ballLogic;
            Balls = new ObservableCollection<Ball>();
            for (int i = 0; i < numOfBalls; i++)
            {
                AddBall();
            }
        }

        public void AddBall()
        {
            Ball newBall = ballLogic.CreateBall();
            Balls.Add(newBall);
            ballLogic.InitializeBall(newBall, Balls);
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
