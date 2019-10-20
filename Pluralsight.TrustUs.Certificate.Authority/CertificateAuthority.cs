using System.IO;
using Pluralsight.TrustUs.DataStructures;
using Pluralsight.TrustUs.Libraries;

namespace Pluralsight.TrustUs
{
    /// <summary>
    ///     Class CertificateAuthority.
    /// </summary>
    public class CertificateAuthority
    {
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

        public void IssueCertificate(CertificateConfiguration certificateConfiguration)
        {
            var caKeyStore = crypt.KeysetOpen(crypt.UNUSED, crypt.KEYSET_FILE, certificateConfiguration.SigningKeyFileName,
                crypt.KEYOPT_READONLY);
            var caKey = crypt.GetPrivateKey(caKeyStore, crypt.KEYID_NAME, certificateConfiguration.SigningKeyLabel, certificateConfiguration.SigningKeyPassword);

            var certStore = crypt.KeysetOpen(crypt.UNUSED, crypt.KEYSET_ODBC_STORE, @"TrustUs",
                crypt.KEYOPT_NONE);

            var certRequest = crypt.CAGetItem(certStore, crypt.CERTTYPE_REQUEST_CERT, crypt.KEYID_NAME,
                certificateConfiguration.DistinguishedName.CommonName);

            crypt.CACertManagement(crypt.CERTACTION_ISSUE_CERT, certStore, caKey, certRequest);

            var certChain = crypt.CAGetItem(certStore, crypt.CERTTYPE_CERTCHAIN, crypt.KEYID_NAME,
                certificateConfiguration.DistinguishedName.CommonName);

            var certificate = new Certificate();
            File.WriteAllText(certificateConfiguration.CertificateFileName, Certificate.ExportCertificateAsText(certChain));

            crypt.DestroyObject(certChain);
            crypt.DestroyObject(caKey);
            crypt.DestroyObject(certRequest);
            crypt.KeysetClose(caKeyStore);
            crypt.KeysetClose(certStore);
        }

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