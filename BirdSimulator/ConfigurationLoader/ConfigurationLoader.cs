using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.XPath;
using Engine.Bird;
using Engine.Factories;
using Engine.Interfaces;
using Engine.Strategies;
using Engine.Time;
using OpenTK;

namespace Engine.ConfigurationLoader 
{
    public class ConfigurationLoader : IConfigurationLoader
    {
        private readonly XElement _configurationDocument;

        public ConfigurationLoader(string configurationPath)
        {
            _configurationDocument = XElement.Load(configurationPath);
        }

        public World.World LoadWorld()
        {
            var world = _configurationDocument.XPathSelectElement("world");
            return ParseWorld(world);
        }

        public TimeMachine LoadTimeMachine()
        {
            var timeMachine = _configurationDocument.XPathSelectElement("timeMachine");
            return ParseTimeMachine(timeMachine);
        }

        public IList<Bird.Bird> LoadBirds()
        {
            IList<Bird.Bird> birdsList = new List<Bird.Bird>();
            
            var birds = _configurationDocument.XPathSelectElements("birds/bird");
            foreach (var bird in birds)
            {
                try
                {  
                    birdsList.Add(ParseBird(bird));
                }
                catch (Exception e)
                {
                    DebugLog.DebugLog.WriteLine("Parsing error!");
                    DebugLog.DebugLog.WriteLine(e.ToString());
                }
            }

            return birdsList;
        }

        private World.World ParseWorld(XElement world)
        {
            DebugLog.DebugLog.WriteLine("Parsing world...");

            var renderFps = (int)world.XPathSelectElement("renderFps");
            var width = (int)world.XPathSelectElement("windowResolution/width");
            var height = (int)world.XPathSelectElement("windowResolution/height");
            var windowResolution = new Vector2(width, height);
            var worldSize = (int)world.XPathSelectElement("worldSize");
            var rotationSpeed = (float)world.XPathSelectElement("rotationSpeed");

            DebugLog.DebugLog.WriteLine("Parsing successfull!");
            return WorldFactory.CreateWorld(renderFps, windowResolution, worldSize, rotationSpeed);
        }

        private Time.TimeMachine ParseTimeMachine(XElement timeMachine)
        {
            DebugLog.DebugLog.WriteLine("Parsing time machine...");

            var quantum = (int)timeMachine.XPathSelectElement("quantum");

            DebugLog.DebugLog.WriteLine("Parsing successfull!");
            return TimeMachineFactory.CreateTimeMachine(quantum);
        }

        private Bird.Bird ParseBird(XElement bird)
        {
            DebugLog.DebugLog.WriteLine("Parsing bird...");
            var x = (float)bird.XPathSelectElement("position/x");
            var y = (float)bird.XPathSelectElement("position/y");
            var z = (float)bird.XPathSelectElement("position/z");

            var position = new Vector3(x, y, z);

            x = (float)bird.XPathSelectElement("direction/x");
            y = (float)bird.XPathSelectElement("direction/y");
            z = (float)bird.XPathSelectElement("direction/z");

            var direction = new Vector3(x, y, z);
            
            var statistics = ParseStatistics(bird.XPathSelectElement("statistics"));
            var strategy = ParseStrategy(bird.XPathSelectElement("strategy"));

            DebugLog.DebugLog.WriteLine("Parsing successfull!");
            return BirdFactory.CreateBird(position, direction, statistics, strategy);
        }

        private Statistics ParseStatistics(XElement statistics)
        {
            var speed = (float)statistics.XPathSelectElement("speed");
            return StatisticsFactory.CreateStatistics(speed);
        }

        private IStrategy ParseStrategy(XElement strategy)
        {
            var typeAttribute = (string)strategy.Attribute("type");
            Strategies.Strategies strategyType;
            Enum.TryParse(typeAttribute, true, out strategyType);

            switch (strategyType)
            {
                case Strategies.Strategies.VectorFlight:
                    var x = (float)strategy.XPathSelectElement("flightVector/x");
                    var y = (float)strategy.XPathSelectElement("flightVector/y");
                    var z = (float)strategy.XPathSelectElement("flightVector/z");
                    var flightVector = new Vector3(x, y, z);
                    return StrategyFactory.CreateVectorFlight(flightVector);
            }

            return new NoStrategy();
        }
    }
}
