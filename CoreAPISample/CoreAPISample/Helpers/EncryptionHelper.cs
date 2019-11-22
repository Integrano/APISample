using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CoreAPISample.API.Helpers
{
    /// <summary>
    /// Encryption Helper
    /// </summary>
    public class EncryptionHelper
    {
        /// <summary>
        ///  Gets the instance.
        /// </summary>
        /// <value>
        ///   The instance.
        /// </value>
        public static EncryptionHelper Instance
        {
            get { return new EncryptionHelper(); }
        }

        /// <summary>
        ///  Gets the M_STR four value.
        /// </summary>
        /// <value>
        ///  The M_STR four value.
        /// </value>
        private string FourValue
        {
            get { return "SuFjcEmp/TE="; }
        }

        /// <summary>
        ///     Gets the M_STR key value.
        /// </summary>
        /// <value>
        ///     The M_STR key value.
        /// </value>
        private string KeyValue
        {
            get { return "KIPSToILGp6fl+3gXJvMsN4IajizYBBT"; }
        }

        /// <summary>
        ///  Gets the encrypted value.
        /// </summary>
        /// <param name="pStrInputValue">The application string input value.</param>
        /// <returns>
        ///  Encrypted value.
        /// </returns>
        public string GetEncryptedValue(string pStrInputValue)
        {
            try
            {
                TripleDESCryptoServiceProvider objProvider = GetCryptoProvider();

                // Create a MemoryStream.
                var objMemoryStream = new MemoryStream();

                // Create a CryptoStream using the MemoryStream 
                // and the passed key and initialization vector (IV).
                var objCryptoStream = new CryptoStream(objMemoryStream, objProvider.CreateEncryptor(),
                    CryptoStreamMode.Write);

                // Convert the passed string to a byte array.
                byte[] bytToEncrypt = new ASCIIEncoding().GetBytes(pStrInputValue);

                // Write the byte array to the crypto stream and flush it.
                objCryptoStream.Write(bytToEncrypt, 0, bytToEncrypt.Length);
                objCryptoStream.FlushFinalBlock();

                // Get an array of bytes from the 
                // MemoryStream that holds the 
                // encrypted data.
                byte[] bytReturnValue = objMemoryStream.ToArray();

                // Close the streams.
                objCryptoStream.Close();
                objMemoryStream.Close();

                // Return the encrypted buffer.
                return Convert.ToBase64String(bytReturnValue);
            }
            catch
            {
                return pStrInputValue;
            }
        }

        /// <summary>
        ///  Gets the decrypted value.
        /// </summary>
        /// <param name="pStrInputValue">The application string input value.</param>
        /// <returns>
        ///  Decrypted value.
        /// </returns>
        public string GetDecryptedValue(string pStrInputValue)
        {
            try
            {
                TripleDESCryptoServiceProvider objProvider = GetCryptoProvider();
                byte[] bytInputEquivalent = Convert.FromBase64String(pStrInputValue);

                // Create a new MemoryStream.
                var objMemoryDecrypt = new MemoryStream();

                // Create a CryptoStream using the MemoryStream 
                // and the passed key and initialization vector (IV).
                var objCryptoDecrypt = new CryptoStream(objMemoryDecrypt, objProvider.CreateDecryptor(),
                    CryptoStreamMode.Write);
                objCryptoDecrypt.Write(bytInputEquivalent, 0, bytInputEquivalent.Length);
                objCryptoDecrypt.FlushFinalBlock();
                objCryptoDecrypt.Close();

                // Convert the buffer into a string and return it.
                return new UTF8Encoding().GetString(objMemoryDecrypt.ToArray());
            }
            catch
            {
                return pStrInputValue;
            }
        }

        /// <summary>
        ///  Gets the crypto provider.
        /// </summary>
        /// <returns>
        ///  Crypto provider
        /// </returns>
        private TripleDESCryptoServiceProvider GetCryptoProvider()
        {
            var objProvider = new TripleDESCryptoServiceProvider();
            objProvider.IV = Convert.FromBase64String(FourValue);
            objProvider.Key = Convert.FromBase64String(KeyValue);
            return objProvider;
        }
    }
}
