#nullable enable

namespace GGroupp
{
    public static class NumberInWords
    {
        public static string GetRussian(
            decimal number,
            byte decimals,
            RusWord? integerWord = default,
            RusWord? fractionalWord = default,
            bool isFractionalRequired = false)
            =>
            NumberInWordsRus.BuildRusText(number, decimals, integerWord, fractionalWord, isFractionalRequired);
    }
}