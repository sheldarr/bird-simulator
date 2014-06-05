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
            get { return (Position - Direction); }
        }

        public Matrix4 LookAt;
        public float CameraSpeed { get; set; } 

        public Camera()
        {
            Position = new Vector3(-190, -190, -190);
            Direction = new Vector3(-3, -3, -3);
            CameraUp = new Vector3(0, 1, 0);
            CameraSpeed = 1.0f;
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
            var angle = -(89 * y);
            //if (angle > 180)
            //{
            //    angle = 180 - (angle - 180);
            //}

            //if (angle < -180)
            //{
            //    angle = 180 - (angle - 180);
            //}

            var rotation = Matrix4.CreateRotationX((float)D3Math.DegreeToRadian(angle));
            Direction = Vector3.TransformNormal(new Vector3(0, 0, 1), rotation);

            angle = -(270 * x);
            rotation = Matrix4.CreateRotationY((float)D3Math.DegreeToRadian(angle));
            Direction = Vector3.TransformNormal(Direction, rotation);

            LookAt = Matrix4.LookAt(Position, Target, CameraUp);
        }
    }
}
