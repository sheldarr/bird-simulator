using OpenTK;

namespace BirdSimulator.World
{
    public class World
    {
        public int RenderFps { get; set; }
        public Vector2 WindowResolution { get; set; }
        public Vector3 WorldScale { get; set; }
        public Vector3 WorldSize { get; set; }
        public float RotationAngle { get; set; }

        public World(int renderFps, Vector2 windowResolution, float worldScale, float worldSize, float rotationAngle)
        {
            RenderFps = renderFps;
            WindowResolution = windowResolution;
            WorldScale = new Vector3(worldScale);
            WorldSize = new Vector3(worldSize);
            RotationAngle = rotationAngle;
        }
    }
}
