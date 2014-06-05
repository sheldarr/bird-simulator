using Engine.Interfaces;

namespace Engine.Effects
{
    public class Slowdown : IEffect
    {
        private readonly float _intensity;

        public Slowdown(float intensity)
        {
            _intensity = Clamp(intensity);
        }

        public void Apply(Bird.Bird bird)
        {
            bird.Statistics.SpeedModificator = 1 - _intensity;
        }

        private float Clamp(float value)
        {
            return (value < 0) ? 0 : (value > 1) ? 1 : value;
        }
    }
}
