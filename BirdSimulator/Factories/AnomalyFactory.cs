using System.Collections.Generic;
using Engine.Interfaces;
using OpenTK;

namespace Engine.Factories
{
    class AnomalyFactory
    {
        public static Anomaly.Anomaly CreateAnomaly(float size, Vector3 position, List<IEffect> effects)
        {
            return new Anomaly.Anomaly(size, position, effects);
        }
    }
}
