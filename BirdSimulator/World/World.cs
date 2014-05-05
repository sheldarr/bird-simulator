using OpenTK;

namespace Engine.World
{
    public class World
    {
        public int RenderFps { get; set; }
        public Vector2 WindowResolution { get; set; }
        public int WorldSize { get; set; }
        public float RotationAngle { get; set; }

        public World(int renderFps, Vector2 windowResolution, int worldSize, float rotationSpeed)
        {
            RenderFps = renderFps;
            WindowResolution = windowResolution;
            WorldSize = worldSize;
            RotationAngle = rotationSpeed;
        }
    }
}
