using Proposal.Domain.ProposalAggregate.ValueObjects;
using Proposal.Domain.SeedWork.Exceptions;

namespace TestProject1._3._Domain_Layer_Tests.ValueObjects
{
    public class MoneyTest
    {
        [Fact]
        public void Ctor_WithValidValue_UsesDefaultCurrencyBRL()
        {
            var money = new Money(123.45m);

            Assert.Equal(123.45m, money.Value);
            Assert.Equal("BRL", money.Currency);
        }

        [Fact]
        public void Ctor_NormalizesCurrencyToUpper()
        {
            var money = new Money(10m, "usd");
            Assert.Equal("USD", money.Currency);
        }

        [Fact]
        public void Ctor_NegativeValue_ThrowsBusinessRulesException()
        {
            var ex = Assert.Throws<BusinessRulesException>(() => new Money(-0.01m, "BRL"));
            Assert.Contains("greater than or equal to 0", ex.Message);
        }

        [Fact]
        public void Ctor_InvalidCurrency_ThrowsBusinessRulesException()
        {
            var ex = Assert.Throws<BusinessRulesException>(() => new Money(10m, "ABC"));
            Assert.Contains("Invalid currency", ex.Message);
        }

        [Theory]
        [InlineData("USD", true)]
        [InlineData("eur", true)]
        [InlineData("BRL", true)]
        [InlineData("JPY", true)]
        [InlineData("GBP", true)]
        [InlineData("abc", false)]
        public void IsValidCurrency_Works(string code, bool expected)
        {
            Assert.Equal(expected, Money.IsValidCurrency(code));
        }
    }
}
