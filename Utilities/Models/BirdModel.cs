using System.Drawing;
using Engine.Bird;
using OpenTK;
using Utilities.Interfaces;
using Utilities.ObjModel;
using OpenTK.Graphics.OpenGL;

namespace Utilities.Models
{
    public class BirdModel : IRenderable
    {
        private readonly Bird _bird;
        private readonly Vector3 _scale;
        private readonly Color _color = Color.SaddleBrown;
        private readonly ObjModel.ObjModel _model = ObjModelLoader.Load(@"Resources\birdModel.obj");

        public BirdModel(Bird bird, float scale)
        {
            _bird = bird;
            _scale = new Vector3(scale);
        }

        public void Render()
        {
            GL.Color3(_color);
            GL.Translate(_bird.Position.X, _bird.Position.Y, _bird.Position.Z);

            RotateModel();

            GL.Scale(_scale);
            _model.Render();
        }

        private void RotateModel()
        {
            var a = D3Math.GetRotationBetween(new Vector3(0, 0, 1), _bird.Direction);
            var axis = new Vector3();
            float angle;
            a.ToAxisAngle(out axis, out angle);
            var b = D3Math.RadianToDegree(angle);
            GL.Rotate((float) b, axis);
        }
    }
}
