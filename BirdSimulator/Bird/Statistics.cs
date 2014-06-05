namespace Engine.Bird
{
    public class Statistics
    {
        public float Speed { get; set; }
        public float SpeedModificator { get; set; }

        public Statistics(float speed)
        {
            Speed = speed;
            SpeedModificator = 1;
        }

        public void ResetModificators()
        {
            SpeedModificator = 1;
        }
    }
}
