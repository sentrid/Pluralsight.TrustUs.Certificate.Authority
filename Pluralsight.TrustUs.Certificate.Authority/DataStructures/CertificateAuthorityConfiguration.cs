namespace Pluralsight.TrustUs.DataStructures
{
    /// <summary>
    /// Class CertificateAuthorityConfiguration.
    /// Implements the <see cref="Pluralsight.TrustUs.DataStructures.CertificateConfiguration" />
    /// </summary>
    /// <seealso cref="Pluralsight.TrustUs.DataStructures.CertificateConfiguration" />
    /// TODO Edit XML Comment Template for CertificateAuthorityConfiguration
    public class CertificateAuthorityConfiguration : CertificateConfiguration
    {
        /// <summary>
        /// Gets or sets the certificate store file path.
        /// </summary>
        /// <value>The certificate store file path.</value>
        /// TODO Edit XML Comment Template for CertificateStoreFilePath
        public string CertificateStoreFilePath { get; set; }

        /// <summary>
        /// Gets or sets the name of the certificate store ODBC.
        /// </summary>
        /// <value>The name of the certificate store ODBC.</value>
        /// TODO Edit XML Comment Template for CertificateStoreOdbcName
        public string CertificateStoreOdbcName { get; set; }

        /// <summary>
        /// Gets or sets the cert store URL.
        /// </summary>
        /// <value>The cert store URL.</value>
        /// TODO Edit XML Comment Template for CertStoreUrl
        public string CertStoreUrl { get; set; }

        /// <summary>
        /// Gets or sets the revocation list URL.
        /// </summary>
        /// <value>The revocation list URL.</value>
        /// TODO Edit XML Comment Template for RevocationListUrl
        public string RevocationListUrl { get; set; }

        /// <summary>
        /// Gets or sets the ocsp URL.
        /// </summary>
        /// <value>The ocsp URL.</value>
        /// TODO Edit XML Comment Template for OcspUrl
        public string OcspUrl { get; set; }

    }
}