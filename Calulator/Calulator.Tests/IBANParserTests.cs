using FluentAssertions;

namespace Calulator.Tests
{
    public class IBANParserTests
    {
        [Fact]
        [Trait("Category", "UnitTest")]
        public void ConvertIBANToBLZUndKontonummer_throws_if_iban_is_null_or_empty()
        {
            var actNull = () => IBANParser.ConvertIBANToBLZUndKontonummer(null);
            actNull.Should().Throw<ArgumentException>();

            var actEmpty = () => IBANParser.ConvertIBANToBLZUndKontonummer("");
            actNull.Should().Throw<ArgumentException>();
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        public void ConvertIBANToBLZUndKontonummer_throws_if_iban_not_starts_with_DE()
        {
            var act = () => IBANParser.ConvertIBANToBLZUndKontonummer("AT123456");
            act.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData("DE02120300000000202051", "12030000", "0000202051")]
        [InlineData("DE02500105170137075030", "50010517", "0137075030")]
        [InlineData("DE02100500000054540402", "10050000", "0054540402")]
        [InlineData("DE02300209000106531065", "30020900", "0106531065")]
        public void ConvertIBANToBLZUndKontonummer_Tests(string iban, string expBLZ, string expKontoNr)
        {
            var result = IBANParser.ConvertIBANToBLZUndKontonummer(iban);

            result.Item1.Should().Be(expBLZ);
            result.Item2.Should().Be(expKontoNr);
        }
    }
}
