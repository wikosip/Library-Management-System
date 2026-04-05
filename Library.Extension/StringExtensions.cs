namespace Library.Extension;

using System;
using System.Collections.Generic;

public static class StringExtensions
{
    private static readonly Dictionary<string, string> IrregularMap =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "Person", "People" },
            { "Man", "Men" },
            { "Woman", "Women" },
            { "Child", "Children" },
            { "Mouse", "Mice" },
            { "Foot", "Feet" },
            { "Tooth", "Teeth" }
        };

    private static readonly HashSet<string> UncountableWords =
        new(StringComparer.OrdinalIgnoreCase)
        {
            "Fish",
            "Sheep",
            "Deer",
            "Species",
            "Aircraft",
            "Series",
            "Information",
            "News"
        };

    public static string ToPluralize(this string? word)
    {
        if (string.IsNullOrWhiteSpace(word))
            return word ?? string.Empty;

        word = word.Trim();

        if (UncountableWords.Contains(word))
            return word;

        if (IrregularMap.TryGetValue(word, out var irregular))
            return irregular;

        if (word.Length == 1)
            return word + "s";

        if (word.EndsWith("y", StringComparison.OrdinalIgnoreCase) &&
            !IsVowel(word[^2]))
        {
            return word[..^1] + "ies";
        }

        string lowerWord = word.ToLowerInvariant();
        if (lowerWord.EndsWith("s") || lowerWord.EndsWith("x") || lowerWord.EndsWith("z") ||
            lowerWord.EndsWith("ch") || lowerWord.EndsWith("sh"))
        {
            return word + "es";
        }

        if (word.EndsWith("o", StringComparison.OrdinalIgnoreCase) &&
            !IsVowel(word[^2]))
        {
            return word + "es";
        }

        return word + "s";
    }

    private static bool IsVowel(char c) =>
        "aeiouAEIOU".IndexOf(c) >= 0;

}