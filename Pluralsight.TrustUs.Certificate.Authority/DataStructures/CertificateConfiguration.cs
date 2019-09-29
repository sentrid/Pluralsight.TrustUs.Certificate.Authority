namespace Pluralsight.TrustUs.DataStructures
{
    /// <summary>
    /// Class CertificateConfiguration.
    /// </summary>
    public class CertificateConfiguration : KeyConfiguration
    {
        /// <summary>
        /// Gets or sets the name of the signing key file.
        /// </summary>
        /// <value>The name of the signing key file.</value>
        public string SigningKeyFileName { get; set; }

        /// <summary>
        /// Gets or sets the signing key label.
        /// </summary>
        /// <value>The signing key label.</value>
        public string SigningKeyLabel { get; set; }

        /// <summary>
        /// Gets or sets the signing key password.
        /// </summary>
        /// <value>The signing key password.</value>
        public string SigningKeyPassword { get; set; }
        
    }
}