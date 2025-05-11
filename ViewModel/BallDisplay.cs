using System.Collections.ObjectModel;
using System.Windows.Input;
using Logic;
using Model;

namespace ViewModel
{
    public class BallDisplay
    {
        private int ballsCount = 4;
        private BallModel ballModel;

        public ObservableCollection<Data.Ball> Balls { get; set; }

        public ICommand UpdateBallCountCommand { get; }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return parameter is string text && !string.IsNullOrWhiteSpace(text);
        }

        public void Execute(object? parameter) 
        {
            if (parameter is string text && int.TryParse(text, out int newCount))
            {
                if (newCount > 0)
                {

                    ballModel.Balls.Clear();
                    ballModel.StopAllThreads();
                    ballsCount = newCount;
                    for (int i = 0; i < newCount; i++)
                    {
                        ballModel.AddBall();
                    }
                }
                
            }
        }

        public BallDisplay(int width, int height)
        {
            BallLogic ballLogic = new BallLogic(width, height);
            ballModel = new BallModel(ballLogic, ballsCount);
            Balls = ballModel.Balls;
            UpdateBallCountCommand = new RelayCommand(Execute, CanExecute);
        }
    }

    internal class RelayCommand : ICommand
    {
        private readonly Action<object?> execute;
        private readonly Predicate<object?> canExecute;
        public RelayCommand(Action<object?> execute, Predicate<object?> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        public bool CanExecute(object? parameter)
        {
            return canExecute(parameter);
        }
        public void Execute(object? parameter)
        {
            execute(parameter);
        }
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

    }

    internal class CommandManager
    {
        public static event EventHandler? RequerySuggested;
        public static void InvalidateRequerySuggested()
        {
            RequerySuggested?.Invoke(null, EventArgs.Empty);
        }
    }
}
