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
        private BallDisplay ballDisplay;
        public MainWindow()
        {
            ballDisplay = new BallDisplay(800, 600);
            InitializeComponent();
            DataContext = ballDisplay;
            BallModel model = new BallModel(10);
        }
    }
}
