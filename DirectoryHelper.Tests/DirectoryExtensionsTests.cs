namespace InitiateNorth.DirectoryHelper.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using DirectoryHelper;

    [TestClass]
    public class DirectoryExtensionsTests
    {
        #region Case testing

        [TestMethod]
        public void IsEqualWhenCaseIsDifferent()
        {
            var a = "TEST";
            var b = "test";

            var result = a.EqualsCaseInsensitive(b);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsEqualWhenCaseIsSame()
        {
            var a = "test";
            var b = "test";

            var result = a.EqualsCaseInsensitive(b);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsNotEqualWhenDifferentStrings()
        {
            var a = "ode to joy";
            var b = "tESt";

            var result = a.EqualsCaseInsensitive(b);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void FqdnIsEqualWhenCaseIsDifferent()
        {
            var fqdn = "foo.google.com";
            var a = Fqdn.Create(fqdn.ToUpperInvariant()).Value;
            var b = Fqdn.Create(fqdn.ToLowerInvariant()).Value;

            Assert.IsTrue(a.Equals(b));
        }

        #endregion

        #region Trimming

        [TestMethod]
        public void IsTrimmedCorrectlyWithStringWithSameCase()
        {
            var input = "SMTP:primary.email@address.com";
            var toTrim = "SMTP:";

            var result = input.TrimStart(toTrim);

            Assert.AreEqual("primary.email@address.com", result);
        }

        [TestMethod]
        public void IsTrimmedCorrectlyWithStringWithDifferentCase()
        {
            var input = "SMTP:primary.email@address.com";
            var toTrim = "smtp:";

            var result = input.TrimStart(toTrim);

            Assert.AreEqual("primary.email@address.com", result);
        }

        #endregion

        #region LDAP filter value testing

        [TestMethod]
        public void IsLdapFilterValueReplacedWhenUsingSlashes()
        {
            var input = @"havea\inthe/morning";

            var result = input.EscapeValueForLdapFilter();

            Assert.AreEqual(@"havea\5cinthe\2fmorning", result);
        }

        [TestMethod]
        public void IsLdapFilterValueReplacedWhenUsingSpaces()
        {
            var input = @"everyone loves the whitespace";

            var result = input.EscapeValueForLdapFilter();

            Assert.AreEqual(@"everyone\20loves\20the\20whitespace", result);
        }

        [TestMethod]
        public void IsLdapFilterValueReplacedWhenUsingAsterix()
        {
            var input = @"fred*";

            var result = input.EscapeValueForLdapFilter();

            Assert.AreEqual(@"fred\2a", result);
        }

        [TestMethod]
        public void IsLdapFilterValueReplacedWhenUsingParens()
        {
            var input = @"something(useful)";

            var result = input.EscapeValueForLdapFilter();

            Assert.AreEqual(@"something\28useful\29", result);
        }

        #endregion

        #region LDAP Configuration context testing

        [TestMethod]
        public void IsCorrectLdapConfigWhenRightMixtureOfFqdnAndDomainController()
        {
            var fqdn = "google.com";
            var dc = "domaincontroller.google.com";
            var fqdnResult = Fqdn.Create(fqdn);

            Assert.IsTrue(fqdnResult.IsSuccess);

            var result = fqdnResult.Value.ToLdapConfigurationConnectionString(dc);

            Assert.AreEqual("LDAP://domaincontroller.google.com/CN=Configuration,DC=google,DC=com", result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsCorrectLdapConfigExceptionWhenBadMixtureOfFqdnAndDomainController()
        {
            var fqdn = "google.com";
            var dc = "domaincontroller.yahoo.com";
            var fqdnResult = Fqdn.Create(fqdn);

            Assert.IsTrue(fqdnResult.IsSuccess);

            var result = fqdnResult.Value.ToLdapConfigurationConnectionString(dc);

            Assert.AreEqual("LDAP://domaincontroller.google.com/CN=Configuration,DC=google,DC=com", result);
        }

        [TestMethod]
        public void IsCorrectLdapConfigConnectionStringWithValidDomainFqdn()
        {
            var fqdn = "google.com";
            var fqdnResult = Fqdn.Create(fqdn);

            Assert.IsTrue(fqdnResult.IsSuccess);

            var result = fqdnResult.Value.ToLdapConfigurationConnectionString();

            Assert.AreEqual("LDAP://google.com/CN=Configuration,DC=google,DC=com", result);
        }

        [TestMethod]
        public void IsFailedLdapConfigConnectionStringWithBadDomainFqdn()
        {
            var fqdn = "controller.google.";
            var fqdnResult = Fqdn.Create(fqdn);
            Assert.IsFalse(fqdnResult.IsSuccess);
        }

        [TestMethod]
        public void IsCorrectLdapConfigConnectionStringWithValidDomainFqdnString()
        {
            var fqdn = "google.com";
            var result = fqdn.ToLdapConfigurationConnectionString();

            Assert.AreEqual("LDAP://google.com/CN=Configuration,DC=google,DC=com", result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsFailedLdapConfigConnectionStringWithBadDomainFqdnString()
        {
            var fqdn = "controller.google.";
            fqdn.ToLdapConfigurationConnectionString();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsFailedLdapConfigConnectionStringWithBadEmptyDomainFqdnString()
        {
            var fqdn = "";
            fqdn.ToLdapConfigurationConnectionString();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsFailedLdapConfigConnectionStringWithBadNullDomainFqdnString()
        {
            string fqdn = null;
            fqdn.ToLdapConfigurationConnectionString();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsFailedLdapConfigConnectionStringWithBadSpaceyDomainFqdnString()
        {
            var fqdn = "  ";
            fqdn.ToLdapConfigurationConnectionString();
        }

        #endregion

        #region LDAP Default context testing

        [TestMethod]
        public void IsCorrectLdapDefaultWhenRightMixtureOfFqdnAndDomainController()
        {
            var fqdn = "google.com";
            var dc = "domaincontroller.google.com";
            var fqdnResult = Fqdn.Create(fqdn);

            Assert.IsTrue(fqdnResult.IsSuccess);

            var result = fqdnResult.Value.ToLdapConnectionString(dc);

            Assert.AreEqual("LDAP://domaincontroller.google.com/DC=google,DC=com", result);
        }

        [TestMethod]
        public void IsCorrectLdapDefaultWhenRightMixtureOfFqdnAndDomainControllerCaseSensitive()
        {
            var fqdn = "GOOGLE.COM";
            var dc = "domaincontroller.google.com";
            var fqdnResult = Fqdn.Create(fqdn);

            Assert.IsTrue(fqdnResult.IsSuccess);

            var result = fqdnResult.Value.ToLdapConnectionString(dc);

            Assert.AreEqual("LDAP://domaincontroller.google.com/DC=GOOGLE,DC=COM", result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsCorrectLdapDefaultExceptionWhenBadMixtureOfFqdnAndDomainController()
        {
            var fqdn = "google.com";
            var dc = "domaincontroller.yahoo.com";
            var fqdnResult = Fqdn.Create(fqdn);

            Assert.IsTrue(fqdnResult.IsSuccess);

            var result = fqdnResult.Value.ToLdapConnectionString(dc);

            Assert.AreEqual("LDAP://domaincontroller.google.com/DC=google,DC=com", result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsCorrectLdapDefaultExceptionWhenBadMixtureOfFqdnAndDomainControllerCaseInsensitive()
        {
            var fqdn = "google.com";
            var dc = "domaincontroller.yahoo.com";
            var fqdnResult = Fqdn.Create(fqdn);

            Assert.IsTrue(fqdnResult.IsSuccess);

            var result = fqdnResult.Value.ToLdapConnectionString(dc);

            Assert.AreEqual("LDAP://DOMAINCONTROLLER.GOOGLE.COM/DC=google,DC=com", result);
        }

        [TestMethod]
        public void IsCorrectLdapDefaultConnectionStringWithValidDomainFqdn()
        {
            var fqdn = "google.com";
            var fqdnResult = Fqdn.Create(fqdn);

            Assert.IsTrue(fqdnResult.IsSuccess);

            var result = fqdnResult.Value.ToLdapConnectionString();

            Assert.AreEqual("LDAP://google.com/DC=google,DC=com", result);
        }

        [TestMethod]
        public void IsFailedLdapDefaultConnectionStringWithBadDomainFqdn()
        {
            var fqdn = "controller.google.";
            var fqdnResult = Fqdn.Create(fqdn);
            Assert.IsFalse(fqdnResult.IsSuccess);
        }

        [TestMethod]
        public void IsCorrectLdapDefaultConnectionStringWithValidDomainFqdnString()
        {
            var fqdn = "google.com";
            var result = fqdn.ToLdapConnectionString();

            Assert.AreEqual("LDAP://google.com/DC=google,DC=com", result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsFailedLdapDefaultConnectionStringWithBadDomainFqdnString()
        {
            var fqdn = "controller.google.";
            fqdn.ToLdapConnectionString();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsFailedLdapDefaultConnectionStringWithBadEmptyDomainFqdnString()
        {
            var fqdn = "";
            fqdn.ToLdapConnectionString();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsFailedLdapDefaultConnectionStringWithBadNullDomainFqdnString()
        {
            string fqdn = null;
            fqdn.ToLdapConnectionString();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsFailedLdapDefaultConnectionStringWithBadSpaceyDomainFqdnString()
        {
            var fqdn = "  ";
            fqdn.ToLdapConnectionString();
        }

        #endregion
        
        /* TODO: tests for:
         *  public static string ToLdapDNConnectionString(this Fqdn domainFqdn, string dn)
         *  public static string ToLdapSidConnectionString(this Fqdn domainFqdn, AccountSid sid)
         *  and their string counterparts
         */
    }
}
