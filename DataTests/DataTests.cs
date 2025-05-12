using Data;

namespace DataTests
{
    public class BallTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            var position = new Vector(10, 20);
            var velocity = new Vector(1, 1);
            Ball ball = new Ball(position, velocity, 30);

            Assert.Equal(10, ball.Position.X);
            Assert.Equal(20, ball.Position.Y);
            Assert.Equal(1, ball.Velocity.X);
            Assert.Equal(1, ball.Velocity.Y);
            Assert.Equal(30, ball.Diameter);
        }

        [Fact]
        public void Position_Setter()
        {
            var position = new Vector(10, 20);
            var velocity = new Vector(1, 1);
            Ball ball = new Ball(position, velocity, 30);

            ball.Position = new Vector(50, 60);

            Assert.Equal(50, ball.Position.X);
            Assert.Equal(60, ball.Position.Y);
        }

        [Fact]
        public void Diameter_Setter()
        {
            var position = new Vector(10, 20);
            var velocity = new Vector(1, 1);
            Ball ball = new Ball(position, velocity, 30);
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
        public void CanvasLeft()
        {
            var position = new Vector(50, 50);
            var velocity = new Vector(0, 0);
            Ball ball = new Ball(position, velocity, 20);

            double canvasLeft = ball.CanvasLeft;

            Assert.Equal(40, canvasLeft);
        }

        [Fact]
        public void CanvasTop()
        {
            var position = new Vector(50, 50);
            var velocity = new Vector(0, 0);
            Ball ball = new Ball(position, velocity, 20);

            double canvasTop = ball.CanvasTop;

            Assert.Equal(40, canvasTop);
        }

        [Fact]
        public void PropertyChanged()
        {
            var position = new Vector(50, 50);
            var velocity = new Vector(0, 0);
            Ball ball = new Ball(position, velocity, 20);
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

            ball.Position = new Vector(60, 70);

            Assert.True(canvasLeftChanged);
            Assert.True(canvasTopChanged);
        }
    }
}
