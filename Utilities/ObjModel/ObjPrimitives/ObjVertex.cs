using System.Runtime.InteropServices;
using OpenTK;

namespace Utilities.ObjModel.ObjPrimitives
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ObjVertex
    {
        public Vector2 TexCoord;
        public Vector3 Normal;
        public Vector3 Vertex;
    }
}
