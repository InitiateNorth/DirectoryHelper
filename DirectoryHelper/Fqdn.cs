namespace InitiateNorth.DirectoryHelper
{
    using CSharpFunctionalExtensions;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Represents a fully qualified domain name.
    /// </summary>
    public class Fqdn
    {
        /// <summary>The regex for a valid FQDN.</summary>
        private const string RegexFqdn = @"(?=^.{1,254}$)(^(?:(?!\d+\.)[a-zA-Z0-9_\-]{1,63}\.?)+(?:[a-zA-Z]{2,})$)";

        /// <summary>The value.</summary>
        private readonly string _value;

        /// <summary>Initializes a new instance of the <see cref="Fqdn"/> class.</summary>
        /// <param name="value">The value.</param>
        private Fqdn(string value)
        {
            _value = value;
        }

        /// <summary>Creates the specified FQDN.</summary>
        /// <param name="fqdn">The FQDN.</param>
        /// <returns>A <see cref="Result"/> which represents whether the <see cref="Fqdn"/> was created.</returns>
        public static Result<Fqdn> Create(string fqdn)
        {
            if (string.IsNullOrWhiteSpace(fqdn))
            {
                return Result.Fail<Fqdn>("Empty or null FQDN");
            }

            return !Regex.IsMatch(fqdn, RegexFqdn) ? Result.Fail<Fqdn>("Invalid FQDN") : Result.Ok(new Fqdn(fqdn));
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
        /// Performs an implicit conversion from <see cref="Fqdn"/> to <see cref="System.String"/>.
        /// </summary>
        /// <param name="fqdn">The FQDN.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(Fqdn fqdn)
        {
            return fqdn._value;
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
            var fqdn = obj as Fqdn;

            return !ReferenceEquals(fqdn, null) && _value.EqualsCaseInsensitive(fqdn._value);
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