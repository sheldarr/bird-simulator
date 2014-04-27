using System.Collections.Generic;

namespace BirdSimulator.World
{
    public class World
    {
        public IEnumerable<Bird.Bird> Birds;

        public World(IEnumerable<Bird.Bird> birds)
        {
            Birds = birds;
        }
    }
}
