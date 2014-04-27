using OpenTK;

namespace BirdSimulator.World
{
    public class WorldConfiguration
    {
        public int RenderFps { get; set; }
        public Vector3 WorldScale { get; set; }
        public Vector3 WorldSize { get; set; }
        public float RotationAngle { get; set; }
        


        public WorldConfiguration(int renderFps, float worldScale, float worldSize, float rotationAngle)
        {
            RenderFps = renderFps;
            WorldScale = new Vector3(worldScale);
            WorldSize = new Vector3(worldSize);
            RotationAngle = rotationAngle;
        }
    }
}
