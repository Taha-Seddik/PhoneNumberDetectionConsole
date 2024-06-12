
namespace TechnicalTest.Utils;

public static class LanguageUtils
{
    private static readonly Dictionary<string, string> englishToDigits = new Dictionary<string, string>
    {
        {"ONE", "1"},
        {"TWO", "2"},
        {"THREE", "3"},
        {"FOUR", "4"},
        {"FIVE", "5"},
        {"SIX", "6"},
        {"SEVEN", "7"},
        {"EIGHT", "8"},
        {"NINE", "9"},
        {"ZERO", "0"}
    };

    private static readonly Dictionary<string, string> hindiToDigits = new Dictionary<string, string>
    {
        {"एक", "1"},
        {"दो", "2"},
        {"तीन", "3"},
        {"चार", "4"},
        {"पांच", "5"},
        {"छह", "6"},
        {"सात", "7"},
        {"आठ", "8"},
        {"नौ", "9"},
        {"शून्य", "0"}
    };

    public static bool ContainsOnlyEnglish(string input)
    {
        // Split the input string into individual words
        string[] words = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        // Check if every word is an English word
        foreach (string word in words)
        {
            if (!englishToDigits.Keys.Contains(word.ToUpper()))
            {
                return false;
            }
        }

        return true;
    }

    public static bool ContainsOnlyHindi(string input)
    {
        // Split the input string into individual words
        string[] words = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        // Check if every word is a Hindi word
        foreach (string word in words)
        {
            if (!hindiToDigits.Keys.Contains(word))
            {
                return false;
            }
        }

        return true;
    }

    public static string WordsToDigits(string input)
    {
        // Split the input string into individual words
        string[] words = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        // Initialize the result
        string result = "";

        // Convert each word to its corresponding digit
        foreach (string word in words)
        {
            if (englishToDigits.ContainsKey(word.ToUpper()))
            {
                result += englishToDigits[word.ToUpper()];
            }
            else if (hindiToDigits.ContainsKey(word))
            {
                result += hindiToDigits[word];
            }
            else
            {
                throw new ArgumentException($"Invalid word: {word}");
            }
        }

        return result;
    }
}