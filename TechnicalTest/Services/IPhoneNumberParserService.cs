using TechnicalTest.Models;

namespace TechnicalTest.Services;

public interface IPhoneNumberParserService
{
    List<ParsedPhoneNumber> ParsePhoneNumbers(string inputText);
}

