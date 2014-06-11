using System.Drawing;
using OpenTK.Graphics.OpenGL;
using Utilities.Interfaces;

namespace Utilities.Shapes
{
    public class WorldCube : IRenderable
    {
        private int Size { get; set; }

        private readonly int _skybox = Textures.Textures.Skybox;

        public WorldCube(int size)
        {
            Size = size;
        }

        public void Render()
        {
            GL.Color3(Color.White);
            GL.PushMatrix();
            GL.Enable(EnableCap.Texture2D);

            GL.BindTexture(TextureTarget.Texture2D, _skybox);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0.25, 0.33); GL.Vertex3(Size, Size, -Size);
            GL.TexCoord2(0.25, 0); GL.Vertex3(-Size, Size, -Size);
            GL.TexCoord2(0.5, 0); GL.Vertex3(-Size, Size, Size);
            GL.TexCoord2(0.5, 0.33); GL.Vertex3(Size, Size, Size);
            GL.End();

            GL.BindTexture(TextureTarget.Texture2D, _skybox);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 0.66); GL.Vertex3(-Size, -Size, -Size);
            GL.TexCoord2(0, 0.35); GL.Vertex3(-Size, Size, -Size);
            GL.TexCoord2(0.25, 0.35); GL.Vertex3(Size, Size, -Size);
            GL.TexCoord2(0.25, 0.66); GL.Vertex3(Size, -Size, -Size);
            GL.End();

            GL.BindTexture(TextureTarget.Texture2D, _skybox);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0.25, 0.66); GL.Vertex3(Size, -Size, -Size);
            GL.TexCoord2(0.5, 0.66); GL.Vertex3(Size, -Size, Size);
            GL.TexCoord2(0.5, 0.35); GL.Vertex3(Size, Size, Size);
            GL.TexCoord2(0.25, 0.35); GL.Vertex3(Size, Size, -Size);
            GL.End();

            GL.BindTexture(TextureTarget.Texture2D, _skybox);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0.5, 0.66); GL.Vertex3(Size, -Size, Size);
            GL.TexCoord2(0.75, 0.66); GL.Vertex3(-Size, -Size, Size);
            GL.TexCoord2(0.75, 0.35); GL.Vertex3(-Size, Size, Size);
            GL.TexCoord2(0.5, 0.35); GL.Vertex3(Size, Size, Size);
            GL.End();

            GL.BindTexture(TextureTarget.Texture2D, _skybox);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0.75, 0.66); GL.Vertex3(-Size, -Size, Size);
            GL.TexCoord2(0.75, 0.35); GL.Vertex3(-Size, Size, Size);
            GL.TexCoord2(1, 0.35); GL.Vertex3(-Size, Size, -Size);
            GL.TexCoord2(1, 0.66); GL.Vertex3(-Size, -Size, -Size);
            GL.End();

            GL.BindTexture(TextureTarget.Texture2D, _skybox);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0.25, 1); GL.Vertex3(-Size, -Size, Size);
            GL.TexCoord2(0.5, 1); GL.Vertex3(Size, -Size, Size);
            GL.TexCoord2(0.5, 0.66); GL.Vertex3(Size, -Size, -Size);
            GL.TexCoord2(0.25, 0.66); GL.Vertex3(-Size, -Size, -Size);
            GL.End();

            GL.Disable(EnableCap.Texture2D);
            GL.PopMatrix();
        }
    }
}
