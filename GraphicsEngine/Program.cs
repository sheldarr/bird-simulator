using System;
using BirdSimulator.ConfigurationLoader;
using BirdSimulator.DebugLog;
using BirdSimulator.Observer;
using GraphicsEngine.Scene;

namespace GraphicsEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            DebugLog.AddDateTime = true;
            DebugLog.WriteLine("Program start");
            DebugLog.WriteLine("Loading configuration.xml");

            try
            {
                var configurationLoader = new ConfigurationLoader("configuration.xml");
                var world = configurationLoader.LoadWorld();
                var timeMachine = configurationLoader.LoadTimeMachine();
                var birds = configurationLoader.LoadBirds();

                DebugLog.WriteLine("Configuration loaded");

                var observer = new Observer();

                foreach (var bird in birds)
                {
                    observer.Subscribe(bird);
                    timeMachine.AddTraveler(bird);
                } 

                timeMachine.Enabled = true;

                using (var simulationScene = new WorldScene(world, birds))
                {
                    DebugLog.WriteLine("Start rendering");
                    simulationScene.StartRendering();
                }

                DebugLog.WriteLine("Program end");
            }
            catch (Exception e)
            {
                DebugLog.WriteLine("Program abort");
                DebugLog.WriteLine(e.ToString());
            }

            DebugLog.SaveLog();
        }
    }
}
