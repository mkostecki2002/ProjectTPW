using System.Collections.ObjectModel;

using Data;
using Logic;

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
                int x = Random.Shared.Next(0, 800);
                int y = Random.Shared.Next(0, 600);
                int radius = Random.Shared.Next(10, 50);
                Balls.Add(new Ball(x, y, radius));
                ballLogic.InitializeBall(Balls[i], Balls);
            }
        }
    }

}
