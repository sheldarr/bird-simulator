using System.Xml.Linq;
using System.Xml.XPath;
using BirdSimulator.Bird;

namespace BirdSimulator.Factories
{
    public static class StatisticsFactory
    {
        public static Statistics CreateStatistics(XElement statistics)
        {
            var speed = (float) statistics.XPathSelectElement("speed");
            return new Statistics(speed);
        }
    }
}
