using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Interfaces;
using Utilities.Shapes;

namespace Utilities.Anomaly
{
    public class AnomalyModel : IRenderable
    {
        private readonly Cube _cube;

        public AnomalyModel(Engine.Anomaly.Anomaly anomaly)
        {
            _cube = new Cube(anomaly.Size, anomaly.Position, Color.CornflowerBlue);
        }

        public void Render()
        {
            _cube.Render();
        }
    }
}
