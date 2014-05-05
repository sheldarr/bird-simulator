using OpenTK;

namespace GraphicsEngine.Camera
{
    public class Camera
    {
        public Vector3 Position { get; set; }
        public Vector3 Target { get; set; }
        public Vector3 CameraUp { get; set; }

        public Matrix4 LookAt;
        public float CameraSpeed { get; set; }

        private Vector3 Direction
        {
            get { return Target - Position; }
        }

        public Camera()
        {
            Position = new Vector3(3.0f, 3.0f, 3.0f);
            Target = new Vector3(0, 0, 0);
            CameraUp = new Vector3(0, 1, 0);
            CameraSpeed = 0.01f;
            LookAt = Matrix4.LookAt(Position, Target, CameraUp);
        }

        public void MoveForward()
        {
            Direction.NormalizeFast();
            Position += Direction * CameraSpeed;
            LookAt = Matrix4.LookAt(Position, Target, CameraUp);
        }

        public void MoveBackward()
        {
            Direction.NormalizeFast();
            Position -= Direction * CameraSpeed;
            LookAt = Matrix4.LookAt(Position, Target, CameraUp);
        }

        public void MoveLeft()
        {
            var direction = Target - Position;
            direction.NormalizeFast();
            var real = Vector3.Cross(direction, CameraUp);
            Position += real*CameraSpeed;
            LookAt = Matrix4.LookAt(Position, Target, CameraUp);
        }

        public void MoveRight()
        {
            var direction = Target - Position;
            direction.NormalizeFast();
            var real = -Vector3.Cross(direction, CameraUp);
            Position += real * CameraSpeed;
            LookAt = Matrix4.LookAt(Position, Target, CameraUp);
        }
    }
}
