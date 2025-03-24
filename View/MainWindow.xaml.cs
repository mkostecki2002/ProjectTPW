using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ViewModel;
using Data;

namespace View
{
    public partial class MainWindow : Window
    {
        private BallDisplay viewModel;

        public MainWindow()
        {
            InitializeComponent();
            this.viewModel = new ViewModel.BallDisplay(); // Properly initialize viewModel
            DataContext = viewModel;

            ObservableCollection<Ball> Balls = viewModel.Balls;

            Canvas canvas = new Canvas();
            canvas.Children.Clear();
            foreach (var ball in Balls)
            {
                Ellipse ellipse = new Ellipse
                {
                    Width = ball.Radius * 2,
                    Height = ball.Radius * 2,
                    Fill = Brushes.Red
                };
                Canvas.SetLeft(ellipse, ball.X);
                Canvas.SetTop(ellipse, ball.Y);
                canvas.Children.Add(ellipse);
            }

            // Add the canvas to the window's content
            this.Content = canvas;
        }

    }

}
