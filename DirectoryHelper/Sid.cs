namespace DirectoryHelper
{
    using CSharpFunctionalExtensions;
    using System.Text.RegularExpressions;

    public class Sid
    {
        private const string RegexSid = @"^S-1-[0-59]-\d{2}-\d{10}-\d{10}-\d{8}-[1-9]\d{3}";

        private readonly string _value;

        private Sid(string value)
        {
            _value = value;
        }

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

        public override string ToString()
        {
            return _value;
        }

        public static implicit operator string(Sid sid)
        {
            return sid._value;
        }

        public override bool Equals(object obj)
        {
            Sid sid = obj as Sid;

            if (ReferenceEquals(sid, null))
            {
                return false;
            }

            return _value == sid._value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}