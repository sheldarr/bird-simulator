using Engine.Bird;
using Engine.Interfaces;
using OpenTK;

namespace Engine.Factories
{
    public static class BirdFactory
    {
        public static Bird.Bird CreateBird(Vector3 position, Vector3 direction, Statistics statistics, IStrategy strategy)
        {
            return new Bird.Bird(position, direction, statistics, strategy);
        }
    }
}
