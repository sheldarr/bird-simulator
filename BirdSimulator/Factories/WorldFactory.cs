using OpenTK;

namespace Engine.Factories
{
    public static class WorldFactory
    {
        public static World.World CreateWorld(int worldSize, int numberOfTrees)
        {
            return new World.World(worldSize, numberOfTrees);
        }
    }
}
