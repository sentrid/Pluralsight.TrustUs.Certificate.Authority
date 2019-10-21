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
        /// <param name="policyCertificateAuthority">The policy certificate authority.</param>
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
            var certificate = Certificate.CreateCertificate(configuration, keyPair);

            crypt.SetAttribute(certificate, crypt.CERTINFO_SELFSIGNED, 1);
            crypt.SetAttribute(certificate, crypt.CERTINFO_CA, 1);

            crypt.SetAttribute(certificate, crypt.ATTRIBUTE_CURRENT, crypt.CERTINFO_AUTHORITYINFO_OCSP);
            crypt.SetAttributeString(certificate, crypt.CERTINFO_UNIFORMRESOURCEIDENTIFIER,
                configuration.OcspUrl);

            crypt.SignCert(certificate, keyPair);

            var keyStore = crypt.KeysetOpen(crypt.UNUSED, crypt.KEYSET_FILE, configuration.KeystoreFileName,
                crypt.KEYOPT_NONE);
            crypt.AddPublicKey(keyStore, certificate);

            Certificate.ExportCertificateToFile(certificate, configuration.CertificateFileName);

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
            /***************************************************************/
            /*                  Get the CA Certificate                     */
            /***************************************************************/
            var caKeyStore = crypt.KeysetOpen(crypt.UNUSED, crypt.KEYSET_FILE,
                configuration.SigningKeyFileName,
                crypt.KEYOPT_READONLY);
            var caPrivateKey = crypt.GetPrivateKey(caKeyStore, crypt.KEYID_NAME,
                configuration.SigningKeyLabel,
                configuration.SigningKeyPassword);

            /* Create an RSA public/private key context, set a label for it, and generate a key into it */
            var icaKeyPair = crypt.CreateContext(crypt.UNUSED, crypt.ALGO_RSA);

            crypt.SetAttributeString(icaKeyPair, crypt.CTXINFO_LABEL, configuration.KeyLabel);
            crypt.SetAttribute(icaKeyPair, crypt.CTXINFO_KEYSIZE, 2048 / 8);
            crypt.GenerateKey(icaKeyPair);

            var icaKeyStore = crypt.KeysetOpen(crypt.UNUSED, crypt.KEYSET_FILE,
                configuration.KeystoreFileName,
                crypt.KEYOPT_CREATE);
            crypt.AddPrivateKey(icaKeyStore, icaKeyPair, configuration.PrivateKeyPassword);
            
            var certChain = crypt.CreateCert(crypt.UNUSED, crypt.CERTTYPE_CERTCHAIN);

            crypt.SetAttribute(certChain, crypt.CERTINFO_SUBJECTPUBLICKEYINFO, icaKeyPair);
            crypt.SetAttributeString(certChain, crypt.CERTINFO_COUNTRYNAME,
                configuration.DistinguishedName.Country);
            crypt.SetAttributeString(certChain, crypt.CERTINFO_ORGANIZATIONNAME,
                configuration.DistinguishedName.Organization);
            crypt.SetAttributeString(certChain, crypt.CERTINFO_ORGANIZATIONALUNITNAME,
                configuration.DistinguishedName.OrganizationalUnit);
            crypt.SetAttributeString(certChain, crypt.CERTINFO_COMMONNAME,
                configuration.DistinguishedName.CommonName);
            crypt.SetAttribute(certChain, crypt.CERTINFO_CA, 1);
            crypt.SetAttributeString(certChain, crypt.CERTINFO_CERTPOLICYID, "1.3.6.1.4.1.0.1.2");
            crypt.SetAttributeString(certChain, crypt.CERTINFO_CERTPOLICY_CPSURI, "http://cps.trustusca.net");

            crypt.SignCert(certChain, caPrivateKey);
            crypt.AddPublicKey(icaKeyStore, certChain);

            var dataSize = crypt.ExportCert(null, 0, crypt.CERTFORMAT_CERTIFICATE, certChain);
            var exportedCert = new byte[dataSize];
            crypt.ExportCert(exportedCert, dataSize * 2, crypt.CERTFORMAT_CERTIFICATE, certChain);

            File.WriteAllBytes(configuration.CertificateFileName, exportedCert);

            crypt.DestroyCert(certChain);
            crypt.KeysetClose(icaKeyStore);
            crypt.KeysetClose(caKeyStore);
            crypt.DestroyContext(caPrivateKey);
            crypt.DestroyContext(icaKeyPair);
        }
    }
}