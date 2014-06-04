using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Utilities.Interfaces;
using Utilities.ObjModel;

namespace Utilities.Models
{
    public class TreeModel : IRenderable
    {
        private readonly Vector3 _position;
        private readonly Vector3 _scale;
        private readonly Color _color = Color.DarkGreen;
        private readonly ObjModel.ObjModel _model = ObjModelLoader.Load(@"Resources\treeModel.obj"); 

        public TreeModel(Vector3 position, float scale)
        {
            _position = position;
            _scale = new Vector3(scale * 0.02f);
        }

        public void Render()
        {
            GL.Color3(_color);
            GL.Translate(_position.X, _position.Y, _position.Z);
            GL.Scale(_scale);

            _model.Render();
        }
    }
}
