using System.Collections.Generic;
using Engine.Time;

namespace Engine.ConfigurationLoader
{
    public interface IConfigurationLoader
    {
        GraphicsSettings LoadGraphicsSettings();
        World.World LoadWorld();
        TimeMachine LoadTimeMachine();
        IList<Bird.Bird> LoadBirds();
    }
}
