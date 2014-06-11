using System.Xml.Linq;
using System.Xml.XPath;
using Engine.Bird;

namespace Engine.Factories
{
    public static class StatisticsFactory
    {
        public static Statistics CreateStatistics(XElement statsElement)
        {
            var speed = (float) statsElement.XPathSelectElement("speed");
            var aperture = (double) statsElement.XPathSelectElement("visionCone/aperture");
            var viewDistance = (double) statsElement.XPathSelectElement("visionCone/viewDistance");
            return new Statistics(speed, new VisionCone(aperture, viewDistance));
        }
    }
}