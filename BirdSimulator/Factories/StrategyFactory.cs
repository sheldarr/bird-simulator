using System;
using System.Xml.Linq;
using System.Xml.XPath;
using Engine.Interfaces;
using Engine.Strategies;
using OpenTK;

namespace Engine.Factories
{
    public static class StrategyFactory
    {
        public static IStrategy CreateVectorFlight(Vector3 vectorFlight)
        {
            return new VectorFlight(vectorFlight);
        }

        public static IStrategy GetStrategyFromXml(XElement strategyElement)
        {
            var typeAttribute = (string)strategyElement.Attribute("type");
            Strategies.Strategies strategyType;
            Enum.TryParse(typeAttribute, true, out strategyType);

            switch (strategyType)
            {
                case Strategies.Strategies.VectorFlight:
                    var x = (float)strategyElement.XPathSelectElement("flightVector/x");
                    var y = (float)strategyElement.XPathSelectElement("flightVector/y");
                    var z = (float)strategyElement.XPathSelectElement("flightVector/z");
                    var flightVector = new Vector3(x, y, z);
                    return new VectorFlight(flightVector);
            }

            return new NoStrategy();
        }
    }
}
