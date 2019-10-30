using System;
using System.IO;
using System.Text;
using Pluralsight.TrustUs.DataStructures;
using Pluralsight.TrustUs.Libraries;

namespace Pluralsight.TrustUs
{
    /// <summary>
    /// Class Certificate.
    /// </summary>
    /// TODO Edit XML Comment Template for Certificate
    public class Certificate
    {
        /// <summary>
        /// Creates the certificate.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="keyContext">The key context.</param>
        /// <returns>System.Int32.</returns>
        /// TODO Edit XML Comment Template for CreateCaCertificate
        public static int CreateCaCertificate(KeyConfiguration configuration, int keyContext)
        {
            var certificate = crypt.CreateCert(crypt.UNUSED, configuration.CertificateType);

            crypt.SetAttribute(certificate, crypt.CERTINFO_SUBJECTPUBLICKEYINFO, keyContext);
            crypt.SetAttributeString(certificate, crypt.CERTINFO_COUNTRYNAME,
                configuration.DistinguishedName.Country);
            crypt.SetAttributeString(certificate, crypt.CERTINFO_STATEORPROVINCENAME,
                configuration.DistinguishedName.State);
            crypt.SetAttributeString(certificate, crypt.CERTINFO_LOCALITYNAME,
                configuration.DistinguishedName.Locality);
            crypt.SetAttributeString(certificate, crypt.CERTINFO_ORGANIZATIONNAME,
                configuration.DistinguishedName.Organization);
            crypt.SetAttributeString(certificate, crypt.CERTINFO_ORGANIZATIONALUNITNAME,
                configuration.DistinguishedName.OrganizationalUnit);
            crypt.SetAttributeString(certificate, crypt.CERTINFO_COMMONNAME,
                configuration.DistinguishedName.CommonName);

            crypt.SetAttribute(certificate, crypt.CERTINFO_CA, 1);

            return certificate;
        }

        /// <summary>
        /// Exports the certificate.
        /// </summary>
        /// <param name="certificateHandle">The certificate handle.</param>
        /// <returns>System.Byte[].</returns>
        /// TODO Edit XML Comment Template for ExportCertificate
        public static byte[] ExportCertificate(int certificateHandle)
        {
            var certificateSize = crypt.ExportCert(null, 0, crypt.CERTFORMAT_CERTIFICATE, certificateHandle);
            var certificateBuffer = new byte[certificateSize];
            crypt.ExportCert(certificateBuffer, certificateSize, crypt.CERTFORMAT_CERTIFICATE, certificateHandle);
            return certificateBuffer;
        }

        /// <summary>
        /// Exports the certificate as text.
        /// </summary>
        /// <param name="certificateHandle">The certificate handle.</param>
        /// <returns>System.String.</returns>
        /// TODO Edit XML Comment Template for ExportCertificateAsText
        public static string ExportCertificateAsText(int certificateHandle)
        {
            var certificateSize = crypt.ExportCert(null, 0, crypt.CERTFORMAT_TEXT_CERTIFICATE, certificateHandle);
            var certificateBuffer = new byte[certificateSize];
            crypt.ExportCert(certificateBuffer, certificateSize, crypt.CERTFORMAT_TEXT_CERTIFICATE, certificateHandle);
            var certificate = Encoding.UTF8.GetString(certificateBuffer);
            return certificate;
        }

        /// <summary>
        /// Exports the certificate to file.
        /// </summary>
        /// <param name="certificateHandle">The certificate handle.</param>
        /// <param name="fileName">Name of the file.</param>
        /// TODO Edit XML Comment Template for ExportCertificateToFile
        public static void ExportCertificateToFile(int certificateHandle, string fileName)
        {
            var certificateSize = crypt.ExportCert(null, 0, crypt.CERTFORMAT_CERTIFICATE, certificateHandle);
            var certificateBuffer = new byte[certificateSize*2];
            crypt.ExportCert(certificateBuffer, certificateSize, crypt.CERTFORMAT_CERTIFICATE, certificateHandle);
            File.WriteAllBytes(fileName, certificateBuffer);
        }

