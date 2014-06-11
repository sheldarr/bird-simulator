using System.Collections.Generic;
using Engine.Factories;
using Engine.Time;

namespace Engine.ConfigurationLoader
{
    public interface IConfigurationLoader
    {
        GraphicsSettings LoadGraphicsSettings();
        World.World LoadWorld();
        TimeMachine LoadTimeMachine();
        List<Bird.Bird> LoadBirds();
        List<Anomaly.Anomaly> LoadAnomalies();
        void LoadStrategiesForBirds(IEnumerable<Bird.Bird> birds, StrategyFactory stratFactory);
    }
}
