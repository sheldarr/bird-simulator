﻿using System;
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
                    DebugLog.DebugLog.WriteLine("Parsing error!");
                    DebugLog.DebugLog.WriteLine(e.ToString());
                }
            }

            return _loadedBirds;
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

            DebugLog.DebugLog.WriteLine("Parsing successfull!");
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
