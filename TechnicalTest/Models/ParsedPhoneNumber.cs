using TechnicalTest.Extensions;

namespace TechnicalTest.Models;

public class ParsedPhoneNumber
{
    public string RawNumber { get; set; }
    public PhoneNumberParsedFormat Format { get; set; }
    public SeparatorType Separator { get; set; }

    public ParsedPhoneNumber(string rawPhoneNumber, PhoneNumberParsedFormat format, SeparatorType separator)
    {
        RawNumber = rawPhoneNumber;
        Format = format;
        Separator = separator;
    }

    public override string ToString()
    {
        return $"Found Number: {RawNumber.Trim()}  -  ({Format.ToLabel()}) - Separator: {Separator}";
    }
}
