using System;

namespace Engine.Halp
{
    class Maths3D
    {
        public static double DistanceBetweenPoints(Point pointA, Point pointB)
        {
            double dX = pointB.X - pointA.X;
            double dY = pointB.Y - pointA.Y;
            double dZ = pointB.Z - pointA.Z;
            return Math.Sqrt(dX * dX + dY * dY + dZ * dZ);
        }

        public static double Angle(Vector a, Vector b)
        {
            return Math.Acos(DotProduct(a, b)/(a.Length*b.Length));
        }

        public static double DotProduct(Vector a, Vector b)
        {
            return a.X*b.X + a.Y*b.Y + a.Z*b.Z;
        }
    }
}
