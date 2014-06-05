using System.Collections.Generic;
using System.Linq;

namespace Engine.Observer
{
    public class Observer
    {
        private World.World _world;
        private List<Anomaly.Anomaly> _anomalies;

        public Observer(World.World world, List<Anomaly.Anomaly> anomalies)
        {
            _world = world;
            _anomalies = anomalies;
        }

        public void Subscribe(Bird.Bird bird)
        {
            bird.OnTick += (birdie, args) => ApplyEffects(birdie);
        }

        private void ApplyEffects(Bird.Bird bird)
        {
            foreach (var anomaly in _anomalies.Where(anomaly => anomaly.IsWithin(bird)))
            {
                anomaly.ApplyEffects(bird);
            }    
        }
    }
}
