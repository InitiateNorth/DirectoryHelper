namespace InitiateNorth.DirectoryHelper.Sample
{
    using System.DirectoryServices;
    using System.Linq;
    using DirectoryHelper;

    /// <summary>
    /// Class demonstrates the sample usage of the DirectoryHelper library.
    /// </summary>
    public class Program
    {
        
        /// <summary>
        /// Main entry point for the program.
        /// </summary>
        /// <param name="args">Command line arguments (ignored by this method).</param>
        public static void Main(string[] args)
        {
            var searchTerm = "Testy McTestington";

            // Removing primative obsession
            var fqdnResult = Fqdn.Create("internal.domain.local");

            if (fqdnResult.IsSuccess)
            {
                var fqdn = fqdnResult.Value;

                // Use the extensions to give us an LDAP connection string.
                using (var entry = new DirectoryEntry(fqdn.ToLdapConnectionString()))
                using (var searcher = new DirectorySearcher(entry))
                {
                    // Set the search filter up using the attributes, look for people with display name that matches the search term.
                    searcher.Filter = $"(&({DirectoryAttributes.ObjectClass}=person)({DirectoryAttributes.ObjectCategory}=person)({DirectoryAttributes.DisplayName}={searchTerm}))";

                    // No limit on the page size, so we don't have to page the results.
                    searcher.PageSize = 999;

                    // Add some properties we want to see on the returned results.
                    searcher.PropertiesToLoad.AddRange(
                        new string[]
                        {
                            DirectoryAttributes.DistinguishedName,
                            DirectoryAttributes.ObjectSid,
                            DirectoryAttributes.SamAccountName,
                            DirectoryAttributes.MemberOf,
                            DirectoryAttributes.DisplayName,
                            DirectoryAttributes.Mail,
                        });

                    using (var results = searcher.FindAll())
                    {
                        /* 
                         * Sadly the directory stuff doesn't implement any LINQ related goodness, 
                         * and we have to tell the compiler what we're expecting (a SearchResult from a SearchResultCollection)
                         */
                        foreach (SearchResult result in results)
                        {
                            var mail = result.Properties.GetReference<string>(DirectoryAttributes.Mail);
                            var displayName = result.Properties.GetReference<string>(DirectoryAttributes.DisplayName);
                            var memberOf = result.Properties.GetCollectionReference<string>(DirectoryAttributes.MemberOf).ToList(); // Force evaluation now so you can have a poke about.
                            var objectSid = result.Properties.GetReference<byte[]>(DirectoryAttributes.ObjectSid);
                            // ... etc. etc.

                            /* 
                             * NOTE: Be aware that if you are retrieving a group and want to check the 'members'
                             * property for example, AD will limit the results to 1500 per 'page' unless you are
                             * doing an 'attribute scope query'.
                             */

                            // Some examples of other extensions being used to get a SID
                            if (objectSid.IsSidResolvable())
                            {
                                var readableSid = objectSid.ToSidString();
                                var domainAndUser = objectSid.ToResolvedDomainAndUser();
                            }

                            if (result.GetDirectoryEntry().IsContactObject())
                            {
                                // This person is a contact, not a user.
                            }
                        }
                    }
                }
            }
        }
    }
}