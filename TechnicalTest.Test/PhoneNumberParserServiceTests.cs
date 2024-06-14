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
        public void ParseWhiteSpaceSeparatedNumbers()
        {
            // Arrange
            var inputText = "Hello, my phone number is +1 123 456 7890.";

            // Act
            var phoneNumbers = _phoneNumberParserService.ParsePhoneNumbers(inputText);

            // Assert
            Assert.Equal(4, phoneNumbers.Count);
            Assert.Equal("1", phoneNumbers[0].RawNumber);
            Assert.Equal("123", phoneNumbers[1].RawNumber);
            Assert.Equal("456", phoneNumbers[2].RawNumber);
            Assert.Equal("7890", phoneNumbers[3].RawNumber);
            Assert.Equal(PhoneNumberParsedFormat.Numbers, phoneNumbers[0].Format);
        }

        [Fact]
        public void ParseDashSeparatedNumbers()
        {
            // Arrange
            var inputText = "Hello, my phone number is +1-123-456-7890.";

            // Act
            var phoneNumbers = _phoneNumberParserService.ParsePhoneNumbers(inputText);

            // Assert
            Assert.Single(phoneNumbers);
            Assert.Equal("1-123-456-7890", phoneNumbers[0].RawNumber);
            Assert.Equal(PhoneNumberParsedFormat.Numbers, phoneNumbers[0].Format);
            Assert.Equal(SeparatorType.Dash, phoneNumbers[0].Separator);
        }

        [Fact]
        public void ParseNumbersWithParantheses()
        {
            // Arrange
            var inputText = "Hello, my phone number is (123) 456 7890.";

            // Act
            var phoneNumbers = _phoneNumberParserService.ParsePhoneNumbers(inputText);

            // Assert
            Assert.Equal(3, phoneNumbers.Count);
            Assert.Equal("123", phoneNumbers[0].RawNumber);
            Assert.Equal("456", phoneNumbers[1].RawNumber);
            Assert.Equal("7890", phoneNumbers[2].RawNumber);
            Assert.Equal(PhoneNumberParsedFormat.Numbers, phoneNumbers[0].Format);
        }

        [Fact]
        public void ParseNumbersWithEnglishWords()
        {
            // Arrange
            var inputText = "Hello, my phone number is ONE TWO THREE FOUR FIVE SIX SEVEN EIGHT NINE ZERO.";

            // Act
            var phoneNumbers = _phoneNumberParserService.ParsePhoneNumbers(inputText);

            // Assert
            Assert.Single(phoneNumbers);
            Assert.Equal("ONE TWO THREE FOUR FIVE SIX SEVEN EIGHT NINE ZERO", phoneNumbers[0].RawNumber);
            Assert.Equal(PhoneNumberParsedFormat.EnglishWords, phoneNumbers[0].Format);
            Assert.Equal(SeparatorType.WhiteSpace, phoneNumbers[0].Separator);
        }

        [Fact]
        public void ParseNumbersWithHindiWords()
        {
            // Arrange
            var inputText = "Hello, my phone numbers एक दो तीन चार पांच छह सात आठ नौ शून्य.";

            // Act
            var phoneNumbers = _phoneNumberParserService.ParsePhoneNumbers(inputText);

            // Assert
            Assert.Single(phoneNumbers);
            Assert.Equal("एक दो तीन चार पांच छह सात आठ नौ शून्य", phoneNumbers[0].RawNumber);
            Assert.Equal(PhoneNumberParsedFormat.HindiWords, phoneNumbers[0].Format);
            Assert.Equal(SeparatorType.WhiteSpace, phoneNumbers[0].Separator);
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