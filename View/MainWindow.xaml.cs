using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ViewModel;
using Data;
using System.Diagnostics;

namespace View
{
    public partial class MainWindow : Window
    {
        private BallDisplay viewModel;

        public MainWindow()
        {
            InitializeComponent();
            viewModel = new BallDisplay(800, 450); // Pass the width, height, and Dispatcher
            DataContext = viewModel;

            foreach (var ball in viewModel.Balls)
            {
                Ellipse ellipse = new Ellipse
                {
                    Width = ball.Radius * 2,
                    Height = ball.Radius * 2,
                    Fill = Brushes.Red
                };
                BallCanvas.Children.Add(ellipse);
                UpdateBallPosition(ellipse, ball);
            }

            CompositionTarget.Rendering += OnRendering;
        }

        private void OnRendering(object sender, EventArgs e)
        {
            foreach (var ball in viewModel.Balls)
            {
                Ellipse ellipse = (Ellipse)BallCanvas.Children[viewModel.Balls.IndexOf(ball)];
                UpdateBallPosition(ellipse, ball);
            }
        }

        private void UpdateBallPosition(Ellipse ellipse, Ball ball)
        {
            Canvas.SetLeft(ellipse, ball.X - ball.Radius);
            Canvas.SetTop(ellipse, ball.Y - ball.Radius);
        }
    }

}
