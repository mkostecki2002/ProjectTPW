using System.ComponentModel;

namespace Data
{
    public class Ball : IDataAPI,INotifyPropertyChanged
    {
        private int x;
        private int y;
        private int diameter;
        public int CanvasLeft => X - Diameter / 2;
        public int CanvasTop => Y - Diameter / 2;

        public int X
        {
            get => x;
            set
            {
                if (x != value)
                {
                    x = value;
                    OnPropertyChanged(nameof(CanvasLeft));
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
                    OnPropertyChanged(nameof(CanvasTop));
                }
            }
        }

        public int Diameter
        {
            get => diameter;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Diameter must be a positive value.");
                }
                if (diameter != value)
                {
                    diameter = value;
                    OnPropertyChanged(nameof(Diameter));
                    OnPropertyChanged(nameof(CanvasLeft)); 
                    OnPropertyChanged(nameof(CanvasTop));
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
