using System;
using OpenTK;

namespace Utils
{
    public static class D3Math
    {
        public static Vector3 VectorToRotation(Vector3 vector)
        {
            var rotations = new Vector3();
            vector.NormalizeFast();

            rotations.X = (float)RadianToDegree(Math.Acos(vector.X / vector.LengthFast));
            rotations.Y = (float)RadianToDegree(Math.Acos(vector.Y / vector.LengthFast));
            rotations.Z = (float)RadianToDegree(Math.Acos(vector.Z / vector.LengthFast));
            
            return rotations;
        }

        public static double DegreeToRadian(double angle)
        {
           return Math.PI * angle / 180.0;
        }

        public static double RadianToDegree(double angle)
        {
           return angle * (180.0 / Math.PI);
        }
    }
}
