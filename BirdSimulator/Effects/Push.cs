using Engine.Interfaces;
using OpenTK;

namespace Engine.Effects
{
    public class Push : IEffect
    {
        private readonly Vector3 _direction;

        public Push(Vector3 direction)
        {
            _direction = direction;
        }

        public void Apply(Bird.Bird bird)
        {
            bird.Position += _direction;
        }
    }
}
