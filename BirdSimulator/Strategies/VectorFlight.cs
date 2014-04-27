using BirdSimulator.Interfaces;
using OpenTK;

namespace BirdSimulator.Strategies
{
    public class VectorFlight : IStrategy
    {
        private readonly Vector3 _flightDirection;

        public VectorFlight(Vector3 flightDirection)
        {
            _flightDirection = flightDirection.Normalized();
        }

        public void Move(ref Vector3 position, Bird.Statistics statistics)
        {
            position += _flightDirection * statistics.Speed;
        }
    }
}

