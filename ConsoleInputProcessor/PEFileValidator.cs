using PEHeaderReader.Commands;
using PEHeaderReader.StructuredData;
using PEHeaderReader.Structures;

namespace PEHeaderReader.ConsoleInputProcessor
{
    public class PEFileValidator
    {
        private const uint PeHeaderSignature = 0x4550;
        private const ushort DosHeaderSignature = 0x5A4D;
        public static bool IsValidPEFile(byte[] fileBytes)
        {
            if (!IsValidDosHeader(fileBytes))
            {
                return false;
            }

            if (!IsValidPEHeader(fileBytes))
            {
                return false;
            }
                        
            return true;
        }

        private static bool IsValidDosHeader(byte[] fileBytes)
        {
            ushort dosSignature = BitConverter.ToUInt16(fileBytes, 0);

            return dosSignature == DosHeaderSignature;
        }

        private static bool IsValidPEHeader(byte[] fileBytes)
        {
            long imageNtHeaderOffset= NTHeaderCommand.CalculateNTHeaderOffset(fileBytes);
            IMAGE_NT_HEADERS imageNtHeader = 
                StructuredDataReader.ReadStructureFromBytes<IMAGE_NT_HEADERS>(fileBytes,
                                                                              imageNtHeaderOffset);
            return imageNtHeader.Signature == PeHeaderSignature;           
        }
    }
}
