using Engine.Effects;
using OpenTK;

namespace Engine.Factories
{
    public class EffectsFactory
    {
        public Acceleration CreateAcceleration(float intensity)
        {
            return new Acceleration(intensity);
        }

        public Push CreatePush(Vector3 direction)
        {
            return new Push(direction);
        }

        public Slowdown CreateSlowdown(float intensity)
        {
            return new Slowdown(intensity);
        }
    }
}
