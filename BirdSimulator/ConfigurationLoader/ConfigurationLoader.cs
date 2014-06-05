using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.XPath;
using Engine.Bird;
using Engine.Factories;
using Engine.Interfaces;
using Engine.Time;
using OpenTK;

namespace Engine.ConfigurationLoader 
{
    public class ConfigurationLoader : IConfigurationLoader
    {
        private readonly XElement _configurationDocument;
        private readonly IList<Bird.Bird> _loadedBirds;
        private readonly StrategyFactoryWithBirdsMemory _strategyFactory;

        public ConfigurationLoader(string configurationPath)
        {
            _loadedBirds = new List<Bird.Bird>();
            _strategyFactory = new StrategyFactoryWithBirdsMemory(_loadedBirds);
            _configurationDocument = XElement.Load(configurationPath);
        }

        public GraphicsSettings LoadGraphicsSettings()
        {
            var graphicsSettings = _configurationDocument.XPathSelectElement("graphicsSettings");
            return ParseGraphicsSettings(graphicsSettings);
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
            var birds = _configurationDocument.XPathSelectElements("birds/bird");
            foreach (var bird in birds)
            {
                try
                {  
                    _loadedBirds.Add(ParseBird(bird));
                }
                catch (Exception e)
                {
                }
            }

            return _loadedBirds;
        }

        private GraphicsSettings ParseGraphicsSettings(XElement graphicsSettings)
        {
            var fps = (int)graphicsSettings.XPathSelectElement("fps");
            var width = (int)graphicsSettings.XPathSelectElement("windowResolution/width");
            var height = (int)graphicsSettings.XPathSelectElement("windowResolution/height");
            var windowResolution = new Vector2(width, height);
            var cameraSpeed = (float)graphicsSettings.XPathSelectElement("camera/speed");
            var maxVerticalAngle = (float)graphicsSettings.XPathSelectElement("camera/maxVerticalAngle");
            var maxHorizontalAngle = (float)graphicsSettings.XPathSelectElement("camera/maxHorizontalAngle");

            var x = (float)graphicsSettings.XPathSelectElement("camera/position/x");
            var y = (float)graphicsSettings.XPathSelectElement("camera/position/y");
            var z = (float)graphicsSettings.XPathSelectElement("camera/position/z");
            var cameraPosition = new Vector3(x, y, z);

            x = (float)graphicsSettings.XPathSelectElement("camera/direction/x");
            y = (float)graphicsSettings.XPathSelectElement("camera/direction/y");
            z = (float)graphicsSettings.XPathSelectElement("camera/direction/z");
            var cameraDirection = new Vector3(x, y, z);

            return GraphicsSettingsFactory.CreateGraphicsSettings(fps, windowResolution, cameraSpeed, maxVerticalAngle, maxHorizontalAngle, cameraPosition, cameraDirection);
        }

        private World.World ParseWorld(XElement world)
        {
            var worldSize = (int)world.XPathSelectElement("worldSize");
            var numberOfTrees = (int) world.XPathSelectElement("numberOfTrees");

            return WorldFactory.CreateWorld(worldSize, numberOfTrees);
        }

        private Time.TimeMachine ParseTimeMachine(XElement timeMachine)
        {
            var quantum = (int)timeMachine.XPathSelectElement("quantum");

            return TimeMachineFactory.CreateTimeMachine(quantum);
        }

        private Bird.Bird ParseBird(XElement bird)
        {
            var name = (string) bird.XPathSelectElement("name");

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

            return BirdFactory.CreateBird(name, position, direction, statistics, strategy);
        }

        private Statistics ParseStatistics(XElement statistics)
        {
            var speed = (float)statistics.XPathSelectElement("speed");
            return StatisticsFactory.CreateStatistics(speed);
        }

        private IStrategy ParseStrategy(XElement strategy)
        {
            return _strategyFactory.GetStrategy(strategy);
        }
    }
}
