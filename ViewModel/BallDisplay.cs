using System.Collections.ObjectModel;
using Data;
using Logic;

namespace ViewModel
{
    public class BallDisplay
    {
        private Logic.BallLogic ballLogic;

        public BallDisplay(int width, int height)
        {
            ballLogic = new BallLogic(width, height);
            Balls = new ObservableCollection<Ball>();
            Ball ball = new Ball(10, 20, 15);
            Ball ball2 = new Ball(30, 40, 25);
            Balls.Add(ball);
            Balls.Add(ball2);
            ballLogic.AddBall(ball);
            ballLogic.AddBall(ball2);
        }

        public ObservableCollection<Ball> Balls { get; set; }
    }
}
