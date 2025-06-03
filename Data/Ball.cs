using System.ComponentModel;
using Logger;
using Common;
using System.Diagnostics;

namespace Data
{
    public class Ball : INotifyPropertyChanged, IDataAPI
    {

        private readonly object _lock = new();
        private Vector position;
        private Vector velocity;
        private double diameter;
        private Thread _worker;
        private readonly ILogger _logger;
        private volatile bool _running = true;
        private Timer _timer;

        public Ball(Vector position, Vector velocity, int diameter, ILogger logger)
        {
            this.position = position;
            this.velocity = velocity;
            Diameter = diameter;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Start()
        {
            _timer = new Timer(_ => Run(), null, 0, 16);
        }

        private void Run()
        {
            lock (_lock)
            {
                Position += Velocity;
            }
            _logger.Log(this);
        }

        public void Stop() => _running = false;

        public double CanvasLeft
        {
            get { lock (_lock) { return position.X - Diameter / 2; } }
        }
        public double CanvasTop
        {
            get { lock (_lock) { return position.Y - Diameter / 2; } }
        }

        public double Mass
        {
            get { lock (_lock) { return Math.Pow(Diameter / 2.0, 2); } }
        }

        public Vector Position
        {
            get { lock (_lock) { return position; } }
            set
            {
                lock (_lock)
                {
                    if (position.X != value.X || position.Y != value.Y)
                    {
                        position = value;
                        OnPropertyChanged(nameof(CanvasLeft));
                        OnPropertyChanged(nameof(CanvasTop));
                    }
                }
            }
        }

        public Vector Velocity
        {
            get { lock (_lock) { return velocity; } }
            private set
            {
                lock (_lock)
                {
                    if (velocity.X != value.X || velocity.Y != value.Y)
                    {
                        velocity = value;
                        OnPropertyChanged(nameof(Velocity));
                    }
                }
            }
        }

        public double Diameter
        {
            get { lock (_lock) { return diameter; } }
            set
            {
                lock (_lock)
                {
                    if (value <= 0)
                        throw new ArgumentException("Diameter must be a positive value.");
                    if (diameter != value)
                    {
                        diameter = value;
                        OnPropertyChanged(nameof(Diameter));
                        OnPropertyChanged(nameof(CanvasLeft));
                        OnPropertyChanged(nameof(CanvasTop));
                    }
                }
            }
        }


        public void RequestBounce(Vector newVelocity)
        {
            Velocity = newVelocity;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
