#nullable enable

using System;
using Xunit;
using static PrimeFuncPack.UnitTest.TestData;

namespace GGroupp.Core.Tests
{
    partial class NumberInWordsTest
    {
        [Theory]
        [InlineData(0, 0, "ноль")]
        [InlineData(0, 1, "ноль")]
        [InlineData(0.0000021, 7, "ноль целых двадцать одна десятимиллионная")]
        [InlineData(0.0000021, 8, "ноль целых двести десять стомиллионных")]
        [InlineData(9223372036854775807, 0, "девять квинтиллионов двести двадцать три квадриллиона триста семьдесят два триллиона тридцать шесть миллиардов восемьсот пятьдесят четыре миллиона семьсот семьдесят пять тысяч восемьсот семь")]
        [InlineData(18446744073709551615, 0, "восемнадцать квинтиллионов четыреста сорок шесть квадриллионов семьсот сорок четыре триллиона семьдесят три миллиарда семьсот девять миллионов пятьсот пятьдесят одна тысяча шестьсот пятнадцать")]
        [InlineData(1.0002, 4, "одна целая две десятитысячные")]
        [InlineData(9000000001, 0, "девять миллиардов один")]
        [InlineData(7654003, 0, "семь миллионов шестьсот пятьдесят четыре тысячи три")]
        [InlineData(17890001011, 0, "семнадцать миллиардов восемьсот девяносто миллионов одна тысяча одиннадцать")]
        [InlineData(15.03, 2, "пятнадцать целых три сотые")]
        [InlineData(1.01, 2, "одна целая одна сотая")]
        [InlineData(890167000002.01, 0, "восемьсот девяносто миллиардов сто шестьдесят семь миллионов два")]
        [InlineData(1000000010, 0, "один миллиард десять")]
        [InlineData(167008007, 0, "сто шестьдесят семь миллионов восемь тысяч семь")]
        [InlineData(1000000, 0, "один миллион")]
        [InlineData(1000, 0, "одна тысяча")]
        [InlineData(2007.890689108, 9, "две тысячи семь целых восемьсот девяносто миллионов шестьсот восемьдесят девять тысяч сто восемь миллиардных")]
        [InlineData(5101, 0, "пять тысяч сто один")]
        [InlineData(-0.07, 2, "минус ноль целых семь сотых")]
        [InlineData(-17.17, 1, "минус семнадцать целых две десятые")]
        [InlineData(1000000007.102, 3, "один миллиард семь целых сто две тысячные")]
        [InlineData(-1000080017.010, 2, "минус один миллиард восемьдесят тысяч семнадцать целых одна сотая")]
        [InlineData(1000800813, 0, "один миллиард восемьсот тысяч восемьсот тринадцать")]
        [InlineData(60001501000002, 0, "шестьдесят триллионов один миллиард пятьсот один миллион два")]
        [InlineData(3000030407, 0, "три миллиарда тридцать тысяч четыреста семь")]
        [InlineData(1600000009, 0, "один миллиард шестьсот миллионов девять")]
        [InlineData(1008000515, 0, "один миллиард восемь миллионов пятьсот пятнадцать")]
        [InlineData(-300, 3, "минус триста")]
        public void GetRussian_DimensionIsDefault_ExpectCorrectText(decimal value, byte decimals, string expected)
        {
            var actual = NumberInWords.GetRussian(value, decimals);
            Assert.Equal(expected: expected, actual: actual);
        }

        [Theory]
        [MemberData(nameof(GetMaxFractMemberData))]
        public void GetRussian_MaxFractPart_ExpectCorrectText(decimal number, byte decimals, string expected)
        {
            var actual = NumberInWords.GetRussian(number, decimals);
            Assert.Equal(expected: expected, actual: actual);
        }

        [Fact]
        public void GetRussian_DecimalsAreTwelve_ExpectArgumentOutOfRangeException()
        {
            const byte decimals = 12;
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _ = NumberInWords.GetRussian(0, decimals));

