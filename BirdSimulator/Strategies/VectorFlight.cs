using BirdSimulator.Interfaces;
using OpenTK;

namespace BirdSimulator.Strategies
{
    public class VectorFlight : IStrategy
    {
        private readonly Vector3 _flightVector;

        public VectorFlight(Vector3 flightVector)
        {
            _flightVector = flightVector.Normalized();
        }

        public void Move(ref Vector3 position, Bird.Statistics statistics)
        {
            position += _flightVector * statistics.Speed;
        }
    }
}

