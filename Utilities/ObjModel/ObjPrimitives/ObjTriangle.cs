using System.Runtime.InteropServices;

namespace Utilities.ObjModel.ObjPrimitives
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ObjTriangle
    {
        public int Index0;
        public int Index1;
        public int Index2;
    }
}
