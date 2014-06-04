using System;
using System.Collections.Generic;
using Engine.Interfaces;
using OpenTK;

namespace Engine.Bird
{
    public class Bird : ITimeTraveler
    {
        public Vector3 Position;
        public Vector3 Direction;

        public event BirdUpdate OnUpdate;
        public delegate void BirdUpdate(Bird bird, EventArgs e);

  
        private readonly Statistics _statistics;
     
        private readonly IStrategy _strategy;
        private IEnumerable<IExternalCondition> _externalConditions;

        public Bird(Vector3 position, Vector3 direction, Statistics statistics, IStrategy strategy)
        {
            Position = position;
            Direction = direction;
            _statistics = statistics;
            _strategy = strategy;
        }

        public void Tick()
        {
            _strategy.Move(ref Position, ref Direction, _statistics);
            OnUpdate(this, new EventArgs());
        }
    }
}
