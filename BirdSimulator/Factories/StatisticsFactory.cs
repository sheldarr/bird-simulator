using System.Xml.Linq;
using System.Xml.XPath;
using Engine.Bird;
using OpenTK;

namespace Engine.Factories
{
    public static class StatisticsFactory
    {
        public static Statistics CreateStatistics(Vector3 birdPosition, Vector3 birdDirection, XElement statsElement)
        {
            var speed = (float) statsElement.XPathSelectElement("speed");
            var aperture = (double) statsElement.XPathSelectElement("visionCone/aperture");
            var viewDistance = (double) statsElement.XPathSelectElement("visionCone/viewDistance");
            return new Statistics(speed, new ConeOfVision(birdPosition, birdDirection, aperture, viewDistance));
        }
    }
}