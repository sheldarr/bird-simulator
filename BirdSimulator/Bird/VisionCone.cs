using Engine.Halp;

namespace Engine.Bird
{
    public class VisionCone
    {
        public double Aperture { get; private set; }
        public double ViewDistance { get; private set; }
        public Point Apex { get; set; }
        public Vector Direction { get; set; }

        public VisionCone(double aperture, double viewDistance)
        {
            Aperture = aperture;
            ViewDistance = viewDistance;
            Apex = Point.Origin;
            Direction = Vector.ZeroVector;
        }

        public bool Contains(Point point)
        {
            var pointVector = new Vector(Apex, point);
            return pointVector.Length < ViewDistance &&
                   Maths3D.Angle(Direction, pointVector) < Aperture/2;
        }
    }
}
