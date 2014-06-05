using System.Collections.Generic;
using Engine.Interfaces;
using OpenTK;

namespace Engine.Anomaly
{
    public class Anomaly
    {
        public float Size { get; set; }
        public Vector3 Position { get; set; }
        public List<IEffect> Effects { get; set; }

        public Anomaly(float size, Vector3 position, List<IEffect> effects)
        {
            Size = size;
            Position = position;
            Effects = effects;
        }

        public bool IsWithin(Bird.Bird bird)
        {
            return bird.Position.X >= Position.X - Size && bird.Position.X <= Position.X + Size
                   && bird.Position.Y >= Position.Y - Size && bird.Position.Y <= Position.Y + Size
                   && bird.Position.Z >= Position.Z - Size && bird.Position.Z <= Position.Z + Size;
        }

        public void ApplyEffects(Bird.Bird bird)
        {
            Effects.ForEach(effect => effect.Apply(bird));
        }
    }
}
