using System.ComponentModel;

namespace Common
{
    public interface IDataAPI
    {
        Vector Position { get; set; }
        Vector Velocity { get; }
        public double Diameter { get; set; }
        double CanvasLeft { get; }
        double CanvasTop { get; }

        event PropertyChangedEventHandler? PropertyChanged;
    }
}
