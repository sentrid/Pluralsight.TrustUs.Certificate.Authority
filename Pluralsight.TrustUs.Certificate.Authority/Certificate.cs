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
        public static int CreateCaCertificate(KeyConfiguration configuration, int keyContext)
        {
            
        }

        public static byte[] ExportCertificate(int certificateHandle)
        {
            var certificateSize = crypt.ExportCert(null, 0, crypt.CERTFORMAT_CERTIFICATE, certificateHandle);
            var certificateBuffer = new byte[certificateSize];
            crypt.ExportCert(certificateBuffer, certificateSize, crypt.CERTFORMAT_CERTIFICATE, certificateHandle);
            return certificateBuffer;
        }

        public static string ExportCertificateAsText(int certificateHandle)
        {
            var certificateSize = crypt.ExportCert(null, 0, crypt.CERTFORMAT_TEXT_CERTIFICATE, certificateHandle);
            var certificateBuffer = new byte[certificateSize];
            crypt.ExportCert(certificateBuffer, certificateSize, crypt.CERTFORMAT_TEXT_CERTIFICATE, certificateHandle);
            var certificate = Encoding.UTF8.GetString(certificateBuffer);
            return certificate;
        }

        public static void ExportCertificateToFile(int certificateHandle, string fileName)
        {
            var certificateSize = crypt.ExportCert(null, 0, crypt.CERTFORMAT_CERTIFICATE, certificateHandle);
            var certificateBuffer = new byte[certificateSize*2];
            crypt.ExportCert(certificateBuffer, certificateSize, crypt.CERTFORMAT_CERTIFICATE, certificateHandle);
            File.WriteAllBytes(fileName, certificateBuffer);
        }

        public static int ImportCertificate(byte[] certificate)
        {
            var certificateHandle = crypt.ImportCert(certificate, crypt.UNUSED);
            return certificateHandle;
        }

        public static int ImportCertificate(string certificate)
        {
            var certificateHandle = crypt.ImportCert(certificate, crypt.UNUSED);
            return certificateHandle;
        }

        public static int ImportCertificateFromFile(string certificateFileName)
        {
            var certificateHandle = crypt.ImportCert(File.ReadAllText(certificateFileName), crypt.UNUSED);
            return certificateHandle;
        }

        public static void CreateSigningRequest(KeyConfiguration keyConfiguration, int keyPairContext)
        {
            
        }
    }
}