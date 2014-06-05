using Engine.ConfigurationLoader;
using OpenTK;

namespace Engine.Factories
{
    public static class GraphicsSettingsFactory
    {
        public static GraphicsSettings CreateGraphicsSettings(int fps, Vector2 windowResolution, float cameraSpeed, Vector3 cameraPosition, Vector3 cameraDirection)
        {
            return new GraphicsSettings()
            {
                Fps = fps,
                WindowResolution = windowResolution,
                CameraSpeed = cameraSpeed,
                CameraPosition = cameraPosition,
                CameraDirection = cameraDirection
            };
        }
    }
}
