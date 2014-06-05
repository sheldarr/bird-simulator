using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace Utilities.Text
{
    public class Text
    {
        public string Content { get; set; }
        public Point Position { get; set; }
        public Font Font { get; set; }
        public Brush Brush { get; set; }

        private Bitmap Bitmap { get; set; }
        private readonly int _textureId;
        private Size _clientSize;

        public Text(Size clientSize, Size areaSize, Point position, string content)
        {
            Bitmap = new Bitmap(areaSize.Width, areaSize.Height);
            _clientSize = clientSize;
            _textureId = CreateTexture();

            Content = content;   
            Position = position;
            Font = new Font(FontFamily.GenericSansSerif, 12);
            Brush = Brushes.Yellow;
            UpdateText();
        }

        private int CreateTexture()
        {
            int textureId;
            GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (float)TextureEnvMode.Replace);
            var bitmap = Bitmap;
            GL.GenTextures(1, out textureId);
            GL.BindTexture(TextureTarget.Texture2D, textureId);

            var data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.Finish();
            bitmap.UnlockBits(data);
            return textureId;
        }

        public void Dispose()
        {
            if (_textureId > 0)
                GL.DeleteTexture(_textureId);
        }

        public void UpdateText()
        {
            using (var gfx = Graphics.FromImage(Bitmap))
            {
                gfx.Clear(Color.Black);
                gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                gfx.DrawString(Content , Font, Brush, new PointF(0, 0));
            }

            var data = Bitmap.LockBits(new Rectangle(0, 0, Bitmap.Width, Bitmap.Height),
            ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            GL.TexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, Bitmap.Width, Bitmap.Height, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            Bitmap.UnlockBits(data);
        }

        public void Draw()
        {
            GL.PushMatrix();
            GL.LoadIdentity();

            var orthoProjection = Matrix4.CreateOrthographicOffCenter(0, _clientSize.Width, _clientSize.Height, 0, -1, 1);
            GL.MatrixMode(MatrixMode.Projection);
    
            GL.PushMatrix();
            GL.LoadMatrix(ref orthoProjection);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.DstColor);
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, _textureId);

            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 0); GL.Vertex2(Position.X, Position.Y);
            GL.TexCoord2(1, 0); GL.Vertex2(Position.X + Bitmap.Width, Position.Y);
            GL.TexCoord2(1, 1); GL.Vertex2(Position.X + Bitmap.Width, Position.Y + Bitmap.Height);
            GL.TexCoord2(0, 1); GL.Vertex2(Position.X, Position.Y + Bitmap.Height);
            GL.End();
            GL.PopMatrix();

            GL.Disable(EnableCap.Blend);
            GL.Disable(EnableCap.Texture2D);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.PopMatrix();
        }
    }
}
