using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Kakuro.Engine.Authorization
{
    public class Hasher
    {
        /**
         *  <summary>Salt size</summary>
         */
        private const int SaltSize = 16;

        /**
         * <summary>Hash size</summary>
         */
        private const int HashSize = 32;

        /**
         *<summary>Hash prefix</summary>
         */
        private const string HashPrefix = "$KAKURO$V1$";

        /* <summary>Creates a hash from a password.</summary>
         * <param name="password">The password.</param>
         * <param name="iterations">Number of iterations.</param>
         * <returns>The hash.</returns>
         */
        public static string Hash(string password, int iterations)
        {
            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(salt);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            var hash = pbkdf2.GetBytes(HashSize);

            var hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            var base64Hash = Convert.ToBase64String(hashBytes);

            return string.Format("{0}{1}${2}", HashPrefix, iterations, base64Hash);
        }

        /**
         * <summary>Creates a hash from a password with random iterations</summary>
         * <param name="password">The password.</param>
         * <returns>The hash.</returns>
         */
        public static string Hash(string password)
        {
            return Hash(password, new Random().Next(2000, 20000));
        }

        /**
         * <summary>Checks if hash is supported.</summary>
         * <param name="hashString">The hash.</param>
         * <returns>Is supported?</returns>
         */
        public static bool IsHashSupported(string hashString)
        {
            return hashString.StartsWith(HashPrefix);
        }

        /**
         * <summary>Verifies a password against a hash.</summary>
         * <param name="password">The password.</param>
         * <param name="hashedPassword">The hash.</param>
         * <returns>Could be verified?</returns>
         */
        public static bool Verify(string password, string hashedPassword)
        {
            if (!IsHashSupported(hashedPassword))
                throw new NotSupportedException("The hashtype is not supported");

            var splittedHashString = hashedPassword.Replace(HashPrefix, "").Split('$');
            var iterations = int.Parse(splittedHashString[0]);
            var base64Hash = splittedHashString[1];
            var hashBytes = Convert.FromBase64String(base64Hash);

            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            for (var i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
