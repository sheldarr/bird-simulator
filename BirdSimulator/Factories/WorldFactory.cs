using OpenTK;

namespace Engine.Factories
{
    public static class WorldFactory
    {
        public static World.World CreateWorld(int renderFps, Vector2 windowResolution, int worldSize, float rotationSpeed)
        {
            return new World.World(renderFps, windowResolution, worldSize, rotationSpeed);
        }
    }
}
