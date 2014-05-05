using System.Runtime.InteropServices;

namespace Utilities.ObjModel.ObjPrimitives
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ObjQuad
    {
        public int Index0;
        public int Index1;
        public int Index2;
        public int Index3;
    }
}
