using PEHeaderReader.Structures;
using PEHeaderReader.StructuredData;
using System.Runtime.InteropServices;

namespace PEHeaderReader.Commands
{
    internal class OptionalHeaderCommand : IHeaderCommand
    {
        public static long CalculateNTHeaderOffset(byte[] fileBytes)
        {
            long imageHeaderOffset = 0;

            IMAGE_DOS_HEADER imageDosHeader =
                StructuredDataReader.ReadStructureFromBytes<IMAGE_DOS_HEADER>(fileBytes, 0);

            IMAGE_FILE_HEADER imageFileHeader = new IMAGE_FILE_HEADER();
            int sizeOfFileHeaderInBytes = Marshal.SizeOf(imageFileHeader);

            imageHeaderOffset = imageDosHeader.e_lfanew + sizeof(uint) + sizeOfFileHeaderInBytes;

            return imageHeaderOffset;
        }

        public void Process(byte[] fileBytes)
        {
            IMAGE_OPTIONAL_HEADER imageOptionalHeader =
                StructuredDataReader.ReadStructureFromBytes<IMAGE_OPTIONAL_HEADER>(fileBytes,
                                                            CalculateNTHeaderOffset(fileBytes));

            ConsoleDataPrinter.PrintStruct(imageOptionalHeader);
        }
    }
}