            Assert.Equal("decimals", ex.ParamName);
        }

        [Fact]
        public void GetRussian_DecimalMaxValue_DimensionIsDefault_ExpectCorrectText()
        {
            var actual = NumberInWords.GetRussian(decimal.MaxValue, 0);
            const string expected = "семьдесят девять октиллионов двести двадцать восемь септиллионов сто шестьдесят два секстиллиона пятьсот четырнадцать квинтиллионов двести шестьдесят четыре квадриллиона триста тридцать семь триллионов пятьсот девяносто три миллиарда пятьсот сорок три миллиона девятьсот пятьдесят тысяч триста тридцать пять";

            Assert.Equal(expected: expected, actual: actual);
        }

        [Fact]
        public void GetRussian_DecimalMinValue_DimensionIsDefault_ExpectCorrectText()
        {
            var actual = NumberInWords.GetRussian(decimal.MinValue, 0);
            const string expected = "минус семьдесят девять октиллионов двести двадцать восемь септиллионов сто шестьдесят два секстиллиона пятьсот четырнадцать квинтиллионов двести шестьдесят четыре квадриллиона триста тридцать семь триллионов пятьсот девяносто три миллиарда пятьсот сорок три миллиона девятьсот пятьдесят тысяч триста тридцать пять";

            Assert.Equal(expected: expected, actual: actual);
        }

        [Theory]
        [InlineData(0, "ноль рублей ноль копеек")]
        [InlineData(0.1, "ноль рублей десять копеек")]
        [InlineData(-0.05, "минус ноль рублей пять копеек")]
        [InlineData(1, "один рубль ноль копеек")]
        [InlineData(2, "два рубля ноль копеек")]
        [InlineData(3, "три рубля ноль копеек")]
        [InlineData(4, "четыре рубля ноль копеек")]
        [InlineData(-5.02, "минус пять рублей две копейки")]
        [InlineData(6, "шесть рублей ноль копеек")]
        [InlineData(7, "семь рублей ноль копеек")]
        [InlineData(8, "восемь рублей ноль копеек")]
        [InlineData(9, "девять рублей ноль копеек")]
        [InlineData(-10.125, "минус десять рублей тринадцать копеек")]
        [InlineData(11.7, "одиннадцать рублей семьдесят копеек")]
        [InlineData(12, "двенадцать рублей ноль копеек")]
        [InlineData(13.999, "четырнадцать рублей ноль копеек")]
        [InlineData(13, "тринадцать рублей ноль копеек")]
        [InlineData(14, "четырнадцать рублей ноль копеек")]
        [InlineData(-15.917, "минус пятнадцать рублей девяносто две копейки")]
        [InlineData(16, "шестнадцать рублей ноль копеек")]
        [InlineData(17, "семнадцать рублей ноль копеек")]
        [InlineData(18, "восемнадцать рублей ноль копеек")]
        [InlineData(19, "девятнадцать рублей ноль копеек")]
        [InlineData(20, "двадцать рублей ноль копеек")]
        [InlineData(40, "сорок рублей ноль копеек")]
        [InlineData(51, "пятьдесят один рубль ноль копеек")]
        [InlineData(92, "девяносто два рубля ноль копеек")]
        [InlineData(100, "сто рублей ноль копеек")]
        [InlineData(101, "сто один рубль ноль копеек")]
        [InlineData(-102, "минус сто два рубля ноль копеек")]
        [InlineData(1000, "одна тысяча рублей ноль копеек")]
        [InlineData(2001, "две тысячи один рубль ноль копеек")]
        [InlineData(1000000, "один миллион рублей ноль копеек")]
        [InlineData(18446744073709551615, "восемнадцать квинтиллионов четыреста сорок шесть квадриллионов семьсот сорок четыре триллиона семьдесят три миллиарда семьсот девять миллионов пятьсот пятьдесят одна тысяча шестьсот пятнадцать рублей ноль копеек")]
        public void GetRussian_DimensionIsNotDefaultAndFractionalIsRequired_ExpectCorrectText(decimal value, string expected)
        {
            var dimensionWord = new RusWord("рубль", "рубля", "рублей", RusWordGender.Masculine);
            var fractWord = new RusWord("копейка", "копейки", "копеек", RusWordGender.Feminine);

            var actual = NumberInWords.GetRussian(value, 2, dimensionWord, fractWord, true);
            Assert.Equal(expected: expected, actual: actual);
        }

