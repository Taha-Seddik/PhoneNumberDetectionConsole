using PhoneNumbers;
using System.Text;
using TechnicalTest.Services;

public class App
{
    private readonly IPhoneNumberParserService _phoneNumberParserService;

    public App(IPhoneNumberParserService phoneNumberParserService)  {
        _phoneNumberParserService = phoneNumberParserService;
    }

    public void Run(string[] args)
    {
        string inputText;

        if (args.Length > 0)
        {
            string filePath = args[1];
            if (File.Exists(filePath))
            {
                inputText = File.ReadAllText(filePath);
                ParseInput(inputText);
            }
            else
            {
                Console.WriteLine("File not found: " + filePath);
                return;
            }
        }
        else
        {
            bool exit = false;

            while (!exit)
            {
                ProcessInput();

                // Ask if the user wants to continue
                Console.WriteLine("Do you want to enter another input? (yes/no)");
                string response = Console.ReadLine();

                if (response.ToLower() != "yes")
                {
                    exit = true;
                }
            }
            
        }
    }

    private void ProcessInput()
    {
        Console.WriteLine("Enter text:");
        var inputText = Console.ReadLine();
        ParseInput(inputText);

    }

    private void ParseInput(string? inputText)
    {
        // Set console output encoding to UTF-8
        Console.OutputEncoding = Encoding.UTF8;

        var phoneNumbers = _phoneNumberParserService.ParsePhoneNumbers(inputText);

        if (!phoneNumbers.Any())
        {
            Console.WriteLine("No Phone number found");
            return;
        }

        foreach (var p in phoneNumbers)
        {
            Console.WriteLine(p.ToString());
        }
    }
}