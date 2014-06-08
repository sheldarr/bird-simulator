using System;
using Engine.Halp;
using Engine.Interfaces;
using Engine.Strategies;
using Newtonsoft.Json;
using NLog;
using OpenTK;

namespace Engine.Bird
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Bird : ITimeTraveler
    {
        [JsonProperty]
        public string Id { get; set; }
        public IStrategy Strategy { get; set; }
        public Statistics Statistics { get; private set; }

        public Vector3 Position;
        public Vector3 Direction;

        public delegate void BirdUpdate(Bird bird, EventArgs e);
        public event BirdUpdate OnTick;

        public Bird(Vector3 position, Vector3 direction, Statistics stats)
        {
            Position = position;
            Direction = direction;
            Statistics = stats;
            Strategy = new NoStrategy();
        }

        public void Tick()
        {
            OnTick(this, new EventArgs());
            Strategy.Move(ref Position, ref Direction, Statistics);
            Statistics.ResetModificators();
            var log = LogManager.GetCurrentClassLogger();
            log.Trace("{0} moved to ({1};{2};{3}), direction = ({4};{5};{6})", Id, Position.X, Position.Y, Position.Z, Direction.X, Direction.Y, Direction.Z);
            log.Trace("{0} is following strategy: {1}", Id, Strategy.ToString());
        }
    }
}
