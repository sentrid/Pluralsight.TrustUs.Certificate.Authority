using System.IO;
using Pluralsight.TrustUs.DataStructures;
using Pluralsight.TrustUs.Libraries;

namespace Pluralsight.TrustUs
{
    /// <summary>
    /// Class CertificateAuthority.
    /// </summary>
    public class CertificateAuthority
    {
        /// <summary>
        /// Starts the ocsp server.
        /// </summary>
        /// TODO Edit XML Comment Template for StartOcspServer
        public static void StartOcspServer()
        {
            var session = crypt.CreateSession(crypt.UNUSED, crypt.SESSION_OCSP_SERVER);
            var privateKeyStore = crypt.KeysetOpen(crypt.UNUSED, crypt.KEYSET_FILE, @"C:\Pluralsight\Test\Keys\clevelandica.key",
                crypt.KEYOPT_READONLY);
            var privateKey = crypt.GetPrivateKey(privateKeyStore, crypt.KEYID_NAME, "Cleveland", "P@ssw0rd");
            var certStore = crypt.KeysetOpen(crypt.UNUSED, crypt.KEYSET_ODBC_STORE, "TrustUs", crypt.KEYOPT_NONE);
            crypt.SetAttribute(session, crypt.SESSINFO_PRIVATEKEY, privateKey);
            crypt.SetAttribute(session, crypt.SESSINFO_KEYSET, certStore);
            crypt.SetAttribute(session, crypt.SESSINFO_ACTIVE, 1);
            crypt.KeysetClose(privateKeyStore);
        }

        /// <summary>
        /// Submits the certificate request.
        /// </summary>
        /// <param name="certificateRequestFileName">Name of the certificate request file.</param>
        /// TODO Edit XML Comment Template for SubmitCertificateRequest
        public void SubmitCertificateRequest(string certificateRequestFileName)
        {
            var certStore = crypt.KeysetOpen(crypt.UNUSED, crypt.KEYSET_ODBC_STORE, @"TrustUs",
                crypt.KEYOPT_NONE);

            var requestCertificate = File.ReadAllText(certificateRequestFileName);
            var certRequest = crypt.ImportCert(requestCertificate, crypt.UNUSED);
            crypt.CAAddItem(certStore, certRequest);

            crypt.DestroyCert(certRequest);
            crypt.KeysetClose(certStore);
        }

        /// <summary>
        /// Issues the certificate.
        /// </summary>
        /// <param name="certificateAuthorityConfiguration">The certificate configuration.</param>
        /// TODO Edit XML Comment Template for IssueCertificate
        public void IssueCertificate(CertificateAuthorityConfiguration certificateAuthorityConfiguration)
        {
            var caKeyStore = crypt.KeysetOpen(crypt.UNUSED, crypt.KEYSET_FILE, certificateAuthorityConfiguration.SigningKeyFileName,
                crypt.KEYOPT_READONLY);
            var caKey = crypt.GetPrivateKey(caKeyStore, crypt.KEYID_NAME, certificateAuthorityConfiguration.SigningKeyLabel, 
                certificateAuthorityConfiguration.SigningKeyPassword);

            var certStore = crypt.KeysetOpen(crypt.UNUSED, crypt.KEYSET_ODBC_STORE, certificateAuthorityConfiguration.CertificateStoreOdbcName,
                crypt.KEYOPT_NONE);

            var certRequest = crypt.CAGetItem(certStore, crypt.CERTTYPE_REQUEST_CERT, crypt.KEYID_NAME,
                certificateAuthorityConfiguration.DistinguishedName.CommonName);

            crypt.CACertManagement(crypt.CERTACTION_ISSUE_CERT, certStore, caKey, certRequest);

            var certChain = crypt.CAGetItem(certStore, crypt.CERTTYPE_CERTCHAIN, crypt.KEYID_NAME,
                certificateAuthorityConfiguration.DistinguishedName.CommonName);

            File.WriteAllText(certificateAuthorityConfiguration.CertificateFileName, Certificate.ExportCertificateAsText(certChain));

            crypt.DestroyObject(certChain);
            crypt.DestroyObject(caKey);
            crypt.DestroyObject(certRequest);
            crypt.KeysetClose(caKeyStore);
            crypt.KeysetClose(certStore);
        }

        /// <summary>
        /// Revokes the certificate.
        /// </summary>
        /// <param name="crlFileName">Name of the CRL file.</param>
        /// <param name="caKeyFileName">Name of the ca key file.</param>
        /// TODO Edit XML Comment Template for RevokeCertificate
        public void RevokeCertificate(string crlFileName, string caKeyFileName)
        {
            var certificate = new Certificate();
            var importCertificate = certificate.ImportCertificate(File.ReadAllText(crlFileName));
            var certStore = crypt.KeysetOpen(crypt.UNUSED, crypt.KEYSET_ODBC_STORE, @"TrustUs",
                crypt.KEYOPT_NONE);
            var caKey = crypt.KeysetOpen(crypt.UNUSED, crypt.KEYSET_FILE, caKeyFileName, crypt.KEYOPT_READONLY);
            crypt.CACertManagement(crypt.CERTACTION_REVOKE_CERT, certStore, caKey, importCertificate);

            crypt.KeysetClose(certStore);
            crypt.KeysetClose(caKey);
        }
    }
}