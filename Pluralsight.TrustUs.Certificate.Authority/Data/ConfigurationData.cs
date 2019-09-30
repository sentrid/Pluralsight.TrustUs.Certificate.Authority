using Pluralsight.TrustUs.DataStructures;

namespace Pluralsight.TrustUs.Data
{
    public static class ConfigurationData
    {
        public static string BaseDirectory => @"C:\Pluralsight\Keys";

        public static CertificateAuthorityConfiguration Root => new CertificateAuthorityConfiguration
        {
            CertificateFileName = BaseDirectory + @"\ca.cer",
            KeystoreFileName = BaseDirectory + @"\ca.key",
            DistinguishedName = new DistinguishedName
            {
                Country = "US",
                State = "OH",
                Locality = "Cleveland",
                Organization = "Trust Us",
                OrganizationalUnit = "Certificates",
                CommonName = "Root Certificate"
            },
            KeyLabel = "Root",
            PrivateKeyPassword = "P@ssw0rd",
            CertificateStoreFilePath = BaseDirectory + @"\TrustUsStore.db",
            CertificateStoreOdbcName = "TrustUs",
            CertStoreUrl = @"http://certs.trustusca.net",
            OcspUrl = @"http://ocsp.trustusca.net",
            RevocationListUrl = @"http://crl.trustusca.net"
        };

        public static CertificateAuthorityConfiguration Policy => new CertificateAuthorityConfiguration
        {
            CertificateFileName = BaseDirectory + @"\policy.cer",
            KeystoreFileName = BaseDirectory + @"\policy.key",
            DistinguishedName = new DistinguishedName
            {
                Country = "US",
                State = "OH",
                Locality = "Cleveland",
                Organization = "Trust Us",
                OrganizationalUnit = "Certificates",
                CommonName = "Policy Certificate"
            },
            KeyLabel = "Policy",
            PrivateKeyPassword = "P@ssw0rd",
            CertificateStoreFilePath = BaseDirectory + @"\TrustUsStore.db",
            CertificateStoreOdbcName = "TrustUs",
            CertStoreUrl = @"http://certs.trustusca.net"
        };

        public static CertificateAuthorityConfiguration Cleveland => new CertificateAuthorityConfiguration
        {
            CertificateFileName = BaseDirectory + @"\clevelandIca.cer",
            KeystoreFileName = BaseDirectory + @"\clevelandIca.key",
            DistinguishedName = new DistinguishedName
            {
                Country = "US",
                State = "OH",
                Locality = "Cleveland",
                Organization = "Trust Us",
                OrganizationalUnit = "Certificates",
                CommonName = "Cleveland Certificate"
            },
            KeyLabel = "Cleveland",
            PrivateKeyPassword = "P@ssw0rd",
            SigningKeyFileName = BaseDirectory + @"\policy.key",
            SigningKeyLabel = "Policy",
            SigningKeyPassword = "P@ssw0rd"
        };

        public static CertificateAuthorityConfiguration Mumbai => new CertificateAuthorityConfiguration
        {
            CertificateFileName = BaseDirectory + @"\mumbaiIca.cer",
            KeystoreFileName = BaseDirectory + @"\mumbaiIca.key",
            DistinguishedName = new DistinguishedName
            {
                Country = "IN",
                State = "Maharashtra",
                Locality = "Mumbai",
                Organization = "Trust Us",
                OrganizationalUnit = "Certificates",
                CommonName = "Mumbai Certificate"
            },
            KeyLabel = "Mumbai",
            PrivateKeyPassword = "P@ssw0rd",
            SigningKeyFileName = BaseDirectory + @"\policy.key",
            SigningKeyLabel = "Policy",
            SigningKeyPassword = "P@ssw0rd"
        };

        public static CertificateAuthorityConfiguration Berlin => new CertificateAuthorityConfiguration
        {
            CertificateFileName = BaseDirectory + @"\berlinIca.cer",
            KeystoreFileName = BaseDirectory + @"\berlinIca.key",
            DistinguishedName = new DistinguishedName
            {
                Country = "DE",
                Locality = "Berlin",
                Organization = "Trust Us",
                OrganizationalUnit = "Certificates",
                CommonName = "Berlin Certificate"
            },
            KeyLabel = "Berlin",
            PrivateKeyPassword = "P@ssw0rd",
            SigningKeyFileName = BaseDirectory + @"\policy.key",
            SigningKeyLabel = "Policy",
            SigningKeyPassword = "P@ssw0rd"
        };

        public static CertificateAuthorityConfiguration Santiago => new CertificateAuthorityConfiguration
        {
            CertificateFileName = BaseDirectory + @"\santiagoIca.cer",
            KeystoreFileName = BaseDirectory + @"\santiagoIca.key",
            DistinguishedName = new DistinguishedName
            {
                Country = "CL",
                Locality = "Santiago",
                Organization = "Trust Us",
                OrganizationalUnit = "Certificates",
                CommonName = "Santiago Certificate"
            },
            KeyLabel = "Santiago",
            PrivateKeyPassword = "P@ssw0rd",
            SigningKeyFileName = BaseDirectory + @"\policy.key",
            SigningKeyLabel = "Policy",
            SigningKeyPassword = "P@ssw0rd"
        };

        public static CertificateAuthorityConfiguration Moscow => new CertificateAuthorityConfiguration
        {
            CertificateFileName = BaseDirectory + @"\moscowIca.cer",
            KeystoreFileName = BaseDirectory + @"\moscowIca.key",
            DistinguishedName = new DistinguishedName
            {
                Country = "RU",
                State = "Moscow Oblast",
                Locality = "Moscow",
                Organization = "Trust Us",
                OrganizationalUnit = "Certificates",
                CommonName = "Santiago Certificate"
            },
            KeyLabel = "Moscow",
            PrivateKeyPassword = "P@ssw0rd",
            SigningKeyFileName = BaseDirectory + @"\policy.key",
            SigningKeyLabel = "Policy",
            SigningKeyPassword = "P@ssw0rd"
        };

        public static CertificateAuthorityConfiguration Sydney => new CertificateAuthorityConfiguration
        {
            CertificateFileName = BaseDirectory + @"\sydneyIca.cer",
            KeystoreFileName = BaseDirectory + @"\sydneyIca.key",
            DistinguishedName = new DistinguishedName
            {
                Country = "AU",
                State = "New South Wales",
                Locality = "Sydney",
                Organization = "Trust Us",
                OrganizationalUnit = "Certificates",
                CommonName = "Sydney Certificate"
            },
            KeyLabel = "Sydney",
            PrivateKeyPassword = "P@ssw0rd",
            SigningKeyFileName = BaseDirectory + @"\policy.key",
            SigningKeyLabel = "Policy",
            SigningKeyPassword = "P@ssw0rd"
        };

        public static CertificateAuthorityConfiguration Capetown => new CertificateAuthorityConfiguration
        {
            CertificateFileName = BaseDirectory + @"\capeTownIca.cer",
            KeystoreFileName = BaseDirectory + @"\capeTownIca.key",
            DistinguishedName = new DistinguishedName
            {
                Country = "ZA",
                State = "Western Cape",
                Locality = "Cape Town",
                Organization = "Trust Us",
                OrganizationalUnit = "Certificates",
                CommonName = "Cape Town Certificate"
            },
            KeyLabel = "Cape Town",
            PrivateKeyPassword = "P@ssw0rd",
            SigningKeyFileName = BaseDirectory + @"\policy.key",
            SigningKeyLabel = "Policy",
            SigningKeyPassword = "P@ssw0rd"
        };
    }
}