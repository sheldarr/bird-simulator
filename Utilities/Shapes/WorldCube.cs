using System.Drawing;
using OpenTK.Graphics.OpenGL;
using Utilities.Interfaces;

namespace Utilities.Shapes
{
    public class WorldCube : IRenderable
    {
        private int Size { get; set; }

        private readonly int _grassTexture = Textures.Textures.Grass;
        private readonly int _horizonTexture = Textures.Textures.Horizon;
        private readonly int _skyTexture = Textures.Textures.Sky;

        public WorldCube(int size)
        {
            Size = size;
        }

        public void Render()
        {
            GL.Color3(Color.White);
            GL.PushMatrix();
            GL.Enable(EnableCap.Texture2D);

            GL.BindTexture(TextureTarget.Texture2D, _skyTexture);
            GL.Begin(BeginMode.Quads);
            GL.TexCoord2(0, 1); GL.Vertex3(-Size, Size, Size);
            GL.TexCoord2(0, 0); GL.Vertex3(Size, Size, Size);
            GL.TexCoord2(1, 0); GL.Vertex3(Size, Size, -Size);
            GL.TexCoord2(1, 1); GL.Vertex3(-Size, Size, -Size);
            GL.End();

            GL.BindTexture(TextureTarget.Texture2D, _skyTexture);
            GL.Begin(BeginMode.Quads);
            GL.TexCoord2(0, 0); GL.Vertex3(-Size, Size, -Size);
            GL.TexCoord2(1, 0); GL.Vertex3(Size, Size, -Size);
            GL.TexCoord2(1, 1); GL.Vertex3(Size, -Size, -Size);
            GL.TexCoord2(0, 1); GL.Vertex3(-Size, -Size, -Size);
            GL.End();

            GL.BindTexture(TextureTarget.Texture2D, _skyTexture);
            GL.Begin(BeginMode.Quads);
            GL.TexCoord2(0, 0); GL.Vertex3(-Size, Size, Size);
            GL.TexCoord2(1, 0); GL.Vertex3(Size, Size, Size);
            GL.TexCoord2(1, 1); GL.Vertex3(Size, -Size, Size);
            GL.TexCoord2(0, 1); GL.Vertex3(-Size, -Size, Size);
            GL.End();

            GL.BindTexture(TextureTarget.Texture2D, _skyTexture);
            GL.Begin(BeginMode.Quads);
            GL.TexCoord2(0, 1); GL.Vertex3(Size, -Size, Size);
            GL.TexCoord2(0, 0); GL.Vertex3(Size, Size, Size);
            GL.TexCoord2(1, 0); GL.Vertex3(Size, Size, -Size);
            GL.TexCoord2(1, 1); GL.Vertex3(Size, -Size, -Size);
            GL.End();

            GL.BindTexture(TextureTarget.Texture2D, _skyTexture);
            GL.Begin(BeginMode.Quads);
            GL.TexCoord2(0, 1); GL.Vertex3(-Size, -Size, Size);
            GL.TexCoord2(0, 0); GL.Vertex3(-Size, Size, Size);
            GL.TexCoord2(1, 0); GL.Vertex3(-Size, Size, -Size);
            GL.TexCoord2(1, 1); GL.Vertex3(-Size, -Size, -Size);
            GL.End();

            GL.BindTexture(TextureTarget.Texture2D, _grassTexture);
            GL.Begin(BeginMode.Quads);
            GL.TexCoord2(0, 0); GL.Vertex3(-Size, -Size, Size);
            GL.TexCoord2(1, 0); GL.Vertex3(Size, -Size, Size);
            GL.TexCoord2(1, 1); GL.Vertex3(Size, -Size, -Size);
            GL.TexCoord2(0, 1); GL.Vertex3(-Size, -Size, -Size);
            GL.End();

            GL.Disable(EnableCap.Texture2D);
            GL.PopMatrix();
        }
    }
}
