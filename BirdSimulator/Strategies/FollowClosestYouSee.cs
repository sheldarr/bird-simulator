using System.Linq;
using Engine.Bird;
using Engine.Halp;
using Engine.Interfaces;
using Newtonsoft.Json;
using NLog;
using OpenTK;

namespace Engine.Strategies
{
    class FollowClosestYouSee : IStrategy
    {
        private readonly Observer.Observer _observer;
        private readonly double _minDistance;

        public FollowClosestYouSee(Observer.Observer observer, double minDistance)
        {
            _observer = observer;
            _minDistance = minDistance;
        }

        public void Move(ref Vector3 position, ref Vector3 direction, Statistics statistics)
        {
            statistics.VisionVisionCone.Apex = new Point(position);
            statistics.VisionVisionCone.Direction = new Vector(direction);
            var birdsInSight =
                _observer.Inside(statistics.VisionVisionCone).ToList();

            if (birdsInSight.Count == 0)
            {
                LogManager.GetCurrentClassLogger().Info("nothing to follow");
                return;
            }

            var birdToFollow = birdsInSight.First();
            LogManager.GetCurrentClassLogger().Info("following {0}, in vision cone {1}", birdToFollow.Id, JsonConvert.SerializeObject(statistics.VisionVisionCone));
            var followingStrat = new FollowThatGuy(birdToFollow, _minDistance);
            followingStrat.Move(ref position, ref direction, statistics);
        }

        public new string ToString()
        {
            return "trying to follow closest bird";
        }
    }
}
