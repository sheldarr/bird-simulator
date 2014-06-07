namespace Engine.Bird
{
    public class Statistics
    {
        public float Speed { get; set; }
        public float SpeedModificator { get; set; }
        public ConeOfVision VisionCone { get; set; }

        public Statistics(float speed, ConeOfVision visionCone)
        {
            Speed = speed;
            SpeedModificator = 1;
            VisionCone = visionCone;
        }

        public void ResetModificators()
        {
            SpeedModificator = 1;
        }
    }
}
