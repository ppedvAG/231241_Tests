
namespace TDDBank.Tests
{
    public class BankAccountTests
    {
        [Fact]
        public void A_new_account_should_have_0_as_balance()
        {
            var ba = new BankAccount();

            Assert.Equal(0, ba.Balance);
        }

        [Fact]
        public void Deposit_should_add_to_balance()
        {
            var ba = new BankAccount();

            ba.Deposit(3m);
            ba.Deposit(6m);

            Assert.Equal(9m, ba.Balance);
        }

        [Fact]
        public void Deposit_throw_ex_if_value_is_negative_or_zero()
        {
            var ba = new BankAccount();

            Assert.Throws<ArgumentException>(() => ba.Deposit(-3m));
            Assert.Throws<ArgumentException>(() => ba.Deposit(0m));
        }

        [Fact]
        public void Withdraw_should_reduce_balance()
        {
            var ba = new BankAccount();
            ba.Deposit(20m);

            ba.Withdraw(6m);

            Assert.Equal(14m, ba.Balance);
        }

        [Fact]
        public void Withdraw_throw_ex_if_value_is_negative_or_zero()
        {
            var ba = new BankAccount();

            Assert.Throws<ArgumentException>(() => ba.Withdraw(-3m));
            Assert.Throws<ArgumentException>(() => ba.Withdraw(0m));
        }

        [Fact]
        public void Withdraw_more_than_balance_throw()
        {
            var ba = new BankAccount();
            ba.Deposit(20m);

            Assert.Throws<InvalidOperationException>(() => ba.Withdraw(21m));
        }
    }
}