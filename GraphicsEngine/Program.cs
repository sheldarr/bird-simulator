using Engine.ConfigurationLoader;
using Engine.Observer;

namespace GraphicsEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfigurationLoader configurationLoader = new ConfigurationLoader(@"Resources\configuration.xml");
            var graphicsSettings = configurationLoader.LoadGraphicsSettings();
            var world = configurationLoader.LoadWorld();
            var timeMachine = configurationLoader.LoadTimeMachine();
            var birds = configurationLoader.LoadBirds();

            var observer = new Observer();

            foreach (var bird in birds)
            {
                observer.Subscribe(bird);
                timeMachine.AddTraveler(bird);
            } 

            timeMachine.Enabled = true;

            using (var simulationScene = new Scene.Scene(graphicsSettings, world, birds))
            {
                simulationScene.StartRendering();
            }
        }
    }
}
