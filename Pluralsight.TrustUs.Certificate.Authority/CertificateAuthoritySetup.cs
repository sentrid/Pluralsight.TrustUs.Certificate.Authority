using System.Collections.Generic;
using System.IO;
using Pluralsight.TrustUs.Data;
using Pluralsight.TrustUs.DataStructures;
using Pluralsight.TrustUs.Libraries;

namespace Pluralsight.TrustUs
{
    /// <summary>
    /// Class CertificateAuthoritySetup.
    /// </summary>
    /// TODO Edit XML Comment Template for CertificateAuthoritySetup
    public class CertificateAuthoritySetup
    {
        /// <summary>
        /// Installs the specified root certificate authority.
        /// </summary>
        /// <param name="rootCertificateAuthority">The root certificate authority.</param>
        /// <param name="intermediateCertificateAuthorities">The intermediate certificate authorities.</param>
        public void Install(CertificateAuthorityConfiguration rootCertificateAuthority,
            List<CertificateConfiguration> intermediateCertificateAuthorities)
        {
            if (!Directory.Exists(ConfigurationData.BaseDirectory))
            {
                Directory.CreateDirectory(ConfigurationData.BaseDirectory);
            }

            GenerateRootCaCertificate(rootCertificateAuthority);
            InitializeCertificateStore(rootCertificateAuthority);
            //GeneratePolicyCaCertificate(policyCertificateAuthority);
            foreach (var configuration in intermediateCertificateAuthorities)
                GenerateIntermediateCertificate(configuration);
        }

        /// <summary>
        /// Initializes the certificate store.
        /// </summary>
        /// <param name="configuration">The root certificate authority.</param>
        private void InitializeCertificateStore(CertificateAuthorityConfiguration configuration)
        {
            // Don't need this section unless using SQLite
            if (!File.Exists(configuration.CertificateStoreFilePath))
            {
                var file = File.Create(configuration.CertificateStoreFilePath);
                file.Close();
            }

            var certStore = crypt.KeysetOpen(crypt.UNUSED, crypt.KEYSET_ODBC_STORE, configuration.CertificateStoreOdbcName, crypt.KEYOPT_CREATE);
            crypt.KeysetClose(certStore);
        }

        /// <summary>
        /// Generates the root ca certificate.
        /// </summary>
        /// <param name="configuration">The root certificate authority.</param>
        private void GenerateRootCaCertificate(CertificateAuthorityConfiguration configuration)
        {
            var keyPair = Key.GenerateKeyPair(configuration);
            var certificate = Certificate.CreateCaCertificate(configuration, keyPair);

            crypt.SetAttribute(certificate, crypt.CERTINFO_SELFSIGNED, 1);

            crypt.SetAttribute(certificate, crypt.ATTRIBUTE_CURRENT, crypt.CERTINFO_AUTHORITYINFO_OCSP);
            crypt.SetAttributeString(certificate, crypt.CERTINFO_UNIFORMRESOURCEIDENTIFIER,
                configuration.OcspUrl);

            crypt.SetAttribute(certificate, crypt.ATTRIBUTE_CURRENT, crypt.CERTINFO_AUTHORITYINFO_CRLS);
            crypt.SetAttributeString(certificate, crypt.CERTINFO_UNIFORMRESOURCEIDENTIFIER, 
                configuration.RevocationListUrl);

            crypt.SignCert(certificate, keyPair);

            var keyStore = crypt.KeysetOpen(crypt.UNUSED, crypt.KEYSET_FILE, configuration.KeystoreFileName,
                crypt.KEYOPT_NONE);
            crypt.AddPublicKey(keyStore, certificate);

            new Certificate().ExportCertificateToFile(certificate, configuration.CertificateFileName);

            crypt.KeysetClose(keyStore);
            crypt.DestroyContext(keyPair);
            crypt.DestroyCert(certificate);
        }

        /// <summary>
        /// Generates the intermediate certificate.
        /// </summary>
        /// <param name="configuration">The certificate configuration.</param>
        private void GenerateIntermediateCertificate(CertificateConfiguration configuration)
        {
            var caKeyStore = crypt.KeysetOpen(crypt.UNUSED, crypt.KEYSET_FILE,
                configuration.SigningKeyFileName,
                crypt.KEYOPT_READONLY);
            var caPrivateKey = crypt.GetPrivateKey(caKeyStore, crypt.KEYID_NAME,
                configuration.SigningKeyLabel,
                configuration.SigningKeyPassword);

            var icaKeyPair = Key.GenerateKeyPair(configuration);
            var certChain = Certificate.CreateCaCertificate(configuration, icaKeyPair);

            crypt.SetAttributeString(certChain, crypt.CERTINFO_CERTPOLICYID, "1.3.6.1.4.1.16404.1.2");
            crypt.SetAttributeString(certChain, crypt.CERTINFO_CERTPOLICY_CPSURI, "http://cps.trustusca.net");

            crypt.SignCert(certChain, caPrivateKey);
            var icaKeyStore = crypt.KeysetOpen(crypt.UNUSED, crypt.KEYSET_FILE, configuration.KeystoreFileName, crypt.KEYOPT_NONE);
            crypt.AddPublicKey(icaKeyStore, certChain);

            new Certificate().ExportCertificateToFile(certChain, configuration.CertificateFileName);

            crypt.DestroyCert(certChain);
            crypt.KeysetClose(icaKeyStore);
            crypt.KeysetClose(caKeyStore);
            crypt.DestroyContext(caPrivateKey);
            crypt.DestroyContext(icaKeyPair);
        }
    }
}