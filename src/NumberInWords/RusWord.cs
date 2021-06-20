#nullable enable

using System.Diagnostics.CodeAnalysis;

namespace GGroupp
{
    public sealed record RusWord
    {
        public RusWord(
            [AllowNull] string nominative,
            [AllowNull] string genitiveSingular,
            [AllowNull] string genitivePlural,
            RusWordGender gender)
        {
            Nominative = nominative ?? string.Empty;
            GenitiveSingular = genitiveSingular ?? string.Empty;
            GenitivePlural = genitivePlural ?? string.Empty;
            Gender = gender;
        }

        public string Nominative { get; }

        public string GenitiveSingular { get; }

        public string GenitivePlural { get; }

        public RusWordGender Gender { get; }
    }
}