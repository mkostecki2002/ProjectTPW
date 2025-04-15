using System.Windows;

using ViewModel;

namespace View
{
    public partial class MainWindow : Window
    {
        private BallDisplay ballDisplay;
        public MainWindow()
        {
            InitializeComponent();
            ballDisplay = new BallDisplay(800, 600);
            DataContext = ballDisplay;
        } 
    }
}
