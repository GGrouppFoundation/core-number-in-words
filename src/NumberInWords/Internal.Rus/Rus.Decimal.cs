#nullable enable

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace GGroupp
{
    partial class NumberInWordsRus
    {
        private static byte MaxDecimals => (byte)DefaultFractWords.Length;

        private const string Minus = "минус";

        private static readonly RusWord DefaultIntWord = new("целая", "целых", "целых", RusWordGender.Feminine);

        private static readonly RusWord[] DefaultFractWords = new RusWord[]
        {
            new("десятая", "десятые", "десятых", RusWordGender.Feminine),
            new("сотая", "сотые", "сотых", RusWordGender.Feminine),
            new("тысячная", "тысячные", "тысячных", RusWordGender.Feminine),
            new("десятитысячная", "десятитысячные", "десятитысячных", RusWordGender.Feminine),
            new("стотысячная", "стотысячные", "стотысячных", RusWordGender.Feminine),
            new("миллионная", "миллионные", "миллионных", RusWordGender.Feminine),
            new("десятимиллионная", "десятимиллионные", "десятимиллионных", RusWordGender.Feminine),
            new("стомиллионная", "стомиллионные", "стомиллионных", RusWordGender.Feminine),
            new("миллиардная", "миллиардные", "миллиардных", RusWordGender.Feminine),
            new("десятимиллиардная", "десятимиллиардные", "десятимиллиардных", RusWordGender.Feminine),
            new("стомиллиардная", "стомиллиардные", "стомиллиардных", RusWordGender.Feminine)
        };

        public static string BuildRusText(decimal number, byte decimals, RusWord? intWord, RusWord? fractWord, bool fractRequired)
        {
            if (decimals > MaxDecimals)
            {
                throw new ArgumentOutOfRangeException(paramName: nameof(decimals), message: $"Value cannot be greater than {MaxDecimals}.");
            }

            var intTextBuilder = new StringBuilder();
            var isPositive = number >= 0;

            if (isPositive is false)
            {
                number = -number;
            }

            var parts = SplitParts(number, decimals);

            var intPart = parts.IntPart;
            var fractPart = parts.FractPart;

            var hasFractional = fractRequired || (decimals > 0) && (fractPart > 0);
            var dimensionsWords = GetIntDimensions(hasFractional, intWord);
            intTextBuilder.Append(intPart.GetEnumerator(), dimensionsWords.GetEnumerator(), true);

            if (decimals > 0)
            {
                var fractDimensionsWords = GetFractDimensions(decimals, fractWord);

                if (hasFractional)
                {
                    var fractTextBuilder = new StringBuilder().Append(fractPart, fractDimensionsWords.GetEnumerator(), true);
                    intTextBuilder.AppendWithSpace(fractTextBuilder.ToString());
                }
            }

            var positiveText = intTextBuilder.ToString();
            return isPositive ? positiveText : $"{Minus} {positiveText}";
        }

        private static StringBuilder Append(
            this StringBuilder textBuilder, IEnumerator<uint> threeDigitNumbers, IEnumerator<RusWord> dimensionsWords, bool isEnd)
        {
            var dimensionWord = dimensionsWords.MoveNext() ? dimensionsWords.Current : EmptyRusWord;
            if (threeDigitNumbers.MoveNext())
            {
                var threeDigitNumber = threeDigitNumbers.Current;
                return textBuilder.Append(threeDigitNumbers, dimensionsWords, false).AppendThreeDigitsNumber(threeDigitNumber, dimensionWord, isEnd);
            }

            return textBuilder;
        }

        private static IEnumerable<RusWord> GetIntDimensions(bool hasFractional, RusWord? intWord)
        {
            var word = intWord ?? EmptyRusWord;
            var dimensionsWords = new List<RusWord>
            {
                word == EmptyRusWord && hasFractional ? DefaultIntWord : word
            };
            dimensionsWords.AddRange(Words);

            return dimensionsWords;
        }

        private static IEnumerable<RusWord> GetFractDimensions(byte decimals, RusWord? fractWord)
        {
            var word = fractWord ?? EmptyRusWord;
            var fractDimensionsWords = new List<RusWord>
            {
                word == EmptyRusWord ? DefaultFractWords[decimals - 1] : word
            };
            fractDimensionsWords.AddRange(Words);

            return fractDimensionsWords;
        }

        private static (IReadOnlyCollection<uint> IntPart, ulong FractPart) SplitParts(decimal number, byte decimals)
        {
            var roundedNumber = number;
            if (decimals > 0)
            {
                roundedNumber = Math.Round(
                    d: number,
                    decimals: decimals,
                    mode: MidpointRounding.AwayFromZero);
            }

            var numberAsString = roundedNumber.ToString($"N{decimals}", CultureInfo.InvariantCulture);

            var fractionals = numberAsString.Split('.');
            var intPartAsString = fractionals[0];
            var fractPartAsString = string.Empty;
            if (fractionals.Length > 1)
            {
                fractPartAsString = fractionals[1];
            }

            var intParts = intPartAsString.Split(',').Select(uint.Parse).Reverse().ToArray();
            var fractPart = string.IsNullOrEmpty(fractPartAsString) ? 0 : ulong.Parse(fractPartAsString);

            return (intParts, fractPart);
        }
    }
}