using Engine.Bird;
using Engine.Interfaces;
using OpenTK;
using Utilities;

namespace Engine.Strategies
{
    class FollowThatGuy : IStrategy
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
            var estimatedPosition = GetPosition(position, direction, statistics.Speed);
            var estimatedDistance = D3Math.DistanceBetweenPoints(estimatedPosition, _guide.Position);
            position = (estimatedDistance >= _minDistance) ? estimatedPosition : FindClosestPositionToGuide(position, direction, statistics.Speed);
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
                adjustedDistanceToGuide = D3Math.DistanceBetweenPoints(adjustedPosition, _guide.Position);

            } while (adjustedDistanceToGuide <= _minDistance && i < 1000);

            return adjustedPosition;
        }
    }
}
