using PhoneNumbers;
using System.Text.RegularExpressions;
using TechnicalTest.Models;
using TechnicalTest.Utils;

namespace TechnicalTest.Services;

public class PhoneNumberParserService : IPhoneNumberParserService
{
    private string NumberPattern = @"\b\d+(-\d+)*\b";
    private string HindiPattern= @"\b(एक|दो|तीन|चार|पांच|छह|सात|आठ|नौ|शून्य)(\s(एक|दो|तीन|चार|पांच|छह|सात|आठ|नौ|शून्य))*\b";
    private string EnglishPattern = @"\b(one|two|three|four|five|six|seven|eight|nine|zero)(\s(one|two|three|four|five|six|seven|eight|nine|zero))*\b";
    public PhoneNumberParserService()
    {
    }


    public List<ParsedPhoneNumber> ParsePhoneNumbers(string inputText)
    {
        var phoneNumbers = new List<ParsedPhoneNumber>();

        // Combine the patterns
        string combinedPattern = $"{NumberPattern}|{HindiPattern}|{EnglishPattern}";

        // Perform the regex match
        Regex regex = new Regex(combinedPattern, RegexOptions.IgnoreCase);
        MatchCollection matches = regex.Matches(inputText);

        // Display the matches
        foreach (Match match in matches)
        {
            var rawPhoneNumber = match.Value;
            SeparatorType separator;
            var format = FormatDetectionHelper.GetFormat(rawPhoneNumber, out separator);
            if (format == null) continue;
            var phoneNumber = new ParsedPhoneNumber(rawPhoneNumber, format.Value, separator);
            phoneNumbers.Add(phoneNumber);
        }

        return phoneNumbers;
    }



}