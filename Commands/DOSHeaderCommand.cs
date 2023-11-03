using PEHeaderReader.Structures;
using PEHeaderReader.StructuredData;

namespace PEHeaderReader.Commands
{
    internal class DOSHeaderCommand : IHeaderCommand
    {
        public void Process(byte[] fileBytes)
        {
            IMAGE_DOS_HEADER imageDosHeader =
                StructuredDataReader.ReadStructureFromBytes<IMAGE_DOS_HEADER>(fileBytes, 0);
                        
            ConsoleDataPrinter.PrintStruct(imageDosHeader);
        }        
    }
}
