using PEHeaderReader.Structures;
using PEHeaderReader.StructuredData;

namespace PEHeaderReader.Commands
{
    internal class FileHeaderCommand : IHeaderCommand
    {
        public static long CalculateNTHeaderOffset(byte[] fileBytes)
        {
            long fileHeaderOffset = 0;

            IMAGE_DOS_HEADER imageDosHeader =
                StructuredDataReader.ReadStructureFromBytes<IMAGE_DOS_HEADER>(fileBytes, 
                                                                              fileHeaderOffset);

            fileHeaderOffset = imageDosHeader.e_lfanew + sizeof(uint);

            return fileHeaderOffset;
        }
        public void Process(byte[] fileBytes)
        {
            IMAGE_FILE_HEADER imageNTHeader =
                StructuredDataReader.ReadStructureFromBytes<IMAGE_FILE_HEADER>(fileBytes,
                                                       CalculateNTHeaderOffset(fileBytes));

            ConsoleDataPrinter .PrintStruct(imageNTHeader);
        }
    }
}
