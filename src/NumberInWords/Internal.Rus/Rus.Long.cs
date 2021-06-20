#nullable enable

using System.Collections.Generic;
using System.Text;

namespace GGroupp
{
    partial class NumberInWordsRus
    {
        private static readonly RusWord[] Words = new RusWord[]
        {
            new("тысяча", "тысячи", "тысяч", RusWordGender.Feminine),
            new("миллион", "миллиона", "миллионов", RusWordGender.Masculine),
            new("миллиард", "миллиарда", "миллиардов", RusWordGender.Masculine),
            new("триллион", "триллиона", "триллионов", RusWordGender.Masculine),
            new("квадриллион", "квадриллиона", "квадриллионов", RusWordGender.Masculine),
            new("квинтиллион", "квинтиллиона", "квинтиллионов", RusWordGender.Masculine),
            new("секстиллион", "секстиллиона", "секстиллионов", RusWordGender.Masculine),
            new("септиллион", "септиллиона", "септиллионов", RusWordGender.Masculine),
            new("октиллион", "октиллиона", "октиллионов", RusWordGender.Masculine)
        };

        private const char Space = ' ';

        private const ushort Thousand = 1000;

        private const ushort Hundred = 100;

        private const ushort Ten = 10;

        private static StringBuilder Append(
            this StringBuilder textBuilder, ulong number, IEnumerator<RusWord> dimensionsWords, bool isEnd)
        {
            var dimensionWord = dimensionsWords.MoveNext() ? dimensionsWords.Current : new(default, default, default, default);

            var threeDigitNumber = number % Thousand;
            var lostNumber = number / Thousand;

            if (lostNumber > 0)
            {
                textBuilder.Append(lostNumber, dimensionsWords, false);
            }
            return textBuilder.AppendThreeDigitsNumber((uint)threeDigitNumber, dimensionWord, isEnd);
        }

        private static StringBuilder AppendWithSpace(this StringBuilder textBuilder, string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return textBuilder;
            }

            if (textBuilder.Length > 0)
            {
                textBuilder.Append(Space);
            }
            return textBuilder.Append(text);
        }
    }
}