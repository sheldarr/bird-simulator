using System.Linq;
using Engine.ConfigurationLoader;
using Engine.Factories;
using Engine.Observer;

namespace GraphicsEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfigurationLoader configurationLoader = new ConfigurationLoader(@"Resources\configuration.xml");
            var graphicsSettings = configurationLoader.LoadGraphicsSettings();
            var timeMachine = configurationLoader.LoadTimeMachine();
            var world = configurationLoader.LoadWorld();
            var anomalies = configurationLoader.LoadAnomalies();

            var observer = new Observer(world);
            observer.Anomalies.AddRange(anomalies);
            observer.AddBirds(configurationLoader.LoadBirds());
            configurationLoader.LoadStrategiesForBirds(observer.Birds, new StrategyFactory(observer));

            observer.Birds.ToList().ForEach(timeMachine.AddTraveler);

            timeMachine.Enabled = true;

            using (var simulationScene = new Scene.Scene(graphicsSettings, world, observer.Birds, anomalies))
            {
                simulationScene.StartRendering();
            }
        }
    }
}
