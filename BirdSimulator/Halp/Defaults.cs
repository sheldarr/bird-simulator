using Engine.Bird;

namespace Engine.Halp
{
    public class Defaults
    {
        private const double _birdSpeed = 0.5;
        private const double _aperture = 170;
        private const double _viewDistance = 5.0;

        public static Statistics Statistics()
        {
            return new Statistics((float)_birdSpeed, VisionCone());
        }

        public static VisionCone VisionCone()
        {
            return new VisionCone(_aperture, _viewDistance);
        }
    }
}
