using System.Runtime.InteropServices;

namespace PEHeaderReader.Structures
{
    [StructLayout(LayoutKind.Explicit)]
    public struct IMAGE_DOS_HEADER
    {
        [FieldOffset(0)]
        public ushort e_magic;
        [FieldOffset(60)]
        public ushort e_lfanew;

    }
}
