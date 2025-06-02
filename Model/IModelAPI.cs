using Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface IModelAPI
    {
        public ObservableCollection<Ball> Balls { get; set; }
        void AddBall();
        void StopAllThreads();
    }
}
