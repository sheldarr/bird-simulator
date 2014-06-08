using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using Engine.Interfaces;
using Engine.Strategies;
using OpenTK;

namespace Engine.Factories
{
    public class StrategyFactory
    {
        private readonly Observer.Observer _observer;
 
        public StrategyFactory(Observer.Observer observer)
        {
            _observer = observer;
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
                    return new FollowThatGuy(_observer.Birds.First(b => b.Id == birdToFollow), minDistance);
                case Strategies.Strategies.FollowClosestYouSee:
                    return new FollowClosestYouSee(_observer, (double)strategyElement.XPathSelectElement("minDistance"));
            }

            return new NoStrategy();
        }
    }
}
