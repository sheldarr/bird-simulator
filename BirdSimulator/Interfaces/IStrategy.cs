using Engine.Bird;
using OpenTK;

namespace Engine.Interfaces
{
    public interface IStrategy
    {
        void Move(ref Vector3 position, ref Vector3 direction,Statistics statistics);
        string ToString();
    }
}
