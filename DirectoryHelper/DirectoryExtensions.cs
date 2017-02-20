namespace DirectoryHelper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security;
    using System.DirectoryServices;
    using System.Security.Principal;

    /// <summary>
    /// Extension methods to help in dealing with the <see cref="System.DirectoryServices.DirectoryEntry"/> class.
    /// </summary>
    public static class DirectoryExtensions
    {
        #region DirectoryEntry extensions

        /// <summary>
        /// Gets a collection of reference typed properties for the specified attribute name.
        /// </summary>
        /// <typeparam name="T">The type we expect to be contained in the collection.</typeparam>
        /// <param name="properties">The properties to extract our values from.</param>
        /// <param name="attributeName">The name of the attribute to get the property values for.</param>
        /// <returns>
        /// A collection of properties, or null/empty if they cannot be found.
        /// </returns>
        /// <remarks>The collection may still contain null values, as we are using the friendly 'as' casting method.</remarks>
        public static IEnumerable<T> GetCollectionReference<T>(this PropertyCollection properties, string attributeName) where T : class
        {
            if (properties.Contains(attributeName) && properties[attributeName].Count > 0)
            {
                var items = new List<T>();

                foreach (var attribute in properties[attributeName])
                {
                    items.Add(attributeName as T);
                }

                return items;
            }
            else if (typeof(T) == typeof(string))
            {
                // TODO: Is this really needed if the requestor is aware we return null and is using the null-conditional operator '?.'
                // We couldn't find what the requestor asked for, but they wanted a collection of strings.
                return (IEnumerable<T>)new List<string>();
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// Gets a collection of reference typed properties for the specified attribute name.
        /// </summary>
        /// <typeparam name="T">The type we expect to be contained in the collection.</typeparam>
        /// <param name="properties">The properties to extract the values from.</param>
        /// <param name="attributeName">The name of the attribute to get the property values for.</param>
        /// <returns>
        /// A collection of properties, or null/empty if they cannot be found.
        /// </returns>
        /// <remarks>The collection may still contain null values, as we are using the friendly 'as' casting method.</remarks>
        public static IEnumerable<T> GetCollectionReference<T>(this ResultPropertyCollection properties, string attributeName) where T : class
        {
            if (properties.Contains(attributeName) && properties[attributeName].Count > 0)
            {
                var items = new List<T>();

                foreach (var attribute in properties[attributeName])
                {
                    items.Add(attributeName as T);
                }

                return items;
            }
            else if (typeof(T) == typeof(string))
            {
                // TODO: Is this really needed if the requestor is aware we return null and is using the null-conditional operator '?.'
                // We couldn't find what the requestor asked for, but they wanted a collection of strings.
                return (IEnumerable<T>)new List<string>();
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// Gets the value for the specified attribute name.
        /// </summary>
        /// <typeparam name="T">The return type the value should be cast to.</typeparam>
        /// <param name="properties">The properties to extract the value from.</param>
        /// <param name="attributeName">The name of the attribute to get the property value for.</param>
        /// <param name="index">The array index of the property we want to return (all properties are multi-valued).</param>
        /// <returns>The typed value of the property at the specified index if it can be found, otherwise null/empty.</returns>
        public static T GetReference<T>(this PropertyCollection properties, string attributeName, int index = 0) where T : class
        {
            if (properties.Contains(attributeName) && properties[attributeName].Count >= index)
            {
                return properties[attributeName][index] as T;
            }
            else if (typeof(T) == typeof(string))
            {
                // TODO: Is this really needed if the requestor is aware we return null and is using the null-conditional operator '?.'
                // We couldn't find what the requestor asked for, but they wanted a string.
                return (T)(object)string.Empty;
            }
            else
            {
                return null;
            }
        }
                
        /// <summary>
        /// Gets the value for the specified attribute name.
        /// </summary>
        /// <typeparam name="T">The return type the value should be cast to.</typeparam>
        /// <param name="properties">The properties to extract the value from.</param>
        /// <param name="attributeName">The name of the attribute to get the property value for.</param>
        /// <param name="index">The array index of the property we want to return (all properties are multi-valued).</param>
        /// <returns>The typed value of the property at the specified index if it can be found otherwise null/empty.</returns>
        public static T GetReference<T>(this ResultPropertyCollection properties, string attributeName, int index = 0) where T : class
        {
            // TODO: Is there a way to abstract the duplicate ResultPropertyCollection/PropertyCollection so we don't have to copy/paste methods? IDictionary?
            if (properties.Contains(attributeName) && properties[attributeName].Count >= index)
            {
                return properties[attributeName][index] as T;
            }
            else if (typeof(T) == typeof(string))
            {
                // TODO: Is this really needed if the requestor is aware we return null and is using the null-conditional operator '?.'
                // We couldn't find what the requestor asked for, but they wanted a string.
                return (T)(object)string.Empty;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get a value typed property for the specified attribute name.
        /// </summary>
        /// <typeparam name="T">The return value type that should be returned.</typeparam>
        /// <param name="properties">The properties to extract the value from.</param>
        /// <param name="attributeName">The name of the attribute to get the property value for.</param>
        /// <param name="index">The array index of the proeprty we want to return (all properties are multi-valued).</param>
        /// <returns>The typed value of the property at the specified index if it can be found and cast, otherwise null/default.</returns>
        public static T? GetValue<T>(this PropertyCollection properties, string attributeName, int index = 0) where T : struct
        {
            var result = new T?();

            if (properties.Contains(attributeName) && properties[attributeName].Count >= index)
            {
                try
                {
                    result = (T)properties[attributeName][index];
                }
                catch
                {
                    // TODO: Does this seem like the right approach returning a default T to the requestor without them knowing?
                    // NOTE: Ignore the error in casting, we will return the default of T.
                }
            }

            return result;
        }

        /// <summary>
        /// Get a value typed property for the specified attribute name.
        /// </summary>
        /// <typeparam name="T">The return value type that should be returned.</typeparam>
        /// <param name="properties">The properties to extract the value from.</param>
        /// <param name="attributeName">The name of the attribute to get the property value for.</param>
        /// <param name="index">The array index of the proeprty we want to return (all properties are multi-valued).</param>
        /// <returns>The typed value of the property at the specified index if it can be found and cast, otherwise null/default.</returns>
        public static T? GetValue<T>(this ResultPropertyCollection properties, string attributeName, int index = 0) where T : struct
        {
            // TODO: Is there a way to abstract the duplicate ResultPropertyCollection/PropertyCollection so we don't have to copy/paste methods? IDictionary?
            var result = new T?();

            if (properties.Contains(attributeName) && properties[attributeName].Count >= index)
            {
                try
                {
                    result = (T)properties[attributeName][index];
                }
                catch
                {
                    // TODO: Does this seem like the right approach returning a default T to the requestor without them knowing?
                    // NOTE: Ignore the error in casting, we will return the default of T.
                }
            }

            return result;
        }

        /// <summary>
        /// Determines whether the <see cref="DirectoryEntry"/> is a contact object or not.
        /// </summary>
        /// <param name="entry">The <see cref="DirectoryEntry"/> to check.</param>
        /// <returns><c>True</c> if the object is a contact, otherwise <c>false</c>.</returns>
        public static bool IsContactObject(this DirectoryEntry entry)
        {
            return entry.Properties.GetCollectionReference<string>(DirectoryAttributes.ObjectClass).Contains("contact");
        }

        /// <summary>
        /// Determines whether the <see cref="DirectoryEntry"/> is older than the specified time span.
        /// </summary>
        /// <param name="entry">The <see cref="DirectoryEntry"/> to check.</param>
        /// <param name="time">The time span we want to know if the object is older than.</param>
        /// <returns><c>True</c> if the object is older than the time span, otherwise <c>false</c>.</returns>
        public static bool IsOlderThan(this DirectoryEntry entry, TimeSpan time)
        {
            var dateIgnoredFrom = DateTime.Now.Subtract(time);
            var dateCreated = entry.Properties.GetValue<DateTime>(DirectoryAttributes.WhenCreated);

            return dateCreated.HasValue && dateCreated.Value.CompareTo(dateIgnoredFrom) < 1;
        }

        #endregion

        /// <summary>
        /// Converts a byte array to a human readable SID.
        /// </summary>
        /// <param name="value">The SID byte array to convert.</param>
        /// <returns>The human readable SID if it can be converted, otherwise null.</returns>
        public static string ToSidString(this byte[] value)
        {
            string sidString;

            try
            {
                sidString = (new SecurityIdentifier(value, 0)).Value;
            }
            catch
            {
                sidString = null;
            }

            return sidString;
        }

        /// <summary>
        /// Resolves a byte array representing a SID to a human readable domain\user.
        /// </summary>
        /// <param name="value">The SID byte array to resolve.</param>
        /// <returns>The resolved domain and user in the form DOMAIN\user, or null if it cannot be resolved.</returns>
        public static string ToResolvedDomainAndUser(this byte[] value)
        {
            try
            {
                var sid = value.ToSidString();
                var securityIdentifier = new SecurityIdentifier(sid);
                return securityIdentifier.Translate(typeof(NTAccount)).Value;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Determines whether a SID byte array can be resolved.
        /// </summary>
        /// <param name="value">The byte array to attempt to resolve.</param>
        /// <returns><c>True</c> if the value can be resolved, otherwise <c>false</c>.</returns>
        public static bool IsSidResolvable(this byte[] value)
        {
            return !string.IsNullOrEmpty(value.ToResolvedDomainAndUser());
        }

        /// <summary>
        /// Converts a string value to a secure string.
        /// </summary>
        /// <param name="value">The input string to secure.</param>
        /// <returns>The secure version of the string.</returns>
        public static SecureString ToSecureString(this string value)
        {
            var secure = new SecureString();

            foreach (var c in value)
            {
                secure.AppendChar(c);
            }

            secure.MakeReadOnly();

            return secure;
        }

        /// <summary>
        /// Compares two strings for equality ignoring case.
        /// </summary>
        /// <param name="source">The first string.</param>
        /// <param name="target">The second string.</param>
        /// <param name="comparison">Optional comparison allows for overriding of type of case ignored.</param>
        /// <returns><c>True</c> if the strings are equal, otherwise <c>false</c>.</returns>
        public static bool EqualsCaseInsensitive(this string source, string target, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            return source.Equals(target, comparison);
        }

        public static string TrimStart(this string value, string toTrim, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            return value.StartsWith(toTrim, comparison) ? value.Remove(0, toTrim.Length) : value;
        }

        public static string EscapeValueForLdapFilter(this string value)
        {
            /*  NOTE: If we want to provide the ability to pass in an entire DN and correctly escape the right parts, we need some decent regexes.
             *  Chars to escape: https://social.technet.microsoft.com/wiki/contents/articles/5312.active-directory-characters-to-escape.aspx
             *  DN's: https://msdn.microsoft.com/en-us/library/aa366101(v=vs.85).aspx
             *  
             *  @"(?giu)(_{2,}?)"
             */

            // For now we just support escaping the "values" in an ldap filter
            return value
                    .Replace(@"\", @"\5c")
                    .Replace(@" ", @"\20")
                    .Replace(@"*", @"\2a")
                    .Replace(@"(", @"\28")
                    .Replace(@")", @"\29")
                    .Replace(@"/", @"\2f");
        }

        public static byte[] ToByteArray(this string hex)
        {
            // TODO: Some checks to ensure that this is valid hex.
            return Enumerable
                        .Range(0, hex.Length)
                        .Where(x => x % 2 == 0)
                        .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                        .ToArray();
        }               

        #region Object FQDN extensions
        
        public static string ToLdapConfigurationConnectionString(this Fqdn domainFqdn)
        {
            return $"LDAP://{domainFqdn}/CN=Configuration,{domainFqdn.ToDistinguishedName()}";
        }

        public static string ToLdapConnectionString(this Fqdn domainFqdn)
        {
            return $"LDAP://{domainFqdn}/{domainFqdn.ToDistinguishedName()}";
        }

        public static string ToLdapDNConnectionString(this Fqdn domainFqdn, string dn)
        {
            // Note: it seems to be very difficult to use a regex to validate a DN.
            // TODO: validate the DN
            return $"LDAP://{domainFqdn}/{dn}";
        }

        public static string ToLdapSidConnectionString(this Fqdn domainFqdn, Sid sid)
        {
            return $"LDAP://{domainFqdn}/<SID={sid}>";
        }

        #endregion

        #region String FQDN extensions

        public static string ToLdapConfigurationConnectionString(this string domainFqdn)
        {
            var fqdnResult = Fqdn.Create(domainFqdn);

            if (fqdnResult.IsFailure)
            {
                throw new ArgumentException(fqdnResult.Error, "domainFqdn");
            }
            
            return fqdnResult.Value.ToLdapConfigurationConnectionString();
        }

        public static string ToLdapConnectionString(this string domainFqdn)
        {
            var fqdnResult = Fqdn.Create(domainFqdn);

            if (fqdnResult.IsFailure)
            {
                throw new ArgumentException(fqdnResult.Error, "domainFqdn");
            }

            return fqdnResult.Value.ToLdapConnectionString();
        }

        public static string ToLdapDNConnectionString(this string domainFqdn, string dn)
        {
            var fqdnResult = Fqdn.Create(domainFqdn);

            if (fqdnResult.IsFailure)
            {
                throw new ArgumentException(fqdnResult.Error, "domainFqdn");
            }

            return fqdnResult.Value.ToLdapDNConnectionString(dn);
        }

        public static string ToLdapSidConnectionString(this string domainFqdn, string sid)
        {
            var fqdnResult = Fqdn.Create(domainFqdn);
            var sidResult = Sid.Create(sid);

            if (fqdnResult.IsFailure)
            {
                throw new ArgumentException(fqdnResult.Error, "domainFqdn");
            }

            if (sidResult.IsFailure)
            {
                throw new ArgumentException(sidResult.Error, "sid");
            }

            return fqdnResult.Value.ToLdapSidConnectionString(sidResult.Value);
        }

        #endregion

        private static string ToDistinguishedName(this Fqdn domainFqdn)
        {
            return $"DC={domainFqdn.ToString().Replace(".", ",DC=")}";
        }
    }
}
