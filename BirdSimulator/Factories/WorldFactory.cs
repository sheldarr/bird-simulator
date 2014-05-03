using System.Xml.Linq;
using System.Xml.XPath;
using OpenTK;

namespace BirdSimulator.Factories
{
    public static class WorldFactory
    {
        public static World.World CreateWorld(XElement world)
        {
            var renderFps = (int)world.XPathSelectElement("renderFps");
            var width = (int)world.XPathSelectElement("windowResolution/width");
            var height = (int)world.XPathSelectElement("windowResolution/height");
            var windowResolution = new Vector2(width, height);
            var worldSize = (int)world.XPathSelectElement("worldSize");
            var worldScale = (float)world.XPathSelectElement("worldScale");
            var rotationAngle = (float)world.XPathSelectElement("rotationAngle");

            return new World.World(renderFps, windowResolution, worldScale, worldSize, rotationAngle);
        }

        public static World.World CreateWorld(int renderFps, Vector2 windowResolution, int worldSize, float worldScale, float rotationAngle)
        {
            return new World.World(renderFps, windowResolution, worldScale, worldSize, rotationAngle);
        }
    }
}
