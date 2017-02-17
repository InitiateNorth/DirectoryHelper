namespace DirectoryHelper
{
    using CSharpFunctionalExtensions;
    using System.Text.RegularExpressions;

    public class Fqdn
    {
        private const string RegexFqdn = @"(?=^.{1,254}$)(^(?:(?!\d+\.)[a-zA-Z0-9_\-]{1,63}\.?)+(?:[a-zA-Z]{2,})$)";

        private readonly string _value;

        private Fqdn(string value)
        {
            _value = value;
        }

        public static Result<Fqdn> Create(string fqdn)
        {
            if (string.IsNullOrWhiteSpace(fqdn))
            {
                return Result.Fail<Fqdn>("Empty or null FQDN");
            }
            else if (!Regex.IsMatch(fqdn, RegexFqdn))
            {
                return Result.Fail<Fqdn>("Invalid FQDN");
            }
            else
            {
                return Result.Ok(new Fqdn(fqdn));
            }
        }

        public override string ToString()
        {
            return _value;
        }

        public static implicit operator string(Fqdn fqdn)
        {
            return fqdn._value;
        }

        public override bool Equals(object obj)
        {
            Fqdn fqdn = obj as Fqdn;

            if (ReferenceEquals(fqdn, null))
            {
                return false;
            }

            return _value == fqdn._value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}