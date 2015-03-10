using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

// ReSharper disable CheckNamespace
namespace CodeX
// ReSharper restore CheckNamespace
{
    /// <summary>
    /// Helper class for Security related methods.
    /// </summary>
    public static class Security
    {
        /// <summary>
        /// Generates the password.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <param name="pickfrom">the valid characters in password, default value &quot;ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%&amp;*&quot;</param>
        /// <returns></returns>
        /// <example><code> 
        /// CodeX.Security.GeneratePassword(8,"a@c#f$")
        /// </code>
        /// <para> 
        /// returns <i> "@@a$###a"</i>
        /// </para></example>
        public static string GeneratePassword(int length, string pickfrom = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%&*")
        {
            var randomNumber = new StringBuilder();
            var objRand = new Random();

            for (var i = 0; i < length; ++i)
            {
                var randomIndex = objRand.Next(0, pickfrom.Length - 1);
                randomNumber.AppendFormat("{0}", pickfrom[randomIndex]);
            }

            return randomNumber.ToString();
        }


        /// <summary>
        /// Encrypts the given string.
        /// </summary>
        /// <param name="s">The String to be Encrypted</param>
        /// <param name="sharedSecret">The Shared Key.</param>
        /// <param name="salt">Salt if any</param>
        /// <returns>Encrypted String</returns>
        /// <example>
        /// <code>Encrypt("mysecretstring",rijn)</code></example>
        public static string Encrypt(this string s, string sharedSecret, string salt = "26829915-a854-40e4-a93c-2b93933e1a2c")
        {
            var algo = Rijndael.Create();
            var deriveBytes = new Rfc2898DeriveBytes(sharedSecret, Encoding.UTF8.GetBytes(salt));
            algo.Key = deriveBytes.GetBytes(algo.KeySize / 8);
            ICryptoTransform encryptor = algo.CreateEncryptor(algo.Key, algo.IV);

            using (var msEncrypt = new MemoryStream())
            {
                msEncrypt.Write(BitConverter.GetBytes(algo.IV.Length), 0, sizeof(int));
                msEncrypt.Write(algo.IV, 0, algo.IV.Length);
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(s);
                    }
                }
                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }

        /// <summary>
        /// Encrypts the given string.
        /// </summary>
        /// <param name="s">The String to be Encrypted</param>
        /// <param name="hashAlgorithm">The hash algorithm.<see cref="HashAlgorithm"/></param>
        /// <returns>
        /// Encrypted String
        /// </returns>
        /// <example><code>
        /// Encrypt("mysecretstring",sha1CryptoServiceProvider)
        /// </code>
        /// <para>
        /// returns <i>Encoded String</i>
        /// </para></example>
        public static string Encrypt(this string s, HashAlgorithm hashAlgorithm)
        {
            return string.Concat(hashAlgorithm.ComputeHash(Encoding.ASCII.GetBytes(s)).Select(x => x.ToString("X2")));
        }


        private static byte[] ReadByteArray(Stream s)
        {
            var ibuffer = new byte[sizeof(int)];
            if (s.Read(ibuffer, 0, ibuffer.Length) != ibuffer.Length)
            {
                throw new SystemException("Stream did not contain properly formatted byte array");
            }

            var buffer = new byte[BitConverter.ToInt32(ibuffer, 0)];
            if (s.Read(buffer, 0, buffer.Length) != buffer.Length)
            {
                throw new SystemException("Did not read byte array properly");
            }
            return buffer;
        }

        /// <summary>
        /// Decrypts the given string.
        /// </summary>
        /// <param name="s">The String to be Decrypted</param>
        /// <param name="sharedSecret">The Shared Key.</param>
        /// <param name="salt">Salt if any</param>
        /// <returns>
        /// Decrypted String
        /// </returns>
        /// <example><code>
        /// Decrypt("ahshihAkjthJAjdlhASSjLLAjALjASlJLAJALJLhlAShdaklhsdhasd",rijn)
        /// </code><para>
        /// returns <i>Decrypted String</i></para></example>
        public static string Decrypt(this string s, string sharedSecret,
                                     string salt = "26829915-a854-40e4-a93c-2b93933e1a2c")
        {

            var key = new Rfc2898DeriveBytes(sharedSecret, Encoding.UTF8.GetBytes(salt));

            byte[] bytes = Convert.FromBase64String(s);
            using (var msDecrypt = new MemoryStream(bytes))
            {
                var algo = Rijndael.Create();
                algo.Key = key.GetBytes(algo.KeySize / 8);
                algo.IV = ReadByteArray(msDecrypt);

                var decryptor = algo.CreateDecryptor(algo.Key, algo.IV);
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }
    }


}