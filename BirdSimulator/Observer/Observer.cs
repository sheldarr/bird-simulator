using System.Collections.Generic;
using System.Linq;
using Engine.Bird;
using Engine.Halp;
using NLog;

namespace Engine.Observer
{
    public class Observer
    {
        private World.World _world;
        public List<Anomaly.Anomaly> Anomalies { get; private set; }

        public IEnumerable<Bird.Bird> Birds { get { return _birds; } }
        private readonly List<Bird.Bird> _birds;

        public Observer(World.World world)
        {
            _world = world;
            Anomalies = new List<Anomaly.Anomaly>();
            _birds = new List<Bird.Bird>();
        }

        public void AddBirds(IEnumerable<Bird.Bird> birds)
        {
            birds.ToList().ForEach(Subscribe);
        }

        public void Subscribe(Bird.Bird bird)
        {
            _birds.Add(bird);
            bird.OnTick += (birdie, args) => ApplyEffects(birdie);
        }

        private void ApplyEffects(Bird.Bird bird)
        {
            foreach (var anomaly in Anomalies.Where(anomaly => anomaly.IsWithin(bird)))
            {
                anomaly.ApplyEffects(bird);
            }    
        }

        public IEnumerable<Bird.Bird> Inside(VisionCone visionVisionCone)
        {
            return Birds.Where(b => visionVisionCone.Contains(new Point(b.Position)));
        }
    }
}