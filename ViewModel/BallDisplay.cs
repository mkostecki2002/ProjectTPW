using System.Collections.ObjectModel;
using Model;

namespace ViewModel
{
    public class BallDisplay
    {
        private BallModel ballModel;
        public ObservableCollection<Data.Ball> Balls => ballModel.Balls;

        public BallDisplay(int width, int height)
        {
            ballModel = new BallModel(4);
        }
    }

    
}
