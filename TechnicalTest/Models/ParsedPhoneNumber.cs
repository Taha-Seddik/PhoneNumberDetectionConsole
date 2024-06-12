using TechnicalTest.Extensions;

namespace TechnicalTest.Models;

public class ParsedPhoneNumber
{
    public string Digits { get; set; }
    public string RawPhoneNumber { get; set; }
    public PhoneNumberParsedFormat Format { get; set; }
    public SeparatorType Separator { get; set; }

    public ParsedPhoneNumber(string digits, string rawPhoneNumber, PhoneNumberParsedFormat format, SeparatorType separator)
    {
        Digits = digits;
        RawPhoneNumber = rawPhoneNumber;
        Format = format;
        Separator = separator;
    }

    public override string ToString()
    {
        return $"Found Phone Number: {RawPhoneNumber} - Digits: {Digits} -  ({Format.ToLabel()}) - Separator: {Separator}";
    }
}
