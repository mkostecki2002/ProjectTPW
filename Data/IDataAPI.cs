using System.ComponentModel;

namespace Data
{
    public interface IDataAPI
    {
        Vector Position { get; set; }
        Vector Velocity { get; set; }
        public double Diameter { get; set; }
        double CanvasLeft { get; }
        double CanvasTop { get; }

        event PropertyChangedEventHandler? PropertyChanged;
    }
}
