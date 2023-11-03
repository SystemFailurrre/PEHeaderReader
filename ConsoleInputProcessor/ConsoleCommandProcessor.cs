using PEHeaderReader.Commands;
using PEHeaderReader.ConsoleInputProcessor;
using System.CommandLine;

namespace PEHeaderReader.ConsoleUtilities
{
    
    class ConsoleCommandProcessor
    {
        private readonly Dictionary<HeadersToDisplay, IHeaderCommand> headerCommands = 
            new Dictionary<HeadersToDisplay, IHeaderCommand>
            {
                { HeadersToDisplay.ALL,  new AllHeaderCommand() },
                { HeadersToDisplay.DOS,  new DOSHeaderCommand() },
                { HeadersToDisplay.FILE, new FileHeaderCommand() },
                { HeadersToDisplay.NT,   new NTHeaderCommand() },
                { HeadersToDisplay.OPT,   new NTHeaderCommand() },
                { HeadersToDisplay.SEC,   new NTHeaderCommand() }
            };

        public void InitializeAndExecuteCommands(string[] args)
        {            
            var fileOption = CreateOption<string>(
                "--file", 
                "-f", 
                "The file to read and display on the console.", 
                true);
            var headerOption = CreateOption<HeadersToDisplay>(
                "--header", 
                "-hr",
                "Console Output Header. Can take the following arguments: " +
                    "ALL, DOS, FILE, NT, OPT, SEC. Default value is ALL.", 
                false);

            var rootCommand = CreateRootCommand(fileOption, 
                                                headerOption);

            rootCommand.Invoke(args);
        }

        private Option<T> CreateOption<T>(string longName, 
                                          string shortName, 
                                          string description, 
                                          bool isRequired)
        {
            var option = new Option<T>(longName)
            {
                Description = description,
                IsRequired = isRequired
            };

            option.AddAlias(shortName);
            return option;
        }

        private RootCommand CreateRootCommand(Option<string> fileOption, 
                                              Option<HeadersToDisplay> headerOption)
        {
            var rootCommand = new RootCommand("PE File Header Reader");
            rootCommand.Add(fileOption);
            rootCommand.Add(headerOption);

            rootCommand.SetHandler((file, header) => 
                { DoRootCommand(file, header); }, 
                fileOption, headerOption);

            return rootCommand;
        }

        public void DoRootCommand(string filePath, HeadersToDisplay header)
        {
            if (!File.Exists(filePath))
            {
                HandleFileNotExist();
                return;
            }

            byte[] fileBytes = File.ReadAllBytes(filePath);

            if (!PEFileValidator.IsValidPEFile(fileBytes))
            {
                HandleInvalidPEFile();
            }

            if (headerCommands.TryGetValue(header, out var strategy))
            {
                strategy.Process(fileBytes);
            }
        }

        private void HandleFileNotExist()
        {
            Console.WriteLine("The file does not exist at the specified path.");
        }

        private void HandleInvalidPEFile()
        {
            Console.WriteLine("The file at the specified path is not a valid PE file. " +
                "It may be corrupted or not conforming to the PE file format. " +
                "Attempting to read headers.");
        }
    }
}
