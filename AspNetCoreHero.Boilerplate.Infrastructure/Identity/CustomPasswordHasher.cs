/*

using AspNetCoreHero.Boilerplate.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Identity
{
    public class CustomPasswordHasher : IPasswordHasher<ApplicationUser>  
    {
        //public string HashPassword(ApplicationUser user, string password)
        //{
        //    return password;
        //}

        public string HashPassword(ApplicationUser user, string password)
        {
            string generatedSalt = "$2a$10$Xy5S1u2k7fqd0WLH82wwi.";

            // Process to generate SHA-512 hash
            string sha512Hash = GenerateSHA512Hash(password);
            Console.WriteLine($"SHA-512 Hash: {sha512Hash}");


            return GenerateBCryptHash(password, generatedSalt);
        }
        static string GenerateSHA512Hash(string input)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] hashedBytes = sha512.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        static string GenerateBCryptHash(string input, string salt)
        {
            return BCrypt.Net.BCrypt.HashPassword(input, salt);
        }
    

    //public PasswordVerificationResult VerifyHashedPassword(ApplicationUser user, string hashedPassword, string providedPassword)
    //{
    //    if (hashedPassword.Equals(providedPassword))
    //        return PasswordVerificationResult.Success;
    //    else return PasswordVerificationResult.Failed;
    //}


    public PasswordVerificationResult VerifyHashedPassword(ApplicationUser user, string hashedPassword, string providedPassword)
        {
            //if (user.PlatformSource.ToString()!=PlatformSource.MiningRace.ToString())
            //{
                
            //                    string bcryptSalt = "$2a$10$Xy5S1u2k7fqd0WLH82wwi.";


            //                    // Step 1: Hash using SHA-512 with a random salt
            //                    string sha512Hash = HashWithSHA512(providedPassword + bcryptSalt);

            //                    // Step 2: Hash using BCrypt
            //                    string newBcryptHash = BCrypt.Net.BCrypt.HashPassword(sha512Hash);

            //                    // Step 3: Verify BCrypt hash
            //                    return BCrypt.Net.BCrypt.Verify(sha512Hash, newBcryptHash)
            //                        ? PasswordVerificationResult.Success
            //                        : PasswordVerificationResult.Failed;
                                
            //}
            //else
            {
                // Step 1: Verify SHA-512 hash
                string sha512Hash = HashWithSHA512(providedPassword);

                // Step 2: Verify BCrypt hash
                return BCrypt.Net.BCrypt.Verify(sha512Hash, hashedPassword)
                    ? PasswordVerificationResult.Success
                    : PasswordVerificationResult.Failed;

            }



           

        }

            private string HashWithSHA512(string input)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] hashedBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(input));
                return ConvertToHexString(hashedBytes);
            }
        }

        private string GenerateRandomSalt()
        {
            byte[] saltBytes = new byte[16];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        private string ConvertToHexString(byte[] bytes)
        {
            StringBuilder builder = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                builder.AppendFormat("{0:x2}", b);
            }
            return builder.ToString();
        }
    }

}



*/

using System;
using System.Security.Cryptography;
using AspNetCoreHero.Boilerplate.Infrastructure.Identity.Models;
using System.Text;
using Microsoft.AspNetCore.Identity;
using BCrypt.Net;
using System.Diagnostics.Eventing.Reader;

public class CustomPasswordHasher : IPasswordHasher<ApplicationUser>
{
    private const int DefaultWorkFactor = 12; // Adjust as needed for security/performance

    //public  string HashPassword(ApplicationUser user, string password)
    //{
    //    // 1. Hash the password using SHA-512
    //    byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
    //    using (SHA512 sha512 = SHA512.Create())
    //    {
    //        byte[] passwordHash512 = sha512.ComputeHash(passwordBytes);

    //        // 2. Convert the SHA-512 hash to a Base64 string to use as the BCrypt password
    //        string passwordHashBase64 = Convert.ToBase64String(passwordHash512);

    //        // 3. Apply BCrypt with the default work factor
    //        return BCrypt.Net.BCrypt.HashPassword(passwordHashBase64, DefaultWorkFactor);
    //    }
    //}

    public string HashPassword(ApplicationUser user, string password)
    {
        string generatedSalt = "$2a$10$Xy5S1u2k7fqd0WLH82wwi.";

        // Process to generate SHA-512 hash
        string sha512Hash = GenerateSHA512Hash(password);
        Console.WriteLine($"SHA-512 Hash: {sha512Hash}");


        return GenerateBCryptHash(password, generatedSalt);
    }
    static string GenerateSHA512Hash(string input)
    {
        using (SHA512 sha512 = SHA512.Create())
        {
            byte[] hashedBytes = sha512.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }

    static string GenerateBCryptHash(string input, string salt)
    {
        return BCrypt.Net.BCrypt.HashPassword(input, salt);
    }
    public PasswordVerificationResult VerifyHashedPassword(ApplicationUser user, string hashedPassword, string providedPassword)
    {
        try
        {

            if (user.PlatformSource == PlatformSource.MiningRace)
            {
                //Login case
                bool passwordMatch1 = VerifyBcryptPassword(providedPassword, user.PasswordHashBcrypt);

                string generate512Hash = HashStringWithSHA512(providedPassword);

                //Reset Password case
                bool passwordMatch2 = VerifyBcryptPassword(generate512Hash, user.PasswordHashBcrypt);

                // Return the appropriate result based on the comparison
                return (passwordMatch1 || passwordMatch2)
                    ? PasswordVerificationResult.Success
                    : PasswordVerificationResult.Failed;

            }



            //    if (user.PlatformSource == PlatformSource.MiningRace)
            //{
 
            //    if ((providedPassword == user.PasswordHash512))
            //    {

            //       return PasswordVerificationResult.Success;
            //    }
            //    else
            //    {

            //        return PasswordVerificationResult.Failed;
            //    }


            //}
            // Extract the salt from the stored hashed password
            string[] passwordParts = hashedPassword.Split('$');
            string salt = passwordParts.Length > 2 ? passwordParts[2] : string.Empty;

            // Hash the provided password with the extracted salt using BCrypt
            string hashedProvidedPassword = BCrypt.Net.BCrypt.HashPassword(providedPassword);

            // Compare the generated hash with the stored hash
            bool passwordMatch = VerifyBcryptPassword(providedPassword, hashedPassword);

            // Return the appropriate result based on the comparison
            return passwordMatch
                ? PasswordVerificationResult.Success
                : PasswordVerificationResult.Failed;
        }
        catch (SaltParseException)
        {
            // Handle exceptions related to salt parsing, if needed
            return PasswordVerificationResult.Failed;
        }
        catch (Exception)
        {
            // Handle other exceptions, if needed
            return PasswordVerificationResult.Failed;
        }
    }

    static bool VerifyBcryptPassword(string providedPassword, string hashedPassword)
    {
        try
        {
            // Use BCrypt to verify the hashed passwords
            return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
        }
        catch (SaltParseException)
        {
            // Handle exceptions related to salt parsing, if needed
            return false;
        }
        catch (Exception)
        {
            // Handle other exceptions, if needed
            return false;
        }
    }
    static string HashStringWithSHA512(string input)
    {
        using (SHA512 sha512 = SHA512.Create())
        {
            // Convert the input string to bytes
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            // Compute the hash
            byte[] hashBytes = sha512.ComputeHash(inputBytes);

            // Convert the hash bytes to a hexadecimal string
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte hashByte in hashBytes)
            {
                stringBuilder.Append(hashByte.ToString("X2"));
            }

            return stringBuilder.ToString().ToLower();
        }
    }

    static string HashStringWithSHA384(string input)
    {
        using (SHA384 sha384 = SHA384.Create())
        {
            // Convert the input string to bytes
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            // Compute the hash
            byte[] hashBytes = sha384.ComputeHash(inputBytes);

            // Convert the hash bytes to a hexadecimal string
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte hashByte in hashBytes)
            {
                stringBuilder.Append(hashByte.ToString("X2"));
            }

            return stringBuilder.ToString();
        }
    }

}
