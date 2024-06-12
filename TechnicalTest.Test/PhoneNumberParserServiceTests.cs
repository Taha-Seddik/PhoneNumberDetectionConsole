using TechnicalTest.Models;
using TechnicalTest.Services;

namespace TechnicalTest.Test
{
    public class PhoneNumberParserServiceTests
    {
        private readonly PhoneNumberParserService _phoneNumberParserService;

        public PhoneNumberParserServiceTests()
        {
            _phoneNumberParserService = new PhoneNumberParserService();
        }

        [Fact]
        public void ParsePhoneNumbers_CountryCodeSpaces_ReturnsParsedPhoneNumber()
        {
            // Arrange
            var inputText = "Hello, my phone number is +1 123 456 7890.";

            // Act
            var phoneNumbers = _phoneNumberParserService.ParsePhoneNumbers(inputText);

            // Assert
            Assert.Single(phoneNumbers);
            Assert.Equal("1234567890", phoneNumbers[0].Digits);
            Assert.Equal("+1 123 456 7890", phoneNumbers[0].RawPhoneNumber);
            Assert.Equal(PhoneNumberParsedFormat.CountryCodeSpaces, phoneNumbers[0].Format);
            Assert.Equal(SeparatorType.WhiteSpace, phoneNumbers[0].Separator);
        }

        [Fact]
        public void ParsePhoneNumbers_CountryCodeDashes_ReturnsParsedPhoneNumber()
        {
            // Arrange
            var inputText = "Hello, my phone number is +1-123-456-7890.";

            // Act
            var phoneNumbers = _phoneNumberParserService.ParsePhoneNumbers(inputText);

            // Assert
            Assert.Single(phoneNumbers);
            Assert.Equal("1234567890", phoneNumbers[0].Digits);
            Assert.Equal("+1-123-456-7890", phoneNumbers[0].RawPhoneNumber);
            Assert.Equal(PhoneNumberParsedFormat.CountryCodeDashes, phoneNumbers[0].Format);
            Assert.Equal(SeparatorType.Dash, phoneNumbers[0].Separator);
        }

        [Fact]
        public void ParsePhoneNumbers_ParenthesesSpaces_ReturnsParsedPhoneNumber()
        {
            // Arrange
            var inputText = "Hello, my phone number is (123) 456 7890.";

            // Act
            var phoneNumbers = _phoneNumberParserService.ParsePhoneNumbers(inputText);

            // Assert
            Assert.Single(phoneNumbers);
            Assert.Equal("4567890", phoneNumbers[0].Digits);
            Assert.Equal("(123) 456 7890", phoneNumbers[0].RawPhoneNumber);
            Assert.Equal(PhoneNumberParsedFormat.ParenthesesSpaces, phoneNumbers[0].Format);
            Assert.Equal(SeparatorType.WhiteSpace, phoneNumbers[0].Separator);
        }

        [Fact]
        public void ParsePhoneNumbers_CombinationEnglishHindi_ReturnsParsedPhoneNumber()
        {
            // Arrange
            var inputText = "Hello, my phone number is ONE TWO THREE FOUR FIVE SIX SEVEN EIGHT NINE ZERO.";

            // Act
            var phoneNumbers = _phoneNumberParserService.ParsePhoneNumbers(inputText);

            // Assert
            Assert.Single(phoneNumbers);
            Assert.Equal("1234567890", phoneNumbers[0].Digits);
            Assert.Equal("ONE TWO THREE FOUR FIVE SIX SEVEN EIGHT NINE ZERO", phoneNumbers[0].RawPhoneNumber);
            Assert.Equal(PhoneNumberParsedFormat.EnglishWords, phoneNumbers[0].Format);
            Assert.Equal(SeparatorType.WhiteSpace, phoneNumbers[0].Separator);
        }

        [Fact]
        public void ParsePhoneNumbers_MultiplePhoneNumbers_ReturnsMultipleParsedPhoneNumbers()
        {
            // Arrange
            var inputText = "Hello, my phone numbers are +1 123 456 7890 and (123) 456-7890 and ONE TWO THREE FOUR FIVE SIX SEVEN EIGHT NINE ZERO.";

            // Act
            var phoneNumbers = _phoneNumberParserService.ParsePhoneNumbers(inputText);

            // Assert
            Assert.Equal(3, phoneNumbers.Count);
            Assert.Equal("1234567890", phoneNumbers[0].Digits);
            Assert.Equal("+1 123 456 7890", phoneNumbers[0].RawPhoneNumber);
            Assert.Equal(PhoneNumberParsedFormat.CountryCodeSpaces, phoneNumbers[0].Format);
            Assert.Equal(SeparatorType.WhiteSpace, phoneNumbers[0].Separator);

            Assert.Equal("4567890", phoneNumbers[1].Digits);
            Assert.Equal("(123) 456-7890", phoneNumbers[1].RawPhoneNumber);
            Assert.Equal(PhoneNumberParsedFormat.ParenthesesDashes, phoneNumbers[1].Format);
            Assert.Equal(SeparatorType.Dash, phoneNumbers[1].Separator);

            Assert.Equal("1234567890", phoneNumbers[2].Digits);
            Assert.Equal("ONE TWO THREE FOUR FIVE SIX SEVEN EIGHT NINE ZERO", phoneNumbers[2].RawPhoneNumber);
            Assert.Equal(PhoneNumberParsedFormat.EnglishWords, phoneNumbers[2].Format);
            Assert.Equal(SeparatorType.WhiteSpace, phoneNumbers[2].Separator);
        }

        [Fact]
        public void ParsePhoneNumbers_NoPhoneNumbers_ReturnsEmptyList()
        {
            // Arrange
            var inputText = "Hello, my name is John Doe.";

            // Act
            var phoneNumbers = _phoneNumberParserService.ParsePhoneNumbers(inputText);

            // Assert
            Assert.Empty(phoneNumbers);
        }
    }
}