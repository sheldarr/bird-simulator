using System;
using System.Xml.Linq;
using System.Xml.XPath;
using BirdSimulator.Interfaces;
using BirdSimulator.Strategies;
using OpenTK;

namespace BirdSimulator.Factories
{
    public static class StrategyFactory
    {
        public static IStrategy CreateStrategy(XElement strategy)
        {
            var typeAttribute = (string)strategy.Attribute("type");
            Strategies.Strategies strategyType;
            Enum.TryParse(typeAttribute, true, out strategyType);

            switch (strategyType)
            {
                case Strategies.Strategies.VectorFlight:
                    return CreateVectorFlight(strategy);
            }

            return new NoStrategy();
        }

        public static IStrategy CreateVectorFlight(XElement strategy)
        {
            var x = (float)strategy.XPathSelectElement("flightVector/x");
            var y = (float)strategy.XPathSelectElement("flightVector/y");
            var z = (float)strategy.XPathSelectElement("flightVector/z");
            var flightVector = new Vector3(x, y, z);

            return new VectorFlight(flightVector);
        }
    }
}
