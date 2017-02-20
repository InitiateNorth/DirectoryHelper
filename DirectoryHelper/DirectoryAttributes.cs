namespace DirectoryHelper
{
    public static class DirectoryAttributes
    {
        public static string WhenCreated => "whenCreated";

        public static string CommonName => "cn";

        public static string Description => "description";

        public static string DistinguishedName => "distinguishedName";

        public static string Mail => "mail";

        public static string MailNickname => "mailNickname";

        public static string SamAccountName => "sAMAccountName";

        public static string ObjectSid => "objectSid";

        public static string UserAccountControl => "userAccountControl";

        public static string UserPrincipleName => "userPrincipalName";

        public static string LastName => "sn";

        public static string FirstName => "giveName";

        public static string Name => "name";

        public static string DisplayName => "displayName";

        public static string MemberOf => "memberOf";

        public static string Member => "member";

        public static string MembersAdded => $"{Member};range=1-1";

        public static string MembersRemoved => $"{Member};range=0-0";

        public static string ProxyAddresses => "proxyAddresses";

        public static string ObjectCategory => "objectCategory";

        public static string ObjectClass => "objectClass";

        public static string ObjectGuid => "objectGUID";

        public static string TelephoneNumber => "telephoneNumber";

        public static string Title => "title";
    }
}
