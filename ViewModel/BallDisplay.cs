using System.Collections.ObjectModel;
using Model;

namespace ViewModel
{
    public class BallDisplay
    {
        private BallModel ballModel;

        public BallDisplay(int width, int height)
        {
            ballModel = new BallModel(10);

        }

    }
}
