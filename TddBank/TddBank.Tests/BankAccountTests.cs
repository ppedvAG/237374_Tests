namespace TddBank.Tests
{
    public class BankAccountTests
    {
        [Fact]
        public void New_account_should_have_0_as_balance()
        {
            var ba = new BankAccount();

            Assert.Equal(0, ba.Balance);
        }

        [Fact]
        public void Deposit_should_add_to_Balance()
        {
            var ba = new BankAccount();

            ba.Deposit(5m);
            ba.Deposit(7m);

            Assert.Equal(12m, ba.Balance);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Deposit_a_negative_or_zero_value_throws_ArgumentEx(decimal value)
        {
            var ba = new BankAccount();

            Assert.Throws<ArgumentException>(() => ba.Deposit(value));
        }

        [Fact]
        public void Withdraw_should_substract_from_Balance()
        {
            var ba = new BankAccount();
            ba.Deposit(10m);

            ba.Withdraw(7m);

            Assert.Equal(3m, ba.Balance);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Withdraw_a_negative_or_zero_value_throws_ArgumentEx(decimal value)
        {
            var ba = new BankAccount();

            Assert.Throws<ArgumentException>(() => ba.Withdraw(value));
        }

        [Fact]
        public void Withdraw_below_zero_throws_InvalidOpEx()
        {
            var ba = new BankAccount();
            ba.Deposit(10m);

            Assert.ThrowsAny<Exception>(() => ba.Withdraw(11m));
        }
    }
}