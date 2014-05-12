using Engine.Bird;
using Engine.Interfaces;
using OpenTK;

namespace Engine.Strategies
{
    public class NoStrategy : IStrategy
    {
        public void Move(ref Vector3 position,ref Vector3 direction, Statistics statistics) { }
    }
}
