using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalTest.Models;

namespace TechnicalTest.Extensions;

public static class PhoneNumberParsedFormatExtensions
{
    public static string ToLabel(this PhoneNumberParsedFormat format)
    {
        switch (format)
        {
            case PhoneNumberParsedFormat.CountryCodeSpaces:
                return "Country Code with Spaces";
            case PhoneNumberParsedFormat.CountryCodeDashes:
                return "Country Code with Dashes";
            case PhoneNumberParsedFormat.CountryCodeNoDigitsSeparators:
                return "Country Code without Digits Separators";
            case PhoneNumberParsedFormat.ParenthesesSpaces:
                return "Parentheses with Spaces";
            case PhoneNumberParsedFormat.ParenthesesDashes:
                return "Parentheses with Dashes";
            case PhoneNumberParsedFormat.ParenthesesNoDigitsSeparators:
                return "Parentheses without Digits Separators";
            case PhoneNumberParsedFormat.NoCountryCodeSpaces:
                return "No Country Code with Spaces";
            case PhoneNumberParsedFormat.NoCountryCodeDashes:
                return "No Country Code with Dashes";
            case PhoneNumberParsedFormat.NoCountryCodeNoDigitsSeparators:
                return "No Country Code without Digits Separators";
            case PhoneNumberParsedFormat.EnglishWords:
                return "English Words";
            case PhoneNumberParsedFormat.HindiWords:
                return "Hindi Words";
            case PhoneNumberParsedFormat.CombinationEnglishHindi:
                return "Combination of English and Hindi Words";
            default:
                throw new ArgumentOutOfRangeException(nameof(format), format, null);
        }
    }
}
