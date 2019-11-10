using System.Runtime.Remoting.Contexts;
using Pluralsight.TrustUs.DataStructures;
using Pluralsight.TrustUs.Libraries;

namespace Pluralsight.TrustUs.Data
{
    /// <summary>
    /// Class ConfigurationData.
    /// </summary>
    /// TODO Edit XML Comment Template for ConfigurationData
    public static class ConfigurationData
    {
        public static string BaseDirectory => @"C:\Pluralsight\Keys";

        public static CertificateAuthorityConfiguration Root => new CertificateAuthorityConfiguration
        {
            CertificateFileName = BaseDirectory + @"\Ca.cer",
            KeystoreFileName = BaseDirectory + @"\Ca.key",
            DistinguishedName = new DistinguishedName
            {
                Country = "US",
                State = "OH",
                Locality = "Cleveland",
                Organization = "Trust Us",
                OrganizationalUnit = "Certificates",
                CommonName = "Trust Us CA Root Certificate"
            },
            KeyLabel = "Root",
            PrivateKeyPassword = "P@ssw0rd",
            CertificateStoreFilePath = BaseDirectory + @"\TrustUsStore.db",
            CertificateStoreOdbcName = "TrustUs",
            OcspUrl = @"http://ocsp.trustusca.net",
            RevocationListUrl = @"http://crl.trustusca.net",
            CertificateType = crypt.CERTTYPE_CERTIFICATE
        };

        public static CertificateAuthorityConfiguration Policy => new CertificateAuthorityConfiguration
        {
            CertificateFileName = BaseDirectory + @"\Policy.cer",
            KeystoreFileName = BaseDirectory + @"\Policy.key",
            DistinguishedName = new DistinguishedName
            {
                Country = "US",
                State = "OH",
                Locality = "Cleveland",
                Organization = "Trust Us",
                OrganizationalUnit = "Certificates",
                CommonName = "Trust Us Policy CA"
            },
            KeyLabel = "Policy",
            PrivateKeyPassword = "P@ssw0rd",
            SigningKeyFileName = BaseDirectory + @"\ca.key",
            SigningKeyLabel = "Root",
            SigningKeyPassword = "P@ssw0rd",
            CertificateType = crypt.CERTTYPE_CERTCHAIN
        };

        public static CertificateAuthorityConfiguration Cleveland => new CertificateAuthorityConfiguration
        {
            CertificateFileName = BaseDirectory + @"\ClevelandIca.cer",
            KeystoreFileName = BaseDirectory + @"\ClevelandIca.key",
            DistinguishedName = new DistinguishedName
            {
                Country = "US",
                State = "OH",
                Locality = "Cleveland",
                Organization = "Trust Us",
                OrganizationalUnit = "Certificates",
                CommonName = "Trust Us Cleveland CA"
            },
            KeyLabel = "Cleveland",
            PrivateKeyPassword = "P@ssw0rd",
            SigningKeyFileName = BaseDirectory + @"\policy.key",
            SigningKeyLabel = "Policy",
            SigningKeyPassword = "P@ssw0rd",
            CertificateType = crypt.CERTTYPE_CERTCHAIN,
            CertificateStoreOdbcName = "TrustUs"
        };

        public static CertificateAuthorityConfiguration Mumbai => new CertificateAuthorityConfiguration
        {
            CertificateFileName = BaseDirectory + @"\MumbaiIca.cer",
            KeystoreFileName = BaseDirectory + @"\MumbaiIca.key",
            DistinguishedName = new DistinguishedName
            {
                Country = "IN",
                State = "Maharashtra",
                Locality = "Mumbai",
                Organization = "Trust Us",
                OrganizationalUnit = "Certificates",
                CommonName = "Trust Us Mumbai CA"
            },
            KeyLabel = "Mumbai",
            PrivateKeyPassword = "P@ssw0rd",
            SigningKeyFileName = BaseDirectory + @"\policy.key",
            SigningKeyLabel = "Policy",
            SigningKeyPassword = "P@ssw0rd",
            CertificateType = crypt.CERTTYPE_CERTCHAIN,
            CertificateStoreOdbcName = "TrustUs"
        };

        public static CertificateAuthorityConfiguration Berlin => new CertificateAuthorityConfiguration
        {
            CertificateFileName = BaseDirectory + @"\BerlinIca.cer",
            KeystoreFileName = BaseDirectory + @"\BerlinIca.key",
            DistinguishedName = new DistinguishedName
            {
                Country = "DE",
                State = "Brandenburg",
                Locality = "Berlin",
                Organization = "Trust Us",
                OrganizationalUnit = "Certificates",
                CommonName = "Trust Us Berlin CA"
            },
            KeyLabel = "Berlin",
            PrivateKeyPassword = "P@ssw0rd",
            SigningKeyFileName = BaseDirectory + @"\policy.key",
            SigningKeyLabel = "Policy",
            SigningKeyPassword = "P@ssw0rd",
            CertificateType = crypt.CERTTYPE_CERTCHAIN,
            CertificateStoreOdbcName = "TrustUs"
        };

        public static CertificateAuthorityConfiguration Santiago => new CertificateAuthorityConfiguration
        {
            CertificateFileName = BaseDirectory + @"\SantiagoIca.cer",
            KeystoreFileName = BaseDirectory + @"\SantiagoIca.key",
            DistinguishedName = new DistinguishedName
            {
                Country = "CL",
                State = "Santiago Province",
                Locality = "Santiago",
                Organization = "Trust Us",
                OrganizationalUnit = "Certificates",
                CommonName = "Trust Us Santiago CA"
            },
            KeyLabel = "Santiago",
            PrivateKeyPassword = "P@ssw0rd",
            SigningKeyFileName = BaseDirectory + @"\policy.key",
            SigningKeyLabel = "Policy",
            SigningKeyPassword = "P@ssw0rd",
            CertificateType = crypt.CERTTYPE_CERTCHAIN,
            CertificateStoreOdbcName = "TrustUs"
        };

        public static CertificateAuthorityConfiguration Moscow => new CertificateAuthorityConfiguration
        {
            CertificateFileName = BaseDirectory + @"\MoscowIca.cer",
            KeystoreFileName = BaseDirectory + @"\MoscowIca.key",
            DistinguishedName = new DistinguishedName
            {
                Country = "RU",
                State = "Moscow Oblast",
                Locality = "Moscow",
                Organization = "Trust Us",
                OrganizationalUnit = "Certificates",
                CommonName = "Trust Us Moscow CA"
            },
            KeyLabel = "Moscow",
            PrivateKeyPassword = "P@ssw0rd",
            SigningKeyFileName = BaseDirectory + @"\policy.key",
            SigningKeyLabel = "Policy",
            SigningKeyPassword = "P@ssw0rd",
            CertificateType = crypt.CERTTYPE_CERTCHAIN,
            CertificateStoreOdbcName = "TrustUs"
        };

        public static CertificateAuthorityConfiguration Sydney => new CertificateAuthorityConfiguration
        {
            CertificateFileName = BaseDirectory + @"\SydneyIca.cer",
            KeystoreFileName = BaseDirectory + @"\SydneyIca.key",
            DistinguishedName = new DistinguishedName
            {
                Country = "AU",
                State = "New South Wales",
                Locality = "Sydney",
                Organization = "Trust Us",
                OrganizationalUnit = "Certificates",
                CommonName = "Trust Us Sydney CA"
            },
            KeyLabel = "Sydney",
            PrivateKeyPassword = "P@ssw0rd",
            SigningKeyFileName = BaseDirectory + @"\policy.key",
            SigningKeyLabel = "Policy",
            SigningKeyPassword = "P@ssw0rd",
            CertificateType = crypt.CERTTYPE_CERTCHAIN,
            CertificateStoreOdbcName = "TrustUs"
        };

        public static CertificateAuthorityConfiguration Capetown => new CertificateAuthorityConfiguration
        {
            CertificateFileName = BaseDirectory + @"\CapeTownIca.cer",
            KeystoreFileName = BaseDirectory + @"\CapeTownIca.key",
            DistinguishedName = new DistinguishedName
            {
                Country = "ZA",
                State = "Western Cape",
                Locality = "Cape Town",
                Organization = "Trust Us",
                OrganizationalUnit = "Certificates",
                CommonName = "Trust Us Cape Town CA"
            },
            KeyLabel = "Cape Town",
            PrivateKeyPassword = "P@ssw0rd",
            SigningKeyFileName = BaseDirectory + @"\policy.key",
            SigningKeyLabel = "Policy",
            SigningKeyPassword = "P@ssw0rd",
            CertificateType = crypt.CERTTYPE_CERTCHAIN,
            CertificateStoreOdbcName = "TrustUs"
        };

        public static CertificateAuthorityConfiguration GetAuthorityByName(string authorityName)
        {
            switch (authorityName)
            {
                case "Root":
                    return Root;
                case "Policy":
                    return Policy;
                case "Cleveland":
                    return Cleveland;
                case "Mumbai":
                    return Mumbai;
                case "Berlin":
                    return Berlin;
                case "Santiago":
                    return Santiago;
                case "Moscow":
                    return Moscow;
                case "Sydney":
                    return Sydney;
                case "Capetown":
                    return Capetown;
                default:
                    return null;
            }
        }
    }
}