namespace PEHeaderReader.Commands
{
    public interface IHeaderCommand
    {
        void Process(byte[] fileBytes);
    }
}
