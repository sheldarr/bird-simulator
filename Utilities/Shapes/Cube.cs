using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Utilities.Interfaces;

namespace Utilities.Shapes
{
    public class Cube : IRenderable
    {
        public float Size { get; set; }
        public Vector3 Position { get; set; }
        public Color Color { get; set; }

        public Cube(float size, Vector3 position, Color color)
        {
            Size = size;
            Position = position;
            Color = color;
        }

        public void Render()
        {
            GL.PushMatrix();
            GL.Color3(Color);
            GL.Translate(Position);

            GL.Begin(PrimitiveType.LineStrip);
            GL.Vertex3(-Size, Size, -Size);
            GL.Vertex3(Size, Size, -Size);
            GL.Vertex3(Size, -Size, -Size);
            GL.Vertex3(-Size, -Size, -Size);
            GL.Vertex3(-Size, Size, -Size);
            GL.End();

            GL.Begin(PrimitiveType.LineStrip);
            GL.Vertex3(-Size, Size, Size);
            GL.Vertex3(Size, Size, Size);
            GL.Vertex3(Size, -Size, Size);
            GL.Vertex3(-Size, -Size, Size);
            GL.Vertex3(-Size, Size, Size);
            GL.End();

            GL.Begin(PrimitiveType.LineStrip);
            GL.Vertex3(-Size, Size, Size);
            GL.Vertex3(Size, Size, Size);
            GL.Vertex3(Size, Size, -Size);
            GL.Vertex3(-Size, Size, -Size);
            GL.Vertex3(-Size, Size, Size);
            GL.End();

            GL.Begin(PrimitiveType.LineStrip);
            GL.Vertex3(Size, -Size, Size);
            GL.Vertex3(Size, Size, Size);
            GL.Vertex3(Size, Size, -Size);
            GL.Vertex3(Size, -Size, -Size);
            GL.Vertex3(Size, -Size, Size);
            GL.End();

            GL.Begin(PrimitiveType.LineStrip);
            GL.Vertex3(-Size, -Size, Size);
            GL.Vertex3(-Size, Size, Size);
            GL.Vertex3(-Size, Size, -Size);
            GL.Vertex3(-Size, -Size, -Size);
            GL.Vertex3(-Size, -Size, Size);
            GL.End();

            GL.Begin(PrimitiveType.LineStrip);
            GL.Vertex3(-Size, -Size, Size);
            GL.Vertex3(Size, -Size, Size);
            GL.Vertex3(Size, -Size, -Size);
            GL.Vertex3(-Size, -Size, -Size);
            GL.Vertex3(-Size, -Size, Size);
            GL.End();

            GL.PopMatrix();
        }
    }
}
