using OpenTK;
using Utilities;

namespace GraphicsEngine.Camera
{
    public class Camera
    {
        public Vector3 Position { get; set; }
        public Vector3 CameraUp { get; set; }
        public Vector3 Direction { get; set; }

        public Vector3 Target 
        {
            get { return Position - Direction; }
        }

        public Matrix4 LookAt;
        public float CameraSpeed { get; set; } 

        public Camera()
        {
            Position = new Vector3(3, 3, 3);
            Direction = new Vector3(3, 3, 3);
            CameraUp = new Vector3(0, 1, 0);
            CameraSpeed = 0.05f;
            LookAt = Matrix4.LookAt(Position, Target, CameraUp);
        }

        public void MoveForward()
        {
            Direction.NormalizeFast();
            Position -= Direction * CameraSpeed;
            LookAt = Matrix4.LookAt(Position, Target, CameraUp);
        }

        public void MoveBackward()
        {
            Direction.NormalizeFast();
            Position += Direction * CameraSpeed;
            LookAt = Matrix4.LookAt(Position, Target, CameraUp);
        }

        public void MoveLeft()
        {
            Direction.NormalizeFast();

            var perpendicularVector = Vector3.Cross(Direction, Vector3.UnitY);
            perpendicularVector.NormalizeFast();

            Position += perpendicularVector * CameraSpeed;
            //Direction -= perpendicularVector * CameraSpeed;

            LookAt = Matrix4.LookAt(Position, Target, CameraUp);
        }

        public void MoveRight()
        {
            Direction.NormalizeFast();

            var perpendicularVector = Vector3.Cross(Direction, Vector3.UnitY);
            perpendicularVector.NormalizeFast();

            Position -= perpendicularVector * CameraSpeed;
            //Direction += perpendicularVector * CameraSpeed;

            LookAt = Matrix4.LookAt(Position, Target, CameraUp);
        }

        public void SetTarget(float x, float y)
        {
            //var look = D3Math.RotatePoint(Position.Xz, Target.Xz, x);
            //var newLook = new Vector3(look.X, Direction.Y, look.Y);
            //LookAt = Matrix4.LookAt(Position, Position - newLook, CameraUp);

            ////var perpendicularVector = Vector3.Cross(direction, Vector3.UnitY);
            //perpendicularVector.NormalizeFast();

            //Position -= perpendicularVector * CameraSpeed;
            //Target -= perpendicularVector * CameraSpeed;

            //LookAt = Matrix4.LookAt(Position, Target, CameraUp);
        }
    }
}
