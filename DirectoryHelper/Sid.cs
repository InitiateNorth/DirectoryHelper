namespace InitiateNorth.DirectoryHelper
{
    using CSharpFunctionalExtensions;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Represents a security identifier (SID).
    /// </summary>
    public class Sid
    {
        /// <summary>The regex which represents a SID.</summary>
        private const string RegexSid = @"^S-1-[0-59]-\d{2}-\d{10}-\d{10}-\d{8}-[1-9]\d{3}";

        /// <summary>The value.</summary>
        private readonly string _value;

        /// <summary>Initializes a new instance of the <see cref="Sid"/> class.</summary>
        /// <param name="value">The value.</param>
        private Sid(string value)
        {
            _value = value;
        }

        /// <summary>Creates the specified SID as the <see cref="Sid"/> object.</summary>
        /// <param name="sid">The SID value.</param>
        /// <returns>A <see cref="Result"/> which represents whether the SID was created or not.</returns>
        public static Result<Sid> Create(string sid)
        {
            if (string.IsNullOrWhiteSpace(sid))
            {
                return Result.Fail<Sid>("Empty or null SID");
            }
            else if (!Regex.IsMatch(sid, RegexSid))
            {
                return Result.Fail<Sid>("Invalid SID");
            }
            else
            {
                return Result.Ok(new Sid(sid));
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
        /// Performs an implicit conversion from <see cref="Sid"/> to <see cref="System.String"/>.
        /// </summary>
        /// <param name="sid">The sid.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(Sid sid)
        {
            return sid._value;
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
            Sid sid = obj as Sid;

            if (ReferenceEquals(sid, null))
            {
                return false;
            }

            return _value == sid._value;
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