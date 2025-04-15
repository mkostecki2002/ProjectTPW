using Data;

namespace DataTests
{
    public class BallTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            Ball ball = new Ball(10, 20, 30);

            Assert.Equal(10, ball.X);
            Assert.Equal(20, ball.Y);
            Assert.Equal(30, ball.Diameter);
            Assert.Equal(1, ball.DeltaX);
            Assert.Equal(1, ball.DeltaY);
        }

        [Fact]
        public void X_Setter()
        {
            Ball ball = new Ball(10, 20, 30);

            ball.X = 50;

            Assert.Equal(50, ball.X);
        }

        [Fact]
        public void Y_Setter()
        {
            Ball ball = new Ball(10, 20, 30);

            ball.Y = 60;

            Assert.Equal(60, ball.Y);
        }

        [Fact]
        public void Diameter_Setter_ShouldUpdateValueAndTriggerPropertyChanged()
        {
            Ball ball = new Ball(10, 20, 30);
            bool propertyChangedTriggered = false;

            ball.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(Ball.Diameter))
                {
                    propertyChangedTriggered = true;
                }
            };

            ball.Diameter = 40;

            Assert.Equal(40, ball.Diameter);
            Assert.True(propertyChangedTriggered);
        }

        [Fact]
        public void CanvasLeft_ShouldReturnCorrectValue()
        {
            Ball ball = new Ball(50, 50, 20);

            int canvasLeft = ball.CanvasLeft;

            Assert.Equal(40, canvasLeft); // X - Diameter / 2
        }

        [Fact]
        public void CanvasTop_ShouldReturnCorrectValue()
        {
            Ball ball = new Ball(50, 50, 20);

            int canvasTop = ball.CanvasTop;

            Assert.Equal(40, canvasTop); // Y - Diameter / 2
        }

        [Fact]
        public void PropertyChanged_ShouldTriggerForCanvasLeftAndCanvasTop()
        {
            Ball ball = new Ball(50, 50, 20);
            bool canvasLeftChanged = false;
            bool canvasTopChanged = false;

            ball.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(Ball.CanvasLeft))
                {
                    canvasLeftChanged = true;
                }
                if (args.PropertyName == nameof(Ball.CanvasTop))
                {
                    canvasTopChanged = true;
                }
            };

            ball.X = 60;
            ball.Y = 70;

            Assert.True(canvasLeftChanged);
            Assert.True(canvasTopChanged);
        }
    }
}
