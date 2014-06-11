using Engine.Bird;
using OpenTK;

namespace Engine.Factories
{
    public static class BirdFactory
    {
        public static Bird.Bird CreateBird(string name, Vector3 position, Vector3 direction, Statistics stats)
        {
            return new Bird.Bird(position, direction, stats)
            {
                Id = name,
            };
        }
    }
}
