namespace DirectoryHelper
{
    /// <summary>
    /// Contains the names of attributes valid in Active Directory.
    /// </summary>
    public static class DirectoryAttributes
    {
        /// <summary>Gets the 'when created' date.</summary>
        /// <value>The 'when created' date.</value>
        public static string WhenCreated => "whenCreated";

        /// <summary>Gets the common name.</summary>
        /// <value>The common name.</value>
        public static string CommonName => "cn";

        /// <summary>Gets the description.</summary>
        /// <value>The description.</value>
        public static string Description => "description";

        /// <summary>Gets the distinguished name.</summary>
        /// <value>The distinguished name.</value>
        public static string DistinguishedName => "distinguishedName";

        /// <summary>Gets the mail.</summary>
        /// <value>The mail.</value>
        public static string Mail => "mail";

        /// <summary>Gets the mail nickname.</summary>
        /// <value>The mail nickname.</value>
        public static string MailNickname => "mailNickname";

        /// <summary>Gets the sam account name.</summary>
        /// <value>The sam account name.</value>
        public static string SamAccountName => "sAMAccountName";

        /// <summary>Gets the object SID.</summary>
        /// <value>The object SID.</value>
        public static string ObjectSid => "objectSid";

        /// <summary>Gets the user account control.</summary>
        /// <value>The user account control.</value>
        public static string UserAccountControl => "userAccountControl";

        /// <summary>Gets the user principal name.</summary>
        /// <value>The user principal name.</value>
        public static string UserPrincipalName => "userPrincipalName";

        /// <summary>Gets the last name.</summary>
        /// <value>The last name.</value>
        public static string LastName => "sn";

        /// <summary>Gets the first name.</summary>
        /// <value>The first name.</value>
        public static string FirstName => "giveName";

        /// <summary>Gets the name.</summary>
        /// <value>The name.</value>
        public static string Name => "name";

        /// <summary>Gets the display name.</summary>
        /// <value>The display name.</value>
        public static string DisplayName => "displayName";

        /// <summary>Gets the member of.</summary>
        /// <value>The member of.</value>
        public static string MemberOf => "memberOf";

        /// <summary>Gets the member.</summary>
        /// <value>The member.</value>
        public static string Member => "member";

        /// <summary>Gets the members added.</summary>
        /// <value>The members added.</value>
        public static string MembersAdded => $"{Member};range=1-1";

        /// <summary>Gets the members removed.</summary>
        /// <value>The members removed.</value>
        public static string MembersRemoved => $"{Member};range=0-0";

        /// <summary>Gets the proxy addresses.</summary>
        /// <value>The proxy addresses.</value>
        public static string ProxyAddresses => "proxyAddresses";

        /// <summary>Gets the object category.</summary>
        /// <value>The object category.</value>
        public static string ObjectCategory => "objectCategory";

        /// <summary>Gets the object class.</summary>
        /// <value>The object class.</value>
        public static string ObjectClass => "objectClass";

        /// <summary>Gets the object unique identifier.</summary>
        /// <value>The object unique identifier.</value>
        public static string ObjectGuid => "objectGUID";

        /// <summary>Gets the telephone number.</summary>
        /// <value>The telephone number.</value>
        public static string TelephoneNumber => "telephoneNumber";

        /// <summary>Gets the title.</summary>
        /// <value>The title.</value>
        public static string Title => "title";
    }
}
