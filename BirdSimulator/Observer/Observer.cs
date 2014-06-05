using System;

namespace Engine.Observer
{
    public class Observer
    {
        public void Subscribe(Bird.Bird bird)
        {
            bird.OnUpdate += LogBirdUpdate;
        }

        private void LogBirdUpdate(Bird.Bird bird, EventArgs e)
        {
            Console.WriteLine("Bird {0} moved to ({1},{2},{3})", bird.Id, bird.Position.X, bird.Position.Y, bird.Position.Z);
        }
    }
}
