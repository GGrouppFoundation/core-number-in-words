#nullable enable

using System.Text;

namespace GGroupp
{
    partial class NumberInWordsRus
    {
        private static readonly string[] Hundreds =
        {
            "сто", "двести", "триста", "четыреста", "пятьсот", "шестьсот", "семьсот", "восемьсот", "девятьсот"
        };

        private static StringBuilder AppendThreeDigitsNumber(
            this StringBuilder textBuilder, uint threeDigitsNumber, RusWord dimensionWord, bool isEnd)
        {
            if ((threeDigitsNumber == 0) && !isEnd)
            {
                return textBuilder;
            }

            var hundredDigit = threeDigitsNumber / Hundred;
            if (hundredDigit > 0)
            {
                textBuilder.AppendWithSpace(Hundreds[hundredDigit - 1]);
            }

            var twoDigitNumber = (ushort)(threeDigitsNumber % Hundred);
            var dimensionText = GetDimensionText(twoDigitNumber, dimensionWord);

            return textBuilder.AppendTwoDigits(twoDigitNumber, dimensionWord.Gender, isEnd).AppendWithSpace(dimensionText);
        }
    }
}