using System.ComponentModel;

namespace Data
{
    public class Ball : INotifyPropertyChanged
    {
        private int x;
        private int y;
        private int diameter;

        public int X
        {
            get => x;
            set
            {
                if (x != value)
                {
                    x = value;
                    OnPropertyChanged(nameof(X));
                }
            }
        }

        public int Y
        {
            get => y;
            set
            {
                if (y != value)
                {
                    y = value;
                    OnPropertyChanged(nameof(Y));
                }
            }
        }

        public int Diameter
        {
            get => diameter;
            set
            {
                if (diameter != value)
                {
                    diameter = value;
                    OnPropertyChanged(nameof(Diameter));
                }
            }
        }
        public int DeltaX { get; set; }
        public int DeltaY { get; set; }

        public Ball(int x, int y, int diameter)
        {
            X = x;
            Y = y;
            Diameter = diameter;
            DeltaX = 1;
            DeltaY = 1; 
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
