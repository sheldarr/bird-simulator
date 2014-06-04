using System.Drawing;
using OpenTK.Graphics.OpenGL;
using Utilities.Interfaces;

namespace Utilities.Shapes
{
    public class Cube : IRenderable
    {
        public int Size { get; set; }
        public Color Color { get; set; }

        public Cube(int size, Color color)
        {
            Size = size;
            Color = color;
        }

        public void Render()
        {
            GL.PushMatrix();
            GL.Color3(Color);

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
