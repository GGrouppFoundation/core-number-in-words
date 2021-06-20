#nullable enable

using Xunit;
using static PrimeFuncPack.UnitTest.TestData;

namespace GGroupp.Core.Tests
{
    partial class RusWordTest
    {
        [Theory]
        [InlineData(EmptyString, EmptyString, EmptyString, RusWordGender.Masculine)]
        [InlineData(null, null, null, RusWordGender.Neuter)]
        [InlineData("рубль", "рубля", "Рублей", RusWordGender.Masculine)]
        [InlineData(EmptyString, null, "Рублей", RusWordGender.Feminine)]
        [InlineData("рубль", "рубля", null, RusWordGender.Masculine)]
        [InlineData("рубль", null, "Рублей", RusWordGender.Neuter)]
        [InlineData("рубль", EmptyString, "Рублей", RusWordGender.Masculine)]
        public void New_AllFormsAreCorrect(string nominative, string genitiveSingular, string genitivePlural, RusWordGender gender)
        {
            var rusWord = new RusWord(
                nominative: nominative,
                genitiveSingular: genitiveSingular,
                genitivePlural: genitivePlural,
                gender: gender);

            Assert.Equal(nominative ?? string.Empty, rusWord.Nominative);
            Assert.Equal(genitiveSingular ?? string.Empty, rusWord.GenitiveSingular);
            Assert.Equal(genitivePlural ?? string.Empty, rusWord.GenitivePlural);
            Assert.Equal(gender, rusWord.Gender);
        }
    }
}