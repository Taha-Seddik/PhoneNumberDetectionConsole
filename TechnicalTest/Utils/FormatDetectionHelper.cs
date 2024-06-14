using System.Text.RegularExpressions;
using TechnicalTest.Models;

namespace TechnicalTest.Utils;

public static class FormatDetectionHelper
{
    private static readonly string[] HindiWords = { "एक", "दो", "तीन", "चार", "पांच", "छह", "सात", "आठ", "नौ", "शून्य" };
    private static readonly string[] EnglishWords = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "zero" };

    public static PhoneNumberParsedFormat? GetFormat(string str, out SeparatorType separator)
    {
        if (IsNumbers(str))
        {
            separator = GetNumberSeparator(str);
            return PhoneNumberParsedFormat.Numbers;
        }
        bool containsHindi;
        bool containsEnglish;
        var isHindiOrEnglishWords = IsMixedEnglishAndHindi(str, out containsHindi, out containsEnglish);
        if (isHindiOrEnglishWords)
        {
            separator = GetLanguageSeparator(str);
            if (!containsEnglish)
            {
                return PhoneNumberParsedFormat.HindiWords;
            }
            else if(!containsHindi)
            {
                return PhoneNumberParsedFormat.EnglishWords;
            }
            else
            {
                return PhoneNumberParsedFormat.EnglishAndHindiWords;
            }
        }
        separator = SeparatorType.None;
        return null;
    }

    public static bool IsNumbers(string input)
    {
        string numberPattern = @"^\d+(-\d+)*$";
        return Regex.IsMatch(input, numberPattern);
    }

    public static SeparatorType GetNumberSeparator(string input)
    {
        if (Regex.IsMatch(input, @"\d+-\d+"))
        {
            return SeparatorType.Dash;
        }
        else if (Regex.IsMatch(input, @"\d+\s\d+"))
        {
            return SeparatorType.WhiteSpace;
        }
        else
        {
            return SeparatorType.None;
        }
    }

    public static bool IsMixedEnglishAndHindi(string input, out bool containsHindi, out bool containsEnglish)
    {
        string hindiWords = @"(एक|दो|तीन|चार|पांच|छह|सात|आठ|नौ|शून्य)";
        string englishWords = @"(one|two|three|four|five|six|seven|eight|nine|zero)";
        containsHindi = Regex.IsMatch(input, hindiWords);
        containsEnglish = Regex.IsMatch(input, englishWords, RegexOptions.IgnoreCase);

        return containsHindi || containsEnglish;
    }

    public static SeparatorType GetLanguageSeparator(string input)
    {
        bool hasEnglishWords = ContainsWords(input, EnglishWords);
        bool hasHindiWords = ContainsWords(input, HindiWords);

        if (hasEnglishWords && !hasHindiWords)
        {
            return GetSeparatorType(input, EnglishWords);
        }
        else if (!hasEnglishWords && hasHindiWords)
        {
            return GetSeparatorType(input, HindiWords);
        }
        else
        {
            // If both English and Hindi words are present,
            // check if they are separated by dash or space
            SeparatorType englishSeparator = GetSeparatorType(input, EnglishWords);
            SeparatorType hindiSeparator = GetSeparatorType(input, HindiWords);

            if (englishSeparator == hindiSeparator)
            {
                return englishSeparator;
            }
            else
            {
                return SeparatorType.None; // Mixed separators
            }
        }
    }

    private static SeparatorType GetSeparatorType(string input, string[] words)
    {
        if (Regex.IsMatch(input, @"(\b(" + string.Join("|", words) + @")\s+\b|\b-\b)", RegexOptions.IgnoreCase))
        {
            return SeparatorType.WhiteSpace; // Space separator
        }
        else if (Regex.IsMatch(input, @"(\b(" + string.Join("|", words) + @")-\b)", RegexOptions.IgnoreCase))
        {
            return SeparatorType.Dash; // Dash separator
        }
        else
        {
            return SeparatorType.None; // No separator
        }
    }

    private static bool ContainsWords(string input, string[] words)
    {
        foreach (var word in words)
        {
            if (Regex.IsMatch(input, @"\b" + word + @"\b", RegexOptions.IgnoreCase))
            {
                return true;
            }
        }
        return false;
    }
}