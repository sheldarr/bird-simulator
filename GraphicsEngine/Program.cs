using Engine.ConfigurationLoader;
using Engine.DebugLog;
using Engine.Observer;

namespace GraphicsEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            DebugLog.AddDateTime = true;
            DebugLog.WriteLine("Program start");
            DebugLog.WriteLine("Loading configuration.xml");

            IConfigurationLoader configurationLoader = new ConfigurationLoader(@"Resources\configuration.xml");
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

            using (var simulationScene = new Scene.Scene(world, birds))
            {
                DebugLog.WriteLine("Start rendering");
                simulationScene.StartRendering();
            }

            DebugLog.WriteLine("Program end");
            DebugLog.SaveLog();
        }
    }
}
