using PEHeaderReader.ConsoleUtilities;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;

namespace PEHeaderReader.Application
{
   
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleCommandProcessor consoleCommandProcessor = 
                new ConsoleCommandProcessor();

            consoleCommandProcessor.InitializeAndExecuteCommands(args);
        }
    }
}