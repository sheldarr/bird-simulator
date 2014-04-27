using BirdSimulator.Bird;
using OpenTK;

namespace BirdSimulator.Interfaces
{
    public interface IStrategy
    {
        void Move(ref Vector3 position, Statistics statistics);
    }
}
