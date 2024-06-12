using System.Text.RegularExpressions;

namespace TechnicalTest.Utils;

public static class PhoneNumberDigitsUtils
{
    public static string GetDigits(string phoneNumber)
    {
        // Use regex to remove non-digit characters from the phone number
        string digitsOnly = Regex.Replace(phoneNumber, @"\D", "");

        // If the number of digits is greater than 10, take only the last 10 digits
        if (digitsOnly.Length > 10)
        {
            digitsOnly = digitsOnly.Substring(digitsOnly.Length - 10);
        }

        return digitsOnly;
    }

    public static string ExtractDigitsAfterClosingParenthesis(string phoneNumberString)
    {
        // Find the index of the closing parenthesis
        int closingParenthesisIndex = phoneNumberString.IndexOf(')');

        // Check if closing parenthesis exists in the string
        if (closingParenthesisIndex != -1 && closingParenthesisIndex < phoneNumberString.Length - 1)
        {
            // Extract the substring after the closing parenthesis
            string digitsAfterClosingParenthesis = phoneNumberString.Substring(closingParenthesisIndex + 1);
            // Remove any non-digit characters from the extracted substring
            digitsAfterClosingParenthesis = new string(digitsAfterClosingParenthesis.Where(char.IsDigit).ToArray());
            return digitsAfterClosingParenthesis;
        }
        else
        {
            // Return empty string if closing parenthesis is not found or there are no digits after it
            return string.Empty;
        }
    }


}