using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DVLD__Business_Tier
{
    public static class clsPasswordHasher
    {
        // The sizes of the salt and hash in bytes
        private const int SaltSize = 16;
        private const int HashSize = 32;

        // Iterations make the hashing slower, protecting against brute-force attacks
        private const int Iterations = 100000;

        /// <summary>
        /// Hashes a password and returns a single Base64 string containing both the salt and the hash.
        /// Save this string directly to your database.
        /// </summary>
        public static string HashPassword(string password)
        {
            // 1. Generate a random "Salt"
            byte[] salt = new byte[SaltSize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            // 2. Hash the password with the salt
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                byte[] hash = pbkdf2.GetBytes(HashSize);

                // 3. Combine the salt and password hash into one array
                byte[] hashBytes = new byte[SaltSize + HashSize];
                Array.Copy(salt, 0, hashBytes, 0, SaltSize);
                Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

                // 4. Convert to a string so you can easily save it in SQL
                return Convert.ToBase64String(hashBytes);
            }
        }

        /// <summary>
        /// Compares a plain-text password typed by the user with the hashed string from the database.
        /// </summary>
        public static bool VerifyPassword(string passwordEntered, string savedHash)
        {
            try
            {
                // 1. Convert the saved string back into bytes
                byte[] hashBytes = Convert.FromBase64String(savedHash);

                // 2. Extract the salt from the beginning (first 16 bytes)
                byte[] salt = new byte[SaltSize];
                Array.Copy(hashBytes, 0, salt, 0, SaltSize);

                // 3. Hash the password the user just typed, using that extracted salt
                using (var pbkdf2 = new Rfc2898DeriveBytes(passwordEntered, salt, Iterations))
                {
                    byte[] hash = pbkdf2.GetBytes(HashSize);

                    // 4. Compare the newly generated hash with the saved hash
                    for (int i = 0; i < HashSize; i++)
                    {
                        if (hashBytes[i + SaltSize] != hash[i])
                        {
                            return false; // Passwords do not match
                        }
                    }
                    return true; // Passwords match!
                }
            }
            catch
            {
                // If the string is corrupted or invalid, fail the login
                return false;
            }
        }        
    }
}
