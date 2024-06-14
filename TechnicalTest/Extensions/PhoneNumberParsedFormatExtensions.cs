using TechnicalTest.Models;

namespace TechnicalTest.Extensions;

public static class PhoneNumberParsedFormatExtensions
{
    public static string ToLabel(this PhoneNumberParsedFormat format)
    {
        switch (format)
        {
            case PhoneNumberParsedFormat.Numbers:
                return "Numbers";
            case PhoneNumberParsedFormat.HindiWords:
                return "Hindi Words";
            case PhoneNumberParsedFormat.EnglishWords:
                return "English Words";
            case PhoneNumberParsedFormat.EnglishAndHindiWords:
                return "English and Hindi Words";
            default:
                throw new ArgumentOutOfRangeException(nameof(format), format, null);
        }
    }
}
