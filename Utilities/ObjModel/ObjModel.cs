using System;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;
using Utilities.ObjModel.ObjPrimitives;

namespace Utilities.ObjModel
{
    public class ObjModel
    {
        public ObjVertex[] Vertices { get; set; }
        public ObjTriangle[] Triangles { get; set; }
        public ObjQuad[] Quads { get; set; }

        private int _verticesBufferId;
        private int _trianglesBufferId;
        private int _quadsBufferId;

        public void Render()
        {
            Prepare();

            GL.PushClientAttrib(ClientAttribMask.ClientVertexArrayBit);
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _verticesBufferId);
            GL.InterleavedArrays(InterleavedArrayFormat.T2fN3fV3f, Marshal.SizeOf(typeof(ObjVertex)), IntPtr.Zero);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _trianglesBufferId);
            GL.DrawElements(PrimitiveType.Triangles, Triangles.Length * 3, DrawElementsType.UnsignedInt, IntPtr.Zero);

            if (Quads.Length > 0)
            {
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, _quadsBufferId);
                GL.DrawElements(PrimitiveType.Quads, Quads.Length * 4, DrawElementsType.UnsignedInt, IntPtr.Zero);
            }

            GL.PopClientAttrib();
        }

        private void Prepare()
        {
            if (_verticesBufferId == 0)
            {
                GL.GenBuffers(1, out _verticesBufferId);
                GL.BindBuffer(BufferTarget.ArrayBuffer, _verticesBufferId);
                GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Vertices.Length * Marshal.SizeOf(typeof(ObjVertex))), Vertices, BufferUsageHint.StaticDraw);
            }

            if (_trianglesBufferId == 0)
            {
                GL.GenBuffers(1, out _trianglesBufferId);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, _trianglesBufferId);
                GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(Triangles.Length * Marshal.SizeOf(typeof(ObjTriangle))), Triangles, BufferUsageHint.StaticDraw);
            }

            if (_quadsBufferId == 0)
            {
                GL.GenBuffers(1, out _quadsBufferId);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, _quadsBufferId);
                GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(Quads.Length * Marshal.SizeOf(typeof(ObjQuad))), Quads, BufferUsageHint.StaticDraw);
            }
        }
        
    }
}
