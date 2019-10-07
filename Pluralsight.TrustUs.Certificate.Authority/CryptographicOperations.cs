using System;
using System.Text;
using Pluralsight.TrustUs.Libraries;

namespace Pluralsight.TrustUs
{
    /// <summary>
    /// Class CryptographicOperations.
    /// </summary>
    /// TODO Edit XML Comment Template for CryptographicOperations
    public class CryptographicOperations
    {
        /// <summary>
        /// Encrypts the specified plain text.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <returns>System.Byte[].</returns>
        /// TODO Edit XML Comment Template for Encrypt
        public byte[] Encrypt(string plainText)
        {
            var keystore = crypt.KeysetOpen(crypt.UNUSED, crypt.KEYSET_ODBC, "TrustUsTest", crypt.KEYOPT_READONLY);
            var publicKey = crypt.GetPublicKey(keystore, crypt.KEYID_NAME, "Flight Ops");

            crypt.KeysetClose(keystore);

            var envelope = crypt.CreateEnvelope(crypt.UNUSED, crypt.FORMAT_CRYPTLIB);
            crypt.SetAttribute(envelope, crypt.ENVINFO_PUBLICKEY, publicKey);
            crypt.SetAttribute(envelope, crypt.ENVINFO_DATASIZE, plainText.Length);
            var bytesCopied = crypt.PushData(envelope, plainText);
            crypt.FlushData(envelope);

            var encryptedDataBuffer = new byte[4096];

            var popDataLength = crypt.PopData(envelope, encryptedDataBuffer, 4096);
            var encryptedData = new byte[popDataLength];
            Array.Copy(encryptedDataBuffer, encryptedData, popDataLength);

            crypt.DestroyEnvelope(envelope);
            return encryptedData;
        }

        /// <summary>
        /// Decrypts the specified encrypted data.
        /// </summary>
        /// <param name="encryptedData">The encrypted data.</param>
        /// <returns>System.String.</returns>
        /// TODO Edit XML Comment Template for Decrypt
        public string Decrypt(byte[] encryptedData)
        {
            var keySet = crypt.KeysetOpen(crypt.UNUSED, crypt.KEYSET_FILE, @"C:\Pluralsight\Test\Keys\DuckAir.key",
                crypt.KEYOPT_READONLY);

            var envelope = crypt.CreateEnvelope(crypt.UNUSED, crypt.FORMAT_AUTO);
            var pushDataLength = crypt.PushData(envelope, encryptedData);
            crypt.SetAttribute(envelope, crypt.ENVINFO_KEYSET_DECRYPT, keySet);
            var attributeType = crypt.GetAttribute(envelope, crypt.ATTRIBUTE_CURRENT);
            if (attributeType == crypt.ENVINFO_PRIVATEKEY)
                crypt.SetAttributeString(envelope, crypt.ENVINFO_PASSWORD, "P@ssw0rd");








            crypt.FlushData(envelope);
            var decryptedDataBuffer = new byte[4096];
            var dataLength = crypt.PopData(envelope, decryptedDataBuffer, 4096);
            var decryptedData = new byte[dataLength];
            Array.Copy(decryptedDataBuffer, decryptedData, dataLength);
            var data = Encoding.ASCII.GetString(decryptedData);

            crypt.DestroyEnvelope(envelope);
            crypt.KeysetClose(keySet);

            return data;
        }
    }
}