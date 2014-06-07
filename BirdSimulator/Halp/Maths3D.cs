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
    }
}