        [Theory]
        [InlineData(0, "миля", "мили", "миль", "ноль миль")]
        [InlineData(1, "миля", "мили", "миль", "одна миля")]
        [InlineData(2, "миля", "мили", "миль", "две мили")]
        [InlineData(3, "миля", "мили", "миль", "три мили")]
        [InlineData(4, "миля", "мили", "миль", "четыре мили")]
        [InlineData(5, "миля", "мили", "миль", "пять миль")]
        [InlineData(6, "миля", "мили", "миль", "шесть миль")]
        [InlineData(7, "миля", "мили", "миль", "семь миль")]
        [InlineData(8, "миля", "мили", "миль", "восемь миль")]
        [InlineData(9, "миля", "мили", "миль", "девять миль")]
        [InlineData(10, "миля", "мили", "миль", "десять миль")]
        [InlineData(11, "миля", "мили", "миль", "одиннадцать миль")]
        [InlineData(12, "миля", "мили", "миль", "двенадцать миль")]
        [InlineData(13, "миля", "мили", "миль", "тринадцать миль")]
        [InlineData(13, "миля", "мили", EmptyString, "тринадцать")]
        [InlineData(14, "миля", "мили", "миль", "четырнадцать миль")]
        [InlineData(15, "миля", "мили", "миль", "пятнадцать миль")]
        [InlineData(16, "миля", "мили", "миль", "шестнадцать миль")]
        [InlineData(17, "миля", "мили", "миль", "семнадцать миль")]
        [InlineData(18, "миля", "мили", "миль", "восемнадцать миль")]
        [InlineData(19, "миля", "мили", "миль", "девятнадцать миль")]
        [InlineData(20, "миля", "мили", "миль", "двадцать миль")]
        [InlineData(40, "миля", "мили", "миль", "сорок миль")]
        [InlineData(51, "миля", "мили", "миль", "пятьдесят одна миля")]
        [InlineData(51, null, "мили", "миль", "пятьдесят одна")]
        [InlineData(92, "миля", "мили", "миль", "девяносто две мили")]
        [InlineData(92, "миля", EmptyString, "миль", "девяносто две")]
        [InlineData(100, "миля", "мили", "миль", "сто миль")]
        [InlineData(100, null, "мили", null, "сто")]
        [InlineData(101, "миля", "мили", "миль", "сто одна миля")]
        [InlineData(101, EmptyString, null, null, "сто одна")]
        [InlineData(102, "миля", "мили", "миль", "сто две мили")]
        [InlineData(1000, "миля", "мили", "миль", "одна тысяча миль")]
        [InlineData(2001, "миля", "мили", "миль", "две тысячи одна миля")]
        [InlineData(1000000, "миля", "мили", "миль", "один миллион миль")]
        [InlineData(18446744073709551615, "миля", "мили", "миль", "восемнадцать квинтиллионов четыреста сорок шесть квадриллионов семьсот сорок четыре триллиона семьдесят три миллиарда семьсот девять миллионов пятьсот пятьдесят одна тысяча шестьсот пятнадцать миль")]
        public void GetRussian_DimensionIsFeminine_ExpectCorrectText(
            decimal value, string nominative, string genitiveSingular, string genitivePlural, string expectedText)
        {
            var dimensionWord = new RusWord(
                nominative: nominative,
                genitiveSingular: genitiveSingular,
                genitivePlural: genitivePlural,
                gender: RusWordGender.Feminine);

            var actual = NumberInWords.GetRussian(value, 0, dimensionWord);
            Assert.Equal(expected: expectedText, actual: actual);
        }

