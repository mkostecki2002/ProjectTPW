using System.ComponentModel;

namespace Data
{
    public class Ball : INotifyPropertyChanged
    {
        private int x;
        private int y;
        private int radius;

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

        public int Radius
        {
            get => radius;
            set
            {
                if (radius != value)
                {
                    radius = value;
                    OnPropertyChanged(nameof(Radius));
                }
            }
        }
        public int DeltaX { get; set; }
        public int DeltaY { get; set; }

        public Ball(int x, int y, int radius)
        {
            X = x;
            Y = y;
            Radius = radius;
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
