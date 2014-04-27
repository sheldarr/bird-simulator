using System.Collections.Generic;
using BirdSimulator.Bird;
using BirdSimulator.Strategies;
using BirdSimulator.Time;
using BirdSimulator.Observer;
using BirdSimulator.World;
using GraphicsEngine.Scene;
using OpenTK;

namespace GraphicsEngine
{
    class Program
    {
        private static int x;
        private static int y;
        private static int z;
        private static float zoom = 1;

        public static List<Vector3> Objects = new List<Vector3>();

        static void Main(string[] args)
        {
            var observer = new Observer();
            var timeMachine = new TimeMachine(100);

            var slowStatistics = new Statistics(0.001f);
            var fastStatistics = new Statistics(0.005f);
            var linearFlightUp = new VectorFlight(new Vector3(1, 1, 1));
            var linearFlightDown = new VectorFlight(new Vector3(-1, -1, -1));

            var birds = new List<Bird>
            { 
                new Bird(new Vector3(0, 0, 0), slowStatistics, linearFlightUp), 
                new Bird(new Vector3(0, 0, 0), slowStatistics, linearFlightDown), 
                new Bird(new Vector3(0.1f, 0.1f, 0.1f), fastStatistics, linearFlightUp),
                new Bird(new Vector3(0.1f, 0.1f, 0.1f), fastStatistics, linearFlightDown)
            };

            birds.ForEach(observer.Subscribe);
            birds.ForEach(timeMachine.AddTraveler); 

            timeMachine.Enabled = true;

            var world = new World(birds);
            var worldConfig = new WorldConfiguration(60, 0.5f, 0.75f, 1.0f);
            using (var simulationScene = new WorldScene(world, worldConfig))
            {
                simulationScene.StartRendering();
            }          
        }
    }
}
