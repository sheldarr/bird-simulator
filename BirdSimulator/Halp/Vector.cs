using System;
using Newtonsoft.Json;
using OpenTK;

namespace Engine.Halp
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Vector
    {
        public Point Start { get; private set; }
        public Point End { get; private set; }

        [JsonProperty]
        public double X { get { return End.X - Start.X; } }
        [JsonProperty]
        public double Y { get { return End.Y - Start.Y; } }
        [JsonProperty]
        public double Z { get { return End.Z - Start.Z; } }
        public double Length { get { return Math.Sqrt(X*X + Y*Y + Z*Z); } }

        public Vector(Point start, Point end)
        {
            Start = start;
            End = end;
        }

        public Vector(Vector3 vector) : this(Point.Origin, new Point(vector))
        {
        }

        public Vector3 OglVector
        {
            get { return new Vector3((float)X, (float)Y, (float)Z);}
        }

        public static Vector ZeroVector
        {
            get { return new Vector(Point.Origin, Point.Origin); }
        }
    }
}
