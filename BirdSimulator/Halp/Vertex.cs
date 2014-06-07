using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Halp
{
    class Vertex
    {
        public Point Start { get; private set; }
        public Point End { get; private set; }
        public double X { get { throw new NotImplementedException();} }
        public double Y { get { throw new NotImplementedException(); } }
        public double Z { get { throw new NotImplementedException(); } }
        public double Length { get { throw new NotImplementedException(); } }

        public Vertex(Point start, Point end)
        {
            Start = start;
            End = end;
        }
    }
}
