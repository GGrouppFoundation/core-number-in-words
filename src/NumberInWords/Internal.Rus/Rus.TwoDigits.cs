#nullable enable

using System.Text;

namespace GGroupp
{
    partial class NumberInWordsRus
    {
        private const ushort Twenty = 20;

        private const string ZeroText = "ноль";

        private static readonly string[] Digits =
        {
            "один", "два", "три", "четыре", "пять", "шесть", "семь", "восемь", "девять"
        };

        private static readonly string[] FemDigits =
        {
            "одна", "две"
        };

        private static readonly string[] NeuterDigits =
        {
            "одно"
        };

        private static readonly string[] Elevens =
        {
            "одиннадцать", "двенадцать", "тринадцать", "четырнадцать", "пятнадцать", "шестнадцать", "семнадцать", "восемнадцать", "девятнадцать"
        };

        private static readonly string[] Tens =
        {
            "десять", "двадцать", "тридцать", "сорок", "пятьдесят", "шестьдесят", "семьдесят", "восемьдесят", "девяносто"
        };

        private static StringBuilder AppendTwoDigits(
            this StringBuilder textBuilder, ushort twoDigits, RusWordGender wordGender, bool isEnd)
        {
            if (twoDigits == 0)
            {
                if (isEnd && textBuilder.Length == 0)
                {
                    textBuilder.Append(ZeroText);
                }

                return textBuilder;
            }

            if ((twoDigits > Ten) && (twoDigits < Twenty))
            {
                return textBuilder.AppendWithSpace(Elevens[twoDigits - Ten - 1]);
            }

            var highDigit = twoDigits / Ten;
            if (highDigit > 0)
            {
                textBuilder.AppendWithSpace(Tens[highDigit - 1]);
            }

            var lowDigit = twoDigits % Ten;
            if (lowDigit > 0)
            {
                textBuilder.AppendWithSpace(GetTextDigit(lowDigit, wordGender));
            }

            return textBuilder;
        }

        private static string GetDimensionText(ushort twoDigits, RusWord dimensionWord)
        {
            if ((twoDigits < Ten) || (twoDigits >= Twenty))
            {
                var lowDigit = twoDigits % Ten;
                if ((lowDigit >= 2) && (lowDigit <= 4))
                {
                    return dimensionWord.GenitiveSingular;
                }

                if (lowDigit == 1)
                {
                    return dimensionWord.Nominative;
                }
            }

            return dimensionWord.GenitivePlural;
        }

        private static string GetTextDigit(int digit, RusWordGender gender)
            =>
            gender switch
            {
                RusWordGender.Feminine when digit <= FemDigits.Length  => FemDigits[digit - 1],
                RusWordGender.Neuter when digit <= NeuterDigits.Length => NeuterDigits[digit - 1],
                _ => Digits[digit - 1]
            };
    }
}