using Engine.ConfigurationLoader;
using OpenTK;

namespace Engine.Factories
{
    public static class GraphicsSettingsFactory
    {
        public static GraphicsSettings CreateGraphicsSettings(int fps, Vector2 windowResolution, float cameraSpeed, float maxVerticalAngle, float maxHorizontalAngle, Vector3 cameraPosition, Vector3 cameraDirection)
        {
            return new GraphicsSettings()
            {
                Fps = fps,
                WindowResolution = windowResolution,
                CameraSpeed = cameraSpeed,
                MaxVerticalAngle = maxVerticalAngle,
                MaxHorizontalAngle = maxHorizontalAngle,
                CameraPosition = cameraPosition,
                CameraDirection = cameraDirection
            };
        }
    }
}