        [Theory]
        [InlineData(0, "очко", "очка", "очков", "ноль очков")]
        [InlineData(1, "очко", "очка", "очков", "одно очко")]
        [InlineData(2, "очко", "очка", "очков", "два очка")]
        [InlineData(3, "очко", "очка", "очков", "три очка")]
        [InlineData(4, "очко", "очка", "очков", "четыре очка")]
        [InlineData(5, "очко", "очка", "очков", "пять очков")]
        [InlineData(6, "очко", "очка", "очков", "шесть очков")]
        [InlineData(7, "очко", "очка", "очков", "семь очков")]
        [InlineData(8, "очко", "очка", "очков", "восемь очков")]
        [InlineData(9, "очко", "очка", "очков", "девять очков")]
        [InlineData(10, "очко", "очка", "очков", "десять очков")]
        [InlineData(11, "очко", "очка", "очков", "одиннадцать очков")]
        [InlineData(12, "очко", "очка", "очков", "двенадцать очков")]
        [InlineData(13, "очко", "очка", "очков", "тринадцать очков")]
        [InlineData(13, "очко", "очка", EmptyString, "тринадцать")]
        [InlineData(14, "очко", "очка", "очков", "четырнадцать очков")]
        [InlineData(15, "очко", "очка", "очков", "пятнадцать очков")]
        [InlineData(16, "очко", "очка", "очков", "шестнадцать очков")]
        [InlineData(17, "очко", "очка", "очков", "семнадцать очков")]
        [InlineData(18, "очко", "очка", "очков", "восемнадцать очков")]
        [InlineData(19, "очко", "очка", "очков", "девятнадцать очков")]
        [InlineData(20, "очко", "очка", "очков", "двадцать очков")]
        [InlineData(40, "очко", "очка", "очков", "сорок очков")]
        [InlineData(51, "очко", "очка", "очков", "пятьдесят одно очко")]
        [InlineData(51, null, "очка", "очков", "пятьдесят одно")]
        [InlineData(92, "очко", "очка", "очков", "девяносто два очка")]
        [InlineData(92, "очко", EmptyString, "очков", "девяносто два")]
        [InlineData(100, "очко", "очка", "очков", "сто очков")]
        [InlineData(100, null, "очка", null, "сто")]
        [InlineData(101, "очко", "очка", "очков", "сто одно очко")]
        [InlineData(101, EmptyString, null, null, "сто одно")]
        [InlineData(102, "очко", "очка", "очков", "сто два очка")]
        [InlineData(1000, "очко", "очка", "очков", "одна тысяча очков")]
        [InlineData(2001, "очко", "очка", "очков", "две тысячи одно очко")]
        [InlineData(1000000, "очко", "очка", "очков", "один миллион очков")]
        [InlineData(18446744073709551615, "очко", "очка", "очков", "восемнадцать квинтиллионов четыреста сорок шесть квадриллионов семьсот сорок четыре триллиона семьдесят три миллиарда семьсот девять миллионов пятьсот пятьдесят одна тысяча шестьсот пятнадцать очков")]
        public void GetRussian_DimensionIsNeuter_ExpectCorrectText(
            decimal value, string nominative, string genitiveSingular, string genitivePlural, string expectedText)
        {
            var dimensionWord = new RusWord(
                nominative: nominative,
                genitiveSingular: genitiveSingular,
                genitivePlural: genitivePlural,
                gender: RusWordGender.Neuter);

            var actual = NumberInWords.GetRussian(value, 0, dimensionWord);
            Assert.Equal(expected: expectedText, actual: actual);
        }
    }
}