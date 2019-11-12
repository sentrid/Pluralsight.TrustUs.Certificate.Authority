using Pluralsight.TrustUs.DataStructures;
using Pluralsight.TrustUs.Libraries;

namespace Pluralsight.TrustUs
{
    public static class Key
    {
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

        public static int GenerateKeyPair(KeyConfiguration keyConfiguration)
        {
           
        }
    }
}