using System;

namespace DataAccess.Exceptions
{
    public class AccountLockedException : Exception
    {
        private readonly string _accountNumber;
        public AccountLockedException(string accountNumber)
        {
            _accountNumber = accountNumber;
        }
        public override string Message => 
            $"The account is locked: {_accountNumber}.";
    }
}
