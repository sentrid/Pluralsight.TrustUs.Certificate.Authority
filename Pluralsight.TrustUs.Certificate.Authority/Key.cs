using Pluralsight.TrustUs.DataStructures;
using Pluralsight.TrustUs.Libraries;

namespace Pluralsight.TrustUs
{
    /// <summary>
    ///     Class Key.
    /// </summary>
    /// TODO Edit XML Comment Template for Key
    public static class Key
    {
        /// <summary>
        ///  Generates a PKI (public/private) key pair using cryptlib.
        /// 
        ///  Since cryptlib is written in C we need to explicitly destroy cryptlib objects
        ///  that we create. In this method we have 2 objects that are allocated and destroyed.
        ///  The first is 'keyPair' which is allocated by 'CreateContext' on the first line of
        ///  the method and destroyed by 'DestroyContext' on the last line of the method.
        /// </summary>
        /// <param name="keyConfiguration">The key configuration.</param>
        //public static void GenerateKeyPair(KeyConfiguration keyConfiguration)
        //{
        //    var keyPair = crypt.CreateContext(crypt.UNUSED, crypt.ALGO_RSA);
        //    crypt.SetAttributeString(keyPair, crypt.CTXINFO_LABEL, keyConfiguration.KeyLabel);
        //    crypt.SetAttribute(keyPair, crypt.CTXINFO_KEYSIZE, 2048 / 8);
        //    crypt.GenerateKey(keyPair);
        //    var keyStore = crypt.KeysetOpen(crypt.UNUSED, crypt.KEYSET_FILE, keyConfiguration.KeystoreFileName,
        //        crypt.KEYOPT_CREATE);
        //    crypt.AddPrivateKey(keyStore, keyPair, keyConfiguration.PrivateKeyPassword);
        //    crypt.KeysetClose(keyStore);

        //    var certClass = new Certificate();
        //    certClass.CreateSigningRequest(keyConfiguration, keyPair);

        //    crypt.DestroyContext(keyPair);
        //}

        /// <summary>
        ///     Gets the private key.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="keyLabel">The key label.</param>
        /// <param name="password">The password.</param>
        /// <returns>System.Byte[].</returns>
        /// TODO Edit XML Comment Template for GetPrivateKey
        public static byte[] GetPrivateKey(string fileName, string keyLabel, string password)
        {
            var privateKeySize = 4096;
            var keyStore = crypt.KeysetOpen(crypt.UNUSED, crypt.KEYSET_FILE, fileName, crypt.KEYOPT_READONLY);
            var privateKeyId = crypt.GetPrivateKey(keyStore, crypt.KEYID_NAME, keyLabel, password);
            var keyContext = crypt.CreateContext(crypt.UNUSED, crypt.ALGO_RSA);
            var privateKey = new byte[privateKeySize];
            crypt.ExportKeyEx(privateKey, privateKeySize, crypt.FORMAT_SMIME, privateKeyId, keyContext);
            crypt.DestroyContext(keyContext);
            crypt.KeysetClose(keyStore);
            return privateKey;
        }

        public static int GetPrivateKeyHandle(string fileName, string keyLabel, string password)
        {
            var keyStore = crypt.KeysetOpen(crypt.UNUSED, crypt.KEYSET_FILE, fileName, crypt.KEYOPT_READONLY);
            var privateKeyId = crypt.GetPrivateKey(keyStore, crypt.KEYID_NAME, keyLabel, password);
            crypt.KeysetClose(keyStore);
            return privateKeyId;
        }

        /// <summary>
        /// Generates the key pair. Must release context from calling method.
        /// </summary>
        /// <param name="keyConfiguration">The key configuration.</param>
        /// <returns>System.Int32.</returns>
        /// TODO Edit XML Comment Template for GenerateKeyPair
        public static int GenerateKeyPair(KeyConfiguration keyConfiguration)
        {
            var keyContext = crypt.CreateContext(crypt.UNUSED, crypt.ALGO_RSA);
            crypt.SetAttributeString(keyContext, crypt.CTXINFO_LABEL, keyConfiguration.KeyLabel);
            crypt.SetAttribute(keyContext, crypt.CTXINFO_KEYSIZE, 2048 / 8);
            crypt.GenerateKey(keyContext);

            var keyStore = 0;

            try
            {
                keyStore = crypt.KeysetOpen(crypt.UNUSED, crypt.KEYSET_FILE, keyConfiguration.KeystoreFileName,
                        crypt.KEYOPT_NONE);
            }
            catch (CryptException cryptException)
            {
                if (cryptException.Status != crypt.ERROR_NOTFOUND)
                {
                    throw;
                }

                keyStore = crypt.KeysetOpen(crypt.UNUSED, crypt.KEYSET_FILE, keyConfiguration.KeystoreFileName,
                    crypt.KEYOPT_CREATE);
            }

            crypt.AddPrivateKey(keyStore, keyContext, keyConfiguration.PrivateKeyPassword);
            crypt.KeysetClose(keyContext);
            return keyContext;
        }
    }
}