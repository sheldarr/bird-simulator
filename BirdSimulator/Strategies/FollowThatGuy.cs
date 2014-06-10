using System;
using Engine.Bird;
using Engine.Interfaces;
using OpenTK;

namespace Engine.Strategies
{
    public class FollowThatGuy : IStrategy
    {
        private readonly Bird.Bird _guide;
        private readonly double _minDistance;

        public FollowThatGuy(Bird.Bird guide, double minDistance)
        {
            _guide = guide;
            _minDistance = minDistance;
        }

        public void Move(ref Vector3 position, ref Vector3 direction, Statistics statistics)
        {
            direction = (_guide.Position - position).Normalized();
            var estimatedPosition = GetPosition(position, direction, statistics.Speed*statistics.SpeedModificator);
            var estimatedDistance = DistanceBetweenPoints(estimatedPosition, _guide.Position);
            position = (estimatedDistance >= _minDistance) ? estimatedPosition : FindClosestPositionToGuide(position, direction, statistics.Speed*statistics.SpeedModificator);
        }

        private Vector3 GetPosition(Vector3 position, Vector3 direction, float speed)
        {
            return position + direction*speed;
        }

        private Vector3 FindClosestPositionToGuide(Vector3 position, Vector3 direction, float speed)
        {
            Vector3 adjustedPosition;
            double adjustedDistanceToGuide;
            int i = 0;
            do
            {
                var adjustedSpeed = speed - 0.01*++i;
                adjustedPosition = GetPosition(position, direction, (float)adjustedSpeed);
                adjustedDistanceToGuide = DistanceBetweenPoints(adjustedPosition, _guide.Position);

            } while (adjustedDistanceToGuide <= _minDistance && i < 1000);

            return adjustedPosition;
        }

        private double DistanceBetweenPoints(Vector3 pointA, Vector3 pointB)
        {
            double dX = pointB.X - pointA.X;
            double dY = pointB.Y - pointA.Y;
            double dZ = pointB.Z - pointA.Z;
            return Math.Sqrt(dX * dX + dY * dY + dZ * dZ);
        }

        public new string ToString()
        {
            return string.Format("following {0}", _guide.Id);
        }
    }
}
