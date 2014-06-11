namespace Engine.Bird
{
    public class Statistics
    {
        public float Speed { get; set; }
        public float SpeedModificator { get; set; }
        public VisionCone VisionVisionCone { get; set; }

        public Statistics(float speed, VisionCone visionVisionCone)
        {
            Speed = speed;
            SpeedModificator = 1;
            VisionVisionCone = visionVisionCone;
        }

        public void ResetModificators()
        {
            SpeedModificator = 1;
        }
    }
}
