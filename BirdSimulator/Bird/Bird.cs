using System;
using System.Collections.Generic;
using Engine.Interfaces;
using NLog;
using OpenTK;

namespace Engine.Bird
{
    public class Bird : ITimeTraveler
    {
        public string Id { get; set; }

        public Vector3 Position;
        public Vector3 Direction;

        public event Bird.BirdUpdate OnUpdate;
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
            var log = LogManager.GetCurrentClassLogger();
            log.Trace("{0} moved to ({1};{2};{3}), direction = ({4};{5};{6})", Id, Position.X, Position.Y, Position.Z, Direction.X, Direction.Y, Direction.Z);
            log.Trace("{0} is following strategy: {1}", Id, _strategy.ToString());
        }
    }
}
