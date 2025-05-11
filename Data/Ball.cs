using System.ComponentModel;

namespace Data
{
    public class Ball : INotifyPropertyChanged, IDataAPI
    {
        private Vector position;
        private Vector velocity;
        private double diameter;
        public double CanvasLeft => position.X - Diameter / 2;
        public double CanvasTop => position.Y - Diameter / 2;

        public double Mass => Math.Pow(Diameter / 2.0, 2);

        public Vector Position
        {
            get => position;
            set
            {
                if (position.X != value.X || position.Y != value.Y)
                {
                    position = value;
                    OnPropertyChanged(nameof(CanvasLeft));
                    OnPropertyChanged(nameof(CanvasTop));
                }
            }
        }

        public Vector Velocity
        {
            get => velocity;
            set
            {
                if (velocity.X != value.X || velocity.Y != value.Y)
                {
                    velocity = value;
                    OnPropertyChanged(nameof(Velocity));
                }
            }
        }

        public double Diameter
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

        public Ball(Vector position, Vector velocity, int diameter)
        {
            this.position = position;
            this.velocity = velocity;
            Diameter = diameter;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
