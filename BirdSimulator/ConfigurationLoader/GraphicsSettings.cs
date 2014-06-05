using OpenTK;

namespace Engine.ConfigurationLoader
{
    public class GraphicsSettings
    {
        public int Fps { get; set; }
        public Vector2 WindowResolution { get; set; }
        public float CameraSpeed { get; set; }
        public Vector3 CameraPosition { get; set; }
        public Vector3 CameraDirection { get; set; }
    }
}
