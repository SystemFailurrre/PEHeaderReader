using PEHeaderReader.Structures;
using PEHeaderReader.StructuredData;

namespace PEHeaderReader.Commands
{
    internal class NTHeaderCommand : IHeaderCommand
    {

        public static long CalculateNTHeaderOffset(byte[] fileBytes)
        {
            long peHeaderOffset = 0;
            
            IMAGE_DOS_HEADER imageDosHeader =
                StructuredDataReader.ReadStructureFromBytes<IMAGE_DOS_HEADER>(fileBytes, 0);
            peHeaderOffset = imageDosHeader.e_lfanew;

            return peHeaderOffset;
        }

        public void Process(byte[] fileBytes)
        {

            IMAGE_NT_HEADERS imageNTHeader =
                StructuredDataReader.ReadStructureFromBytes<IMAGE_NT_HEADERS>(fileBytes, 
                                                            CalculateNTHeaderOffset(fileBytes));
            ConsoleDataPrinter.PrintStruct(imageNTHeader);
        }
    }
}
