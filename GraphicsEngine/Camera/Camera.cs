using OpenTK;
using Utilities;

namespace GraphicsEngine.Camera
{
    public class Camera
    {
        public float Speed { get; set; }
        public float MaxVerticalAngle { get; set; }
        public float MaxHorizontalAngle { get; set; }

        public Vector3 Position { get; set; }
        public Vector3 CameraUp { get; set; }
        public Vector3 Direction { get; set; }

        public Vector3 Target 
        {
            get { return (Position - Direction); }
        }

        public Matrix4 LookAt;

        public Camera(float speed, float maxVerticalAngle, float maxHorizontalAngle, Vector3 cameraPosition, Vector3 cameraDirection)
        {
            Speed = speed;
            MaxVerticalAngle = maxVerticalAngle;
            MaxHorizontalAngle = maxHorizontalAngle;

            Position = cameraPosition;
            Direction = cameraDirection;

            CameraUp = new Vector3(0, 1, 0);
            LookAt = Matrix4.LookAt(Position, Target, CameraUp);
        }

        public void MoveForward()
        {
            Direction.NormalizeFast();
            Position -= Direction * Speed;
            LookAt = Matrix4.LookAt(Position, Target, CameraUp);
        }

        public void MoveBackward()
        {
            Direction.NormalizeFast();
            Position += Direction * Speed;
            LookAt = Matrix4.LookAt(Position, Target, CameraUp);
        }

        public void MoveLeft()
        {
            Direction.NormalizeFast();

            var perpendicularVector = Vector3.Cross(Direction, Vector3.UnitY);
            perpendicularVector.NormalizeFast();

            Position += perpendicularVector * Speed;

            LookAt = Matrix4.LookAt(Position, Target, CameraUp);
        }

        public void MoveRight()
        {
            Direction.NormalizeFast();

            var perpendicularVector = Vector3.Cross(Direction, Vector3.UnitY);
            perpendicularVector.NormalizeFast();

            Position -= perpendicularVector * Speed;

            LookAt = Matrix4.LookAt(Position, Target, CameraUp);
        }

        public void SetTarget(float x, float y)
        {
            var angle = -(MaxVerticalAngle * y);
            var rotation = Matrix4.CreateRotationX((float)D3Math.DegreeToRadian(angle));
            Direction = Vector3.TransformNormal(new Vector3(0, 0, 1), rotation);

            angle = -(MaxHorizontalAngle * x);
            rotation = Matrix4.CreateRotationY((float)D3Math.DegreeToRadian(angle));
            Direction = Vector3.TransformNormal(Direction, rotation);

            LookAt = Matrix4.LookAt(Position, Target, CameraUp);
        }
    }
}
