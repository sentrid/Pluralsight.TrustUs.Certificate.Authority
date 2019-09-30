using System;
using System.Collections.Generic;
using System.IO;
using Pluralsight.TrustUs.Data;
using Pluralsight.TrustUs.DataStructures;
using Pluralsight.TrustUs.Libraries;

namespace Pluralsight.TrustUs
{
    public class Program
    {
        public static void Main(string[] args)
        {
//#if DEBUG
//            Array.Resize(ref args, 2);
//            args[0] = "install";
//            args[1] = @"C:\Pluralsight\Keys\DuckAir\FlightOps.csr";
//#endif
            if (args.Length == 0)
            {
                ShowHelp();
                return;
            }

            crypt.Init();

            switch (args[0])
            {
                case "install":
                    InstallCertificateAuthority();
                    break;
                case "clean":
                    Directory.Delete(ConfigurationData.BaseDirectory, true);
                    break;
                case "create":
                    CreateCertificateRequest();
                    break;
                case "submit":
                    SubmitCertificateRequest(args);
                    break;
                case "issue":
                    IssueCertificate();
                    break;
                default:
                    ShowHelp();
                    return;
            }


            try
            {
                crypt.End();
            }
            catch (CryptException ce)
            {
                if(ce.Status != -23)
                    throw;
            }
        }

        private static void InstallCertificateAuthority()
        {
            var trustUsCertificateAuthority = new CertificateAuthoritySetup();
            var intermediateCertificateAuthorities = new List<CertificateConfiguration>
            {
                ConfigurationData.Berlin,
                ConfigurationData.Capetown,
                ConfigurationData.Cleveland,
                ConfigurationData.Moscow,
                ConfigurationData.Mumbai,
                ConfigurationData.Santiago,
                ConfigurationData.Sydney
            };
            trustUsCertificateAuthority.Install(ConfigurationData.Root, intermediateCertificateAuthorities);
        }

        private static void CreateCertificateRequest()
        {
            var keyConfiguration = new KeyConfiguration();

            Console.WriteLine("\nCertificate Signing Request Certificate\n" +
                              "---------------------------\n" +
                              "This process will create a Public / Private key pair as well as \n" +
                              "create a certificate signing request for the public key.\n" +
                              "You are going to be walked through each piece of information\n" +
                              "needed for the Certificate Signing Request.\n");
            Console.Write("Country: ");
            keyConfiguration.DistinguishedName.Country = Console.ReadLine();
            Console.Write("State or Locality: ");
            keyConfiguration.DistinguishedName.State = Console.ReadLine();
            Console.Write("City: ");
            keyConfiguration.DistinguishedName.Locality = Console.ReadLine();
            Console.Write("Organization: ");
            keyConfiguration.DistinguishedName.Organization = Console.ReadLine();
            Console.Write("Organizational Unit: ");
            keyConfiguration.DistinguishedName.OrganizationalUnit = Console.ReadLine();
            Console.Write("Common Name: ");
            keyConfiguration.DistinguishedName.CommonName = Console.ReadLine();

            keyConfiguration.KeystoreFileName =
                keyConfiguration.DistinguishedName.CommonName?.Replace(" ", string.Empty) + ".key";
            keyConfiguration.CertificateRequestFileName =
                keyConfiguration.DistinguishedName.CommonName?.Replace(" ", string.Empty) + ".csr";
            keyConfiguration.CertificateFileName =
                keyConfiguration.DistinguishedName.CommonName?.Replace(" ", string.Empty) + ".cer";

            Console.Write("Private Key Password: ");
            keyConfiguration.PrivateKeyPassword = Console.ReadLine();

            Console.Write($"\nKey Store FileName [{keyConfiguration.KeystoreFileName}]: ");
            var tempFileName = Console.ReadLine();
            if (!string.IsNullOrEmpty(tempFileName))
            {
                keyConfiguration.KeystoreFileName = tempFileName;
            }

            Console.Write($"CSR FileName [{keyConfiguration.CertificateRequestFileName}]: ");
            tempFileName = Console.ReadLine();
            if (!string.IsNullOrEmpty(tempFileName))
            {
                keyConfiguration.CertificateRequestFileName = tempFileName;
            }

            Console.Write($"Certificate FileName [{keyConfiguration.CertificateFileName}]: ");
            tempFileName = Console.ReadLine();
            if (!string.IsNullOrEmpty(tempFileName))
            {
                keyConfiguration.CertificateFileName = tempFileName;
            }
        }

        private static void SubmitCertificateRequest(string[] args)
        {
            if (args.Length < 2)
            {
                ShowHelp();
                return;
            }

            new CertificateAuthority().SubmitCertificateRequest(args[1]);
        }

        private static void IssueCertificate()
        {
            var certificateAuthority = new CertificateAuthority();

            var certificateConfiguration = new CertificateConfiguration
            {
                CertificateFileName = @"C:\Pluralsight\Keys\DuckAir\CN4.cer",
                DistinguishedName = new DistinguishedName
                {
                    CommonName = "CN4"
                },
                SigningKeyLabel = ConfigurationData.Berlin.KeyLabel,
                SigningKeyFileName = ConfigurationData.Berlin.KeystoreFileName,
                SigningKeyPassword = ConfigurationData.Berlin.PrivateKeyPassword
            };

            certificateAuthority.IssueCertificate(certificateConfiguration);
        }

        private static void ShowHelp()
        {
            Console.WriteLine("HELP!!");
        }
    }
}