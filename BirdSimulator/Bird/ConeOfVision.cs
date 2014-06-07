using OpenTK;

namespace Engine.Bird
{
    public class ConeOfVision
    {
        private Vector3 _apex;
        private Vector3 _direction;
        private double _aperture;
        private double _viewDistance;

        public ConeOfVision(Vector3 apex, Vector3 direction, double aperture, double viewDistance)
        {
            _apex = apex;
            _direction = direction;
            _aperture = aperture;
            _viewDistance = viewDistance;
        }
    }
}
