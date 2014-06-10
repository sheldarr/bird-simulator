using System.Drawing;
using OpenTK.Graphics.OpenGL;
using Utilities.Interfaces;

namespace Utilities.Shapes
{
    public class WorldCube : IRenderable
    {
        private int Size { get; set; }

        private readonly int _skyboxUp = Textures.Textures.SkyboxUp;
        private readonly int _skyboxLeft = Textures.Textures.SkyboxLeft;
        private readonly int _skyboxFront = Textures.Textures.SkyboxFront;
        private readonly int _skyboxRight = Textures.Textures.SkyboxRight;
        private readonly int _skyboxBack = Textures.Textures.SkyboxBack;
        private readonly int _skyboxDown = Textures.Textures.SkyboxDown;

        public WorldCube(int size)
        {
            Size = size;
        }

        public void Render()
        {
            GL.Color3(Color.White);
            GL.PushMatrix();
            GL.Enable(EnableCap.Texture2D);

            GL.BindTexture(TextureTarget.Texture2D, _skyboxUp);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 1); GL.Vertex3(-Size, Size, Size);
            GL.TexCoord2(0, 0); GL.Vertex3(Size, Size, Size);
            GL.TexCoord2(1, 0); GL.Vertex3(Size, Size, -Size);
            GL.TexCoord2(1, 1); GL.Vertex3(-Size, Size, -Size);
            GL.End();

            GL.BindTexture(TextureTarget.Texture2D, _skyboxLeft);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 1); GL.Vertex3(-Size, -Size, -Size);
            GL.TexCoord2(0, 0); GL.Vertex3(-Size, Size, -Size);
            GL.TexCoord2(1, 0); GL.Vertex3(Size, Size, -Size);
            GL.TexCoord2(1, 1); GL.Vertex3(Size, -Size, -Size);
            GL.End();

            GL.BindTexture(TextureTarget.Texture2D, _skyboxFront);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(1, 1); GL.Vertex3(Size, -Size, Size);
            GL.TexCoord2(1, 0); GL.Vertex3(Size, Size, Size);
            GL.TexCoord2(0, 0); GL.Vertex3(Size, Size, -Size);
            GL.TexCoord2(0, 1); GL.Vertex3(Size, -Size, -Size);
            GL.End();

            GL.BindTexture(TextureTarget.Texture2D, _skyboxRight);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(1, 1); GL.Vertex3(-Size, -Size, Size);
            GL.TexCoord2(1, 0); GL.Vertex3(-Size, Size, Size);
            GL.TexCoord2(0, 0); GL.Vertex3(Size, Size, Size);
            GL.TexCoord2(0, 1); GL.Vertex3(Size, -Size, Size);
            GL.End();

            GL.BindTexture(TextureTarget.Texture2D, _skyboxBack);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 1); GL.Vertex3(-Size, -Size, Size);
            GL.TexCoord2(0, 0); GL.Vertex3(-Size, Size, Size);
            GL.TexCoord2(1, 0); GL.Vertex3(-Size, Size, -Size);
            GL.TexCoord2(1, 1); GL.Vertex3(-Size, -Size, -Size);
            GL.End();

            GL.BindTexture(TextureTarget.Texture2D, _skyboxDown);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(1, 1); GL.Vertex3(-Size, -Size, Size);
            GL.TexCoord2(1, 0); GL.Vertex3(Size, -Size, Size);
            GL.TexCoord2(0, 0); GL.Vertex3(Size, -Size, -Size);
            GL.TexCoord2(0, 1); GL.Vertex3(-Size, -Size, -Size);
            GL.End();

            GL.Disable(EnableCap.Texture2D);
            GL.PopMatrix();
        }
    }
}
