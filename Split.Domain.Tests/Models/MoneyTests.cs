using System;
using Split.Domain.Models;
using Xunit;

namespace Split.Domain.Tests.Models
{
    public class MoneyTests
    {
        [Fact]
        public void Equality_Works()
        {
            var m1 = Money.InEuro(10);
            var m2 = Money.InEuro(10);
            var m3 = Money.InEuro(20);

            Assert.Equal(m1, m2);
            Assert.NotEqual(m1, m3);
        }

        [Fact]
        public void NullEquality_Works()
        {
            var money = Money.InEuro(12);
            Assert.NotEqual((Money) null, money);
        }

        [Fact]
        public void AddSubtract_Works()
        {
            var m1 = (Money) 10.34;
            var m2 = (Money) 14.41;

            var actual = m1 + m2;

            Assert.Equal(24.75m, actual.Amount);
            Assert.Equal(Currency.EUR, actual.Currency);
        }

        [Theory]
        [InlineData("EUR", true)]
        [InlineData("USD", true)]
        [InlineData("foo", false)]
        public void CreateCurrency_From_String(string currencyName, bool expectValid)
        {
            Money money = null;
            Action act = () => money = new Money(100, currencyName);

            if (expectValid)
            {
                act();
                Assert.NotNull(money);
                Assert.Equal(currencyName, money.Currency.ToString());
            }
            else
                Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void Compare_Works()
        {
            var m0 = (Money) null;
            var m1 = Money.InEuro(3);
            var m2 = Money.InEuro(5);
            var m3 = Money.InEuro(-2);

            Assert.True(m0 < m1);
            Assert.True(m0 <= m1);
            Assert.True(m1 <= m2);
            Assert.False(m1 >= m2);
            Assert.True(m3 <= m0);
            Assert.True(m3 <= m2);
        }
    }
}