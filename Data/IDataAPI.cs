using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
