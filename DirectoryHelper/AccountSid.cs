namespace InitiateNorth.DirectoryHelper
{
    using System;
    using System.Security.Principal;
    using CSharpFunctionalExtensions;

    /// <summary>
    /// Represents a security identifier (SID) for an account.
    /// </summary>
    public class AccountSid
    {
        /// <summary>The value.</summary>
        private readonly string _value;

        /// <summary>Initializes a new instance of the <see cref="AccountSid"/> class.</summary>
        /// <param name="value">The value.</param>
        private AccountSid(string value)
        {
            _value = value;
        }

        /// <summary>Creates the specified SID as the <see cref="AccountSid"/> object.</summary>
        /// <param name="sid">The SID value.</param>
        /// <returns>A <see cref="Result"/> which represents whether the SID was created or not.</returns>
        public static Result<AccountSid> Create(string sid)
        {
            if (string.IsNullOrWhiteSpace(sid))
            {
                return Result.Fail<AccountSid>("Empty or null SID");
            }

            try
            {
                var s = new SecurityIdentifier(sid);
                return s.IsAccountSid() ? 
                    Result.Ok(new AccountSid(sid)) :
                    Result.Fail<AccountSid>("Not an Account SID, perhaps a Well-known SID");
            }
            catch (Exception ex)
            {
                return Result.Fail<AccountSid>($"Invalid SID. {ex.Message}");
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return _value;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="AccountSid"/> to <see cref="System.String"/>.
        /// </summary>
        /// <param name="accountSid">The accountSid.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(AccountSid accountSid)
        {
            return accountSid._value;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            AccountSid accountSid = obj as AccountSid;

            if (ReferenceEquals(accountSid, null))
            {
                return false;
            }

            return _value == accountSid._value;
        }

        /// <summary>Returns a hash code for this instance.</summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}