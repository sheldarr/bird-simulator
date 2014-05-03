using BirdSimulator.Bird;
using BirdSimulator.Interfaces;
using OpenTK;

namespace BirdSimulator.Strategies
{
    public class NoStrategy : IStrategy
    {
        public void Move(ref Vector3 position, Statistics statistics) { }
    }
}
