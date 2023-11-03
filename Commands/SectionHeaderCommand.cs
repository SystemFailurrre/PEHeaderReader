using PEHeaderReader.StructuredData;
using PEHeaderReader.Structures;
using System.Runtime.InteropServices;

namespace PEHeaderReader.Commands
{
    internal class SectionHeaderCommand : IHeaderCommand
    {
        public static long CalculateSectionHeaderOffset(byte[] fileBytes)
        {
            long sectionHeaderOffset = 0;

            IMAGE_DOS_HEADER imageDosHeader =
                StructuredDataReader.ReadStructureFromBytes<IMAGE_DOS_HEADER>(fileBytes, 0);

            IMAGE_NT_HEADERS imageNtHeader = new IMAGE_NT_HEADERS();

            long sizeOfNtHeaderInBytes = Marshal.SizeOf(imageNtHeader);

            sectionHeaderOffset = imageDosHeader.e_lfanew + sizeOfNtHeaderInBytes;

            return sectionHeaderOffset;
        }
        public void Process(byte[] fileBytes)
        {            
            var sections = new List<IMAGE_SECTION_HEADER>();
                     
            IMAGE_NT_HEADERS imageNtHeader = ReadNtHeader(fileBytes);
                        
            int sectionOffset = 0;
            int sectionOffsetIncrement = 40;
                        
            for (int i = 0; i < imageNtHeader.FileHeader.NumberOfSections; i++)
            {
                IMAGE_SECTION_HEADER imageSectionHeader = 
                    ReadSectionHeader(fileBytes, 
                                      CalculateSectionHeaderOffset(fileBytes) + sectionOffset);
                
                sections.Add(imageSectionHeader);
                            
                ConsoleDataPrinter.PrintStruct(imageSectionHeader);
                                
                sectionOffset += sectionOffsetIncrement;
            }
        }

        private IMAGE_NT_HEADERS ReadNtHeader(byte[] fileBytes)
        {
            long ntHeaderOffset = NTHeaderCommand.CalculateNTHeaderOffset(fileBytes);

            return StructuredDataReader.
                ReadStructureFromBytes<IMAGE_NT_HEADERS>(fileBytes, ntHeaderOffset);
        }

        private IMAGE_SECTION_HEADER ReadSectionHeader(byte[] fileBytes, long offset)
        {
            return StructuredDataReader.
                ReadStructureFromBytes<IMAGE_SECTION_HEADER>(fileBytes, offset);
        }
    }
}
