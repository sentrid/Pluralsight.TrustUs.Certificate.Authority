using System.IO;
using Pluralsight.TrustUs.Data;
using Pluralsight.TrustUs.DataStructures;
using Pluralsight.TrustUs.Libraries;

namespace Pluralsight.TrustUs
{
    /// <summary>
    ///     Class CertificateAuthority.
    /// </summary>
    public class CertificateAuthority
    {
        /// <summary>
        ///     Submits the certificate request.
        /// </summary>
        /// <param name="certificateAuthorityConfiguration"></param>
        /// <param name="certificateRequestFileName">Name of the certificate request file.</param>
        /// TODO Edit XML Comment Template for SubmitCertificateRequest
        public void SubmitCertificateRequest(CertificateAuthorityConfiguration certificateAuthorityConfiguration,
            string certificateRequestFileName)
        {
            var certStore = crypt.KeysetOpen(crypt.UNUSED, crypt.KEYSET_ODBC_STORE,
                certificateAuthorityConfiguration.CertificateStoreOdbcName,
                crypt.KEYOPT_NONE);


            var requestCertificate = Certificate.ImportCertificateFromFile(certificateRequestFileName);

            crypt.CAAddItem(certStore, requestCertificate);

            crypt.DestroyCert(requestCertificate);
            crypt.KeysetClose(certStore);
        }

        /// <summary>
        ///     Issues the certificate.
        /// </summary>
        /// <param name="certificateAuthorityConfiguration">The certificate configuration.</param>
        /// <param name="certificateEmailAddress"></param>
        /// <param name="certificateFileName"></param>
        /// TODO Edit XML Comment Template for IssueCertificate
        public void IssueCertificate(CertificateAuthorityConfiguration certificateAuthorityConfiguration,
            string certificateEmailAddress, string certificateFileName)
        {
            var caKeyStore = crypt.KeysetOpen(crypt.UNUSED, crypt.KEYSET_FILE,
                certificateAuthorityConfiguration.SigningKeyFileName,
                crypt.KEYOPT_READONLY);
            var caKey = crypt.GetPrivateKey(caKeyStore, crypt.KEYID_NAME,
                certificateAuthorityConfiguration.SigningKeyLabel,
                certificateAuthorityConfiguration.SigningKeyPassword);

            var certStore = crypt.KeysetOpen(crypt.UNUSED, crypt.KEYSET_ODBC_STORE,
                certificateAuthorityConfiguration.CertificateStoreOdbcName,
                crypt.KEYOPT_READONLY);

            var certRequest = crypt.CAGetItem(certStore, crypt.CERTTYPE_REQUEST_CERT, crypt.KEYID_EMAIL,
                certificateEmailAddress);

            crypt.CACertManagement(crypt.CERTACTION_ISSUE_CERT, certStore, caKey, certRequest);

            var certChain = crypt.CAGetItem(certStore, crypt.CERTTYPE_CERTCHAIN, crypt.KEYID_EMAIL,
                certificateEmailAddress);

            File.WriteAllText($"{ConfigurationData.BaseDirectory}\\{certificateFileName}",
                Certificate.ExportCertificateAsText(certChain));

            crypt.DestroyObject(certChain);
            crypt.DestroyObject(caKey);
            crypt.DestroyObject(certRequest);
            crypt.KeysetClose(caKeyStore);
            crypt.KeysetClose(certStore);
        }

        /// <summary>
        ///     Revokes the certificate.
        /// </summary>
        /// <param name="crlFileName">Name of the CRL file.</param>
        /// <param name="caKeyFileName">Name of the ca key file.</param>
        /// TODO Edit XML Comment Template for RevokeCertificate
        public void RevokeCertificate(string crlFileName, string caKeyFileName)
        {
            var importCertificate = Certificate.ImportCertificate(File.ReadAllText(crlFileName));
            var certStore = crypt.KeysetOpen(crypt.UNUSED, crypt.KEYSET_ODBC_STORE, @"TrustUs",
                crypt.KEYOPT_NONE);
            var caKey = crypt.KeysetOpen(crypt.UNUSED, crypt.KEYSET_FILE, caKeyFileName, crypt.KEYOPT_READONLY);
            crypt.CACertManagement(crypt.CERTACTION_REVOKE_CERT, certStore, caKey, importCertificate);

            crypt.KeysetClose(certStore);
            crypt.KeysetClose(caKey);
        }
    }
}