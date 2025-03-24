namespace Data
{
    public class Ball
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Radius { get; set; }
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
    }
}
