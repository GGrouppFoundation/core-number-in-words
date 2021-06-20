#nullable enable

using System.Collections.Generic;

namespace GGroupp.Core.Tests
{
    public sealed partial class NumberInWordsTest
    {
        private static IEnumerable<object[]> GetMaxFractMemberData()
        {
            var number = 1m;
            for (var i = 0; i < FractExpectedValues.Length; i++)
            {
                number *= 0.1m;
                yield return new object[] { number, i + 1, $"ноль целых одна {FractExpectedValues[i]}" };
            }
        }

        private static string[] FractExpectedValues
            =>
            new[]
            {
                "десятая",
                "сотая",
                "тысячная",
                "десятитысячная",
                "стотысячная",
                "миллионная",
                "десятимиллионная",
                "стомиллионная",
                "миллиардная",
                "десятимиллиардная",
                "стомиллиардная"
            };
    }
}