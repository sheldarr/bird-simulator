using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Utilities.Interfaces;
using Utilities.ObjModel;

namespace Utilities.Models
{
    public class TreeModel : IRenderable
    {
        private const float ModelFactor = 0.04f;
        private readonly float _scale;
        private readonly Vector3 _realScale;

        private readonly Vector3 _position;
        
        private readonly Color _color = Color.DarkGreen;
        private readonly ObjModel.ObjModel _model = ObjModelLoader.Load(@"Resources\treeModel.obj"); 

        public TreeModel(Vector3 position, float scale)
        {
            _position = position;
            _scale = scale;
            _realScale = new Vector3(_scale * ModelFactor);
        }

        public void Render()
        {
            GL.Color3(_color);
            GL.Translate(_position.X, _position.Y - _scale, _position.Z);
            GL.Scale(_realScale);

            _model.Render();
        }
    }
}
