using System;
using BirdSimulator.Interfaces;
using System.Collections.Generic;
using OpenTK;

namespace BirdSimulator.Bird
{
    public class Bird : ITimeTraveler
    {
        public Vector3 Position;

        public event BirdUpdate OnUpdate;
        public delegate void BirdUpdate(Bird bird, EventArgs e);

  
        private Statistics _statistics;
     
        private IStrategy _strategy;
        private IEnumerable<IExternalCondition> _externalConditions;

        public Bird(Vector3 position, Statistics statistics, IStrategy strategy)
        {
            Position = position;
            _statistics = statistics;
            _strategy = strategy;
        }

        public void Tick()
        {
            _strategy.Move(ref Position, _statistics);
            OnUpdate(this, new EventArgs());
        }
    }
}
