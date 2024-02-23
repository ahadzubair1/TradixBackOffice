using System;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity;

public class CustomPasswordHasher<TUser> : IPasswordHasher<TUser> where TUser : class
{
    public string HashPassword(TUser user, string password)
    {
        // Step 1: Hash with SHA-512
        string sha512Salt = GenerateRandomSalt();
        string sha512Hash = HashWithSHA512(password + sha512Salt);

        // Step 2: Hash with BCrypt
        string bcryptSalt = BCrypt.Net.BCrypt.GenerateSalt();
        return BCrypt.Net.BCrypt.HashPassword(sha512Hash, bcryptSalt);
    }

    public PasswordVerificationResult VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword)
    {
        // Verify BCrypt hash directly
        return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword)
            ? PasswordVerificationResult.Success
            : PasswordVerificationResult.Failed;
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
