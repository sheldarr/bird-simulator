using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using Engine.Interfaces;
using Engine.Strategies;
using OpenTK;

namespace Engine.Factories
{
    class StrategyFactoryWithBirdsMemory
    {
        private readonly IList<Bird.Bird> _birds;
 
        public StrategyFactoryWithBirdsMemory(IList<Bird.Bird> birds)
        {
            _birds = birds;
        }

        public IStrategy GetStrategy(XElement strategyElement)
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
                case Strategies.Strategies.FollowThatGuy:
                    var birdToFollow = (string) strategyElement.XPathSelectElement("birdToFollow");
                    var minDistance = (double) strategyElement.XPathSelectElement("minDistance");
                    return new FollowThatGuy(_birds.First(b => b.Id == birdToFollow), minDistance);
            }

            return new NoStrategy();
        }
    }
}