        /// <summary>
        /// Imports the certificate.
        /// </summary>
        /// <param name="certificate">The certificate.</param>
        /// <returns>System.Int32.</returns>
        /// TODO Edit XML Comment Template for ImportCertificate
        public static int ImportCertificate(byte[] certificate)
        {
            var certificateHandle = crypt.ImportCert(certificate, crypt.UNUSED);
            return certificateHandle;
        }

        /// <summary>
        /// Imports the certificate.
        /// </summary>
        /// <param name="certificate">The certificate.</param>
        /// <returns>System.Int32.</returns>
        /// TODO Edit XML Comment Template for ImportCertificate
        public static int ImportCertificate(string certificate)
        {
            var certificateHandle = crypt.ImportCert(certificate, crypt.UNUSED);
            return certificateHandle;
        }

        /// <summary>
        /// Imports the certificate from file.
        /// </summary>
        /// <param name="certificateFileName">Name of the certificate file.</param>
        /// <returns>System.Int32.</returns>
        /// TODO Edit XML Comment Template for ImportCertificateFromFile
        public static int ImportCertificateFromFile(string certificateFileName)
        {
            var certificateHandle = crypt.ImportCert(File.ReadAllText(certificateFileName), crypt.UNUSED);
            return certificateHandle;
        }

        /// <summary>
        /// Creates the signing request.
        /// </summary>
        /// <param name="keyConfiguration">The key configuration.</param>
        /// <param name="keyPairContext">The key pair context.</param>
        /// TODO Edit XML Comment Template for CreateSigningRequest
        public static void CreateSigningRequest(KeyConfiguration keyConfiguration, int keyPairContext)
        {
            var certificate = crypt.CreateCert(crypt.UNUSED, crypt.CERTTYPE_CERTREQUEST);

            crypt.SetAttribute(certificate, crypt.CERTINFO_SUBJECTPUBLICKEYINFO, keyPairContext);
            crypt.SetAttributeString(certificate, crypt.CERTINFO_COUNTRYNAME,
                keyConfiguration.DistinguishedName.Country);
            crypt.SetAttributeString(certificate, crypt.CERTINFO_STATEORPROVINCENAME,
                keyConfiguration.DistinguishedName.State);
            crypt.SetAttributeString(certificate, crypt.CERTINFO_LOCALITYNAME,
                keyConfiguration.DistinguishedName.Locality);
            crypt.SetAttributeString(certificate, crypt.CERTINFO_ORGANIZATIONNAME,
                keyConfiguration.DistinguishedName.Organization);
            crypt.SetAttributeString(certificate, crypt.CERTINFO_ORGANIZATIONALUNITNAME,
                keyConfiguration.DistinguishedName.OrganizationalUnit);
            crypt.SetAttributeString(certificate, crypt.CERTINFO_COMMONNAME,
                keyConfiguration.DistinguishedName.CommonName);

            crypt.SignCert(certificate, keyPairContext);
            
            var certificateText = ExportCertificateAsText(certificate);
            File.WriteAllText(keyConfiguration.CertificateRequestFileName, certificateText);

            crypt.DestroyCert(certificate);

        }

        /// <summary>
        /// Creates the revocation request.
        /// </summary>
        /// <param name="certificateFileName">Name of the certificate file.</param>
        /// <param name="crlFileName">Name of the CRL file.</param>
        /// TODO Edit XML Comment Template for CreateRevocationRequest
        public static void CreateRevocationRequest(string certificateFileName, string crlFileName)
        {
            var certificateToBeRevoked = ImportCertificateFromFile(certificateFileName);
            var revocationRequest = crypt.CreateCert(crypt.UNUSED, crypt.CERTTYPE_REQUEST_REVOCATION);
            crypt.SetAttribute(revocationRequest, crypt.CERTINFO_CERTIFICATE, certificateToBeRevoked);
            crypt.SetAttribute(revocationRequest, crypt.CERTINFO_CRLREASON, crypt.CRLREASONFLAG_KEYCOMPROMISE);
            ExportCertificateToFile(revocationRequest, crlFileName);
        }
    }
}