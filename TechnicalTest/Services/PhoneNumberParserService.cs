using PhoneNumbers;
using System.Text.RegularExpressions;
using TechnicalTest.Models;
using TechnicalTest.Utils;

namespace TechnicalTest.Services;

public class PhoneNumberParserService : IPhoneNumberParserService
{
    private readonly PhoneNumberUtil _phoneNumberUtil;
    private Dictionary<PhoneNumberParsedFormat, string> FormatRegexPatterns { get; } = new Dictionary<PhoneNumberParsedFormat, string>
    {
        { PhoneNumberParsedFormat.CountryCodeSpaces, @"\+\d{1,3} \d{1,3} \d{3,4} \d{4}\b" },
        { PhoneNumberParsedFormat.CountryCodeDashes, @"\+\d{1,3}-\d{1,3}-\d{3,4}-\d{4}\b" },
        { PhoneNumberParsedFormat.CountryCodeNoDigitsSeparators, @"\+\d{1,3}-\d{7,10}\b" },
        { PhoneNumberParsedFormat.ParenthesesSpaces, @"\(\d{1,3}\)\s?\d{3,4} \d{4}\b" },
        { PhoneNumberParsedFormat.ParenthesesDashes, @"\(\d{1,3}\)\s?\d{3,4}-\d{4}\b" },
        { PhoneNumberParsedFormat.ParenthesesNoDigitsSeparators, @"\(\d{1,3}\)\s?\d{7,10}\b" },
        { PhoneNumberParsedFormat.NoCountryCodeSpaces, @"\b\d{1,3} \d{3,4} \d{4}\b" },
        { PhoneNumberParsedFormat.NoCountryCodeDashes, @"\b\d{1,3}-\d{3,4}-\d{4}\b" },
        { PhoneNumberParsedFormat.NoCountryCodeNoDigitsSeparators, @"\b\d{10,11}\b" },
        { PhoneNumberParsedFormat.CombinationEnglishHindi, @"\b((ONE|TWO|THREE|FOUR|FIVE|SIX|SEVEN|EIGHT|NINE|ZERO|एक|दो|तीन|चार|पांच|छह|सात|आठ|नौ|शून्य)[ -]){9}(ONE|TWO|THREE|FOUR|FIVE|SIX|SEVEN|EIGHT|NINE|ZERO|एक|दो|तीन|चार|पांच|छह|सात|आठ|नौ|शून्य)\b" }
    };

    public PhoneNumberParserService()
    {
        _phoneNumberUtil = PhoneNumberUtil.GetInstance();
    }

    public List<ParsedPhoneNumber> ParsePhoneNumbers(string inputText)
    {
        var phoneNumbers = new List<ParsedPhoneNumber>();
        var seenPhoneNumbersWithCountryCode = new HashSet<string>();
        var formatRegexPatterns = FormatRegexPatterns;

        foreach (var x in formatRegexPatterns)
        {
            var format = x.Key;
            var pattern = x.Value;
            var regex = new Regex(pattern);
            var matches = regex.Matches(inputText);
            var separatorType = GetSeparatorType(format);
            foreach (Match match in matches)
            {
                switch (format)
                {
                    // Handle each format accordingly
                    case PhoneNumberParsedFormat.CountryCodeSpaces:
                    case PhoneNumberParsedFormat.CountryCodeDashes:
                    case PhoneNumberParsedFormat.CountryCodeNoDigitsSeparators:
                        {
                            var rawPhoneNumber = match.Value;
                            var nationalNumber = _phoneNumberUtil.Parse(rawPhoneNumber, "US").NationalNumber.ToString();
                            var phoneNumber = new ParsedPhoneNumber(nationalNumber, rawPhoneNumber, format, separatorType);
                            phoneNumbers.Add(phoneNumber);
                            seenPhoneNumbersWithCountryCode.Add(nationalNumber);
                            break;
                        }
                    case PhoneNumberParsedFormat.NoCountryCodeSpaces:
                    case PhoneNumberParsedFormat.NoCountryCodeDashes:
                    case PhoneNumberParsedFormat.NoCountryCodeNoDigitsSeparators:
                        {
                            var rawPhoneNumber = match.Value;
                            var nationalNumber = _phoneNumberUtil.Parse(rawPhoneNumber, "US").NationalNumber.ToString();
                            if (seenPhoneNumbersWithCountryCode.Contains(nationalNumber))
                            {
                                break;
                            }
                            var phoneNumber = new ParsedPhoneNumber(nationalNumber, rawPhoneNumber, format, separatorType);
                            phoneNumbers.Add(phoneNumber);
                            break;
                        }
                    case PhoneNumberParsedFormat.ParenthesesSpaces:
                    case PhoneNumberParsedFormat.ParenthesesDashes:
                    case PhoneNumberParsedFormat.ParenthesesNoDigitsSeparators:
                        {
                            var rawPhoneNumber = match.Value;
                            var nationalNumber = PhoneNumberDigitsUtils.ExtractDigitsAfterClosingParenthesis(rawPhoneNumber);
                            var phoneNumber = new ParsedPhoneNumber(nationalNumber, rawPhoneNumber, format, separatorType);
                            phoneNumbers.Add(phoneNumber);
                            seenPhoneNumbersWithCountryCode.Add(nationalNumber);
                            break;
                        }
                    case PhoneNumberParsedFormat.CombinationEnglishHindi:
                        {
                            var textedPhoneNbr = match.Value;
                            var digits = LanguageUtils.WordsToDigits(textedPhoneNbr);
                            var wordsFormat = LanguageUtils.ContainsOnlyEnglish(textedPhoneNbr) ? PhoneNumberParsedFormat.EnglishWords :
                                              LanguageUtils.ContainsOnlyHindi(textedPhoneNbr) ? PhoneNumberParsedFormat.HindiWords :
                                              PhoneNumberParsedFormat.CombinationEnglishHindi;
                            var phoneNumber = new ParsedPhoneNumber(digits, textedPhoneNbr, wordsFormat, SeparatorType.WhiteSpace);
                            phoneNumbers.Add(phoneNumber);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }

        return phoneNumbers;
    }


    public SeparatorType GetSeparatorType(PhoneNumberParsedFormat format)
    {
        if (format.ToString().Contains("Spaces"))
        {
            return SeparatorType.WhiteSpace;
        }
        else if (format.ToString().Contains("Dashes"))
        {
            return SeparatorType.Dash;
        }
        else
        {
            return SeparatorType.None;
        }
    }

}