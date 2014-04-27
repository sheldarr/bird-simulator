namespace BirdSimulator.Observer
{
    public class Observer
    {
        public void Subscribe(Bird.Bird bird)
        {
            bird.OnUpdate += (bird1, args) => System.Console.WriteLine(bird1.ToString() + " updated!");
        }
    }
}
