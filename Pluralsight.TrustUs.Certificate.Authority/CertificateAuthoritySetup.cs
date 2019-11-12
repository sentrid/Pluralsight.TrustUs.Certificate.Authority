using System.Collections.Generic;
using System.IO;
using Pluralsight.TrustUs.Data;
using Pluralsight.TrustUs.DataStructures;
using Pluralsight.TrustUs.Libraries;

namespace Pluralsight.TrustUs
{
    public class CertificateAuthoritySetup
    {
        public void Install(CertificateAuthorityConfiguration rootCertificateAuthority,
            List<CertificateConfiguration> intermediateCertificateAuthorities)
        {
            if (!Directory.Exists(ConfigurationData.BaseDirectory))
            {
                Directory.CreateDirectory(ConfigurationData.BaseDirectory);
            }

            GenerateRootCaCertificate(rootCertificateAuthority);
            InitializeCertificateStore(rootCertificateAuthority);
            foreach (var configuration in intermediateCertificateAuthorities)
                GenerateIntermediateCertificate(configuration);
        }

        private void InitializeCertificateStore(CertificateAuthorityConfiguration configuration)
        {
            // Don't need this section unless using SQLite
            if (!File.Exists(configuration.CertificateStoreFilePath))
            {
                var file = File.Create(configuration.CertificateStoreFilePath);
                file.Close();
            }

            var certStore = crypt.KeysetOpen(crypt.UNUSED, crypt.KEYSET_ODBC_STORE, 
                configuration.CertificateStoreOdbcName, crypt.KEYOPT_CREATE);
            crypt.KeysetClose(certStore);
        }

        private void GenerateRootCaCertificate(CertificateAuthorityConfiguration configuration)
        {
            
        }

        private void GenerateIntermediateCertificate(CertificateConfiguration configuration)
        {
            
        }
    }
}