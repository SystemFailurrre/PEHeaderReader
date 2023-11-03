namespace PEHeaderReader.Commands
{
    internal class AllHeaderCommand : IHeaderCommand
    {
        public void Process(byte[] fileBytes)
        {
            DOSHeaderCommand dOSHeaderCommand = new DOSHeaderCommand();
            dOSHeaderCommand.Process(fileBytes);
           
            NTHeaderCommand ntHeaderCommand = new NTHeaderCommand();
            ntHeaderCommand.Process(fileBytes);
            
            FileHeaderCommand fileHeaderCommand = new FileHeaderCommand();
            fileHeaderCommand.Process(fileBytes);

            OptionalHeaderCommand optionalHeaderCommand = new OptionalHeaderCommand();
            optionalHeaderCommand.Process(fileBytes);

            SectionHeaderCommand sectionHeaderCommand = new SectionHeaderCommand();
            sectionHeaderCommand.Process(fileBytes);
        }
    }
}
