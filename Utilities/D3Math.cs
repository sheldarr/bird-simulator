using System;
using OpenTK;

namespace Utilities
{
    public static class D3Math
    {
        public static double DegreeToRadian(double angle)
        {
           return Math.PI * angle / 180.0;
        }

        public static double RadianToDegree(double angle)
        {
           return angle * (180.0 / Math.PI);
        }

        public static Quaternion GetRotationBetween(Vector3 u, Vector3 v)
        {
            var kCosTheta = Vector3.Dot(u, v);
            var k = (float)Math.Sqrt(Math.Pow(u.Length, 2) * Math.Pow(v.Length, 2));

            if (kCosTheta/k != -1)
            {
                return new Quaternion(Vector3.Cross(u, v), kCosTheta + k).Normalized();
            }

            // 180 degree rotation around any orthogonal vector
            var other = (Math.Abs(Vector3.Dot(u, Vector3.UnitX)) < 1.0) ? Vector3.UnitX : Vector3.UnitY;
            return new Quaternion(Vector3.Cross(u, other).Normalized(), 180);
        }

        public static Vector2 RotatePoint(Vector2 pointToRotate, Vector2 centerPoint, double angleInDegrees)
        {
            var angleInRadians = DegreeToRadian(angleInDegrees);
            var cosTheta = Math.Cos(angleInRadians);
            var sinTheta = Math.Sin(angleInRadians);
            return new Vector2
            {
                X = (int) (cosTheta * (pointToRotate.X - centerPoint.X) -
                    sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X),
                Y = (int) (sinTheta * (pointToRotate.X - centerPoint.X) +
                    cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y)
            };
        }

        public static double DistanceBetweenPoints(Vector3 pointA, Vector3 pointB)
        {
            double dX = pointB.X - pointA.X;
            double dY = pointB.Y - pointA.Y;
            double dZ = pointB.Z - pointA.Z;
            return Math.Sqrt(dX*dX + dY*dY + dZ*dZ);
        }
    }
}
