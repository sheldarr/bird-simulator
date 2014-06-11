using System;
using Engine.Bird;
using Engine.Halp;
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
            var estimatedDistance = Maths3D.DistanceBetweenPoints(new Point(estimatedPosition), new Point(_guide.Position));
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
                adjustedDistanceToGuide = Maths3D.DistanceBetweenPoints(new Point(adjustedPosition), new Point(_guide.Position));

            } while (adjustedDistanceToGuide <= _minDistance && i < 1000);

            return adjustedPosition;
        }

        public new string ToString()
        {
            return string.Format("following {0}", _guide.Id);
        }
    }
}
