using Newtonsoft.Json;
using OpenTK;

namespace Engine.Halp
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Point
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }

        public Point(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point(Vector3 vector) : this(vector.X, vector.Y, vector.Z)
        {
        }

        [JsonIgnore]
        public static Point Origin
        {
            get
            {
                return new Point(0.0, 0.0, 0.0);
            }
        }

        [JsonIgnore]
        public Vector3 OglVector
        {
            get
            {
                return new Vector3((float)X, (float)Y, (float)Z);
            }
        }
    }
}
