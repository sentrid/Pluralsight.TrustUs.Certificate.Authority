using System;
using System.IO;
using System.Text;
using Pluralsight.TrustUs.DataStructures;
using Pluralsight.TrustUs.Libraries;

namespace Pluralsight.TrustUs
{
    public class Certificate
    {
        public byte[] ExportCertificate(int certificateHandle)
        {
            var certificateSize = crypt.ExportCert(null, 0, crypt.CERTFORMAT_CERTIFICATE, certificateHandle);
            var certificateBuffer = new byte[certificateSize];
            crypt.ExportCert(certificateBuffer, certificateSize, crypt.CERTFORMAT_CERTIFICATE, certificateHandle);
            return certificateBuffer;
        }

        public string ExportCertificateAsText(int certificateHandle)
        {
            var certificateSize = crypt.ExportCert(null, 0, crypt.CERTFORMAT_TEXT_CERTIFICATE, certificateHandle);
            var certificateBuffer = new byte[certificateSize];
            crypt.ExportCert(certificateBuffer, certificateSize, crypt.CERTFORMAT_TEXT_CERTIFICATE, certificateHandle);
            var certificate = Encoding.UTF8.GetString(certificateBuffer);
            return certificate;
        }

        public void ExportCertificateToFile(int certificateHandle, string fileName)
        {
            var certificateSize = crypt.ExportCert(null, 0, crypt.CERTFORMAT_TEXT_CERTIFICATE, certificateHandle);
            var certificateBuffer = new byte[certificateSize];
            crypt.ExportCert(certificateBuffer, certificateSize, crypt.CERTFORMAT_TEXT_CERTIFICATE, certificateHandle);
            var certificate = Encoding.UTF8.GetString(certificateBuffer);
        }

        public int ImportCertificate(byte[] certificate)
        {
            var certificateHandle = crypt.ImportCert(certificate, crypt.UNUSED);
            return certificateHandle;
        }

        public int ImportCertificate(string certificate)
        {
            var certificateHandle = crypt.ImportCert(certificate, crypt.UNUSED);
            return certificateHandle;
        }

        public int ImportCertificateFromFile(string certificateFileName)
        {
            var certificateHandle = crypt.ImportCert(File.ReadAllText(certificateFileName), crypt.UNUSED);
            return certificateHandle;
        }

        public void CreateSigningRequest(KeyConfiguration keyConfiguration, int keyPairContext)
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

        public void CreateRevocationRequest(string certificateFileName, string crlFileName)
        {
            var certificateToBeRevoked = ImportCertificateFromFile(certificateFileName);
            var revocationRequest = crypt.CreateCert(crypt.UNUSED, crypt.CERTTYPE_REQUEST_REVOCATION);
            crypt.SetAttribute(revocationRequest, crypt.CERTINFO_CERTIFICATE, certificateToBeRevoked);
            crypt.SetAttribute(revocationRequest, crypt.CERTINFO_CRLREASON, crypt.CRLREASONFLAG_KEYCOMPROMISE);
            ExportCertificateToFile(revocationRequest, crlFileName);
        }
    }
}