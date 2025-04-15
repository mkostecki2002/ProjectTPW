using System.ComponentModel;

namespace Data
{
    public interface IDataAPI
    {
        int X { get; set; }
        int Y { get; set; }
        int Diameter { get; set; }
        int DeltaX { get; set; }
        int DeltaY { get; set; }
        int CanvasLeft { get; }
        int CanvasTop { get; }

        event PropertyChangedEventHandler? PropertyChanged;
    }
}
