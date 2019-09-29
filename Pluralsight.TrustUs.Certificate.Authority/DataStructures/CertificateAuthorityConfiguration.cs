namespace Pluralsight.TrustUs.DataStructures
{
    public class CertificateAuthorityConfiguration : CertificateConfiguration
    {
        public string CertificateStoreFilePath { get; set; }

        public string CertificateStoreOdbcName { get; set; }

        public string CertStoreUrl { get; set; }

        public string RevocationListUrl { get; set; }

        public string OcspUrl { get; set; }

    }
}