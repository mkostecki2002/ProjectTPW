using System.Collections.ObjectModel;
using System.Windows.Input;
using Logic;
using Model;

namespace ViewModel
{
    public class BallDisplay : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter)
        {
         
        }
        public void Refresh()
        {
            // Logic to refresh the display
        }
        public void Stop()
        {
            // Logic to stop the display
        }

        private BallModel ballModel;
        public ObservableCollection<Data.Ball> Balls
        {
            get
            {
                return new ObservableCollection<Data.Ball>(
                    ballModel.Balls.OfType<Data.Ball>()
                );
            }
        }
        public BallDisplay(int width, int height)
        {

            ILogicAPI logicAPI = new BallLogic(width, height); 
            ballModel = new BallModel(1, logicAPI);
        }
        public void UpdateBallCount(int newCount)
        {
            int currentCount = Balls.Count;

            if (newCount > currentCount)
            {
                // Add new balls
                for (int i = 0; i < newCount - currentCount; i++)
                {
                    ballModel.AddBall();
                }
            }
            else if (newCount < currentCount)
            {
                // Remove excess balls
                for (int i = 0; i < currentCount - newCount; i++)
                {
                    ballModel.RemoveBall();
                }
            }
        }
    
    }
}
