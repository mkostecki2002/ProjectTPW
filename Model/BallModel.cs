using Logic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Model
{
    public class BallModel
    {
        public ObservableCollection<object> Balls { get; set; }
        private ILogicAPI ballLogic;


        public BallModel(int numOfBalls, ILogicAPI logicAPI)
        {
            ballLogic = logicAPI;
            //ballLogic = new BallLogic(800, 600);
            Balls = new ObservableCollection<object>(ballLogic.GetBalls());
            for (int i = 0; i < numOfBalls; i++)
            {
                AddBall();
            }
        }

        public void AddBall()
        {
            ballLogic.AddBall();
            RefreshBalls();
        }

        public void RemoveBall()
        {
            ballLogic.RemoveBall();
            RefreshBalls();
        }

        private void RefreshBalls()
        {
            Balls.Clear();
            foreach (var ball in ballLogic.GetBalls())
            {
                Balls.Add(ball);
            }
        }

    }
}
