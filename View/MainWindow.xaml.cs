using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Diagnostics;

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