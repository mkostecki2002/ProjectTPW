namespace Data
{
    public class Ball
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Radius { get; set; }

        public Ball(int x, int y, int radius)
        {
            X = x;
            Y = y;
            Radius = radius;
        }
    }
}
