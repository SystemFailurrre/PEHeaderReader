using System.Runtime.InteropServices;

namespace PEHeaderReader.StructuredData
{
    public static class StructuredDataReader
    {
        public static T ReadStructureFromBytes<T>(byte[] bytes, long offset)
        {
            EnsureValidParameters<T>(bytes, offset);
            IntPtr ptr = IntPtr.Zero;

            try
            {
                ptr = AllocateMemoryForStructure<T>();
                CopyBytesToMemory(bytes, offset, ptr, Marshal.SizeOf(typeof(T)));
                return ReadStructureFromMemory<T>(ptr);
            }
            finally
            {
                FreeMemory(ptr);
            }
        }

        private static void EnsureValidParameters<T>(byte[] bytes, long offset)
        {
            if (bytes == null || offset < 0 || offset + Marshal.SizeOf(typeof(T)) > bytes.Length)
            {
                throw new ArgumentException("Invalid input parameters.");
            }
        }

        private static IntPtr AllocateMemoryForStructure<T>()
        {
            return Marshal.AllocHGlobal(Marshal.SizeOf(typeof(T)));
        }

        private static void CopyBytesToMemory(byte[] bytes, long offset, IntPtr ptr, int size)
        {
            Marshal.Copy(bytes, (int)offset, ptr, size);
        }

        private static T ReadStructureFromMemory<T>(IntPtr ptr)
        {
            return (T)Marshal.PtrToStructure(ptr, typeof(T));
        }

        private static void FreeMemory(IntPtr ptr)
        {
            if (ptr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(ptr);
            }
        }
    }
}
