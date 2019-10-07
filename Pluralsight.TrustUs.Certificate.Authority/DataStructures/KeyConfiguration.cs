namespace Pluralsight.TrustUs.DataStructures
{
    public class KeyConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyConfiguration"/> class.
        /// </summary>
        /// TODO Edit XML Comment Template for #ctor
        public KeyConfiguration()
        {
            DistinguishedName = new DistinguishedName();
        }

        /// <summary>
        /// Gets or sets the name of the distinguished.
        /// </summary>
        /// <value>The name of the distinguished.</value>
        public DistinguishedName DistinguishedName { get; set; }

        /// <summary>
        /// Gets or sets the private key password.
        /// </summary>
        /// <value>The private key password.</value>
        public string PrivateKeyPassword { get; set; }

        /// <summary>
        /// Gets or sets the key label.
        /// </summary>
        /// <value>The key label.</value>
        public string KeyLabel { get; set; }

        /// <summary>
        /// Gets or sets the name of the keystore file.
        /// </summary>
        /// <value>The name of the keystore file.</value>
        public string KeystoreFileName { get; set; }

        /// <summary>
        /// Gets or sets the name of the certificate file.
        /// </summary>
        /// <value>The name of the certificate file.</value>
        public string CertificateRequestFileName { get; set; }

        /// <summary>
        /// Gets or sets the filename of the certificate.
        /// </summary>
        public string CertificateFileName { get; set; }
        
    }
}