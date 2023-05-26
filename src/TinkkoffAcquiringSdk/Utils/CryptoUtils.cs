using System;
using System.Security.Cryptography;
using System.Text;

namespace TinkkoffAcquiringSdk.Utils
{
    public class CryptoUtils
    {
        public static string Sha256(string text)
        {
            var sha256 = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(text));
            return string.IsNullOrEmpty(text)
                ? string.Empty
                : BitConverter.ToString(sha256).Replace("-", string.Empty).ToLowerInvariant();
        }
    }
}