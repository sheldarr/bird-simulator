using OpenTK;

namespace Engine.World
{
    public class World
    {
        public int WorldSize { get; set; }
        public int NumberOfTrees { get; set; }

        public World(int worldSize, int numberOfTrees)
        {
            WorldSize = worldSize;
            NumberOfTrees = numberOfTrees;
        }
    }
}
