using Engine.Interfaces;
using OpenTK;

namespace Engine.Strategies
{
    public class VectorFlight : IStrategy
    {
        private readonly Vector3 _flightVector;

        public VectorFlight(Vector3 flightVector)
        {
            _flightVector = flightVector.Normalized();
        }

        public void Move(ref Vector3 position,ref Vector3 direction, Bird.Statistics statistics)
        {
            direction.X = _flightVector.X;
            direction.Y = _flightVector.Y;
            direction.Z = _flightVector.Z;
            position += _flightVector * statistics.Speed * statistics.SpeedModificator;
        }

        public new string ToString()
        {
            return string.Format("vectorflight with vector ({0};{1};{2})", _flightVector.X, _flightVector.Y,
                _flightVector.Z);
        }
    }
}

