﻿namespace AspNetCoreHero.Boilerplate.Infrastructure.Shared.Security;

public interface IEncryptionService
{
    /// <summary>
    /// Simple Decryption & Authentication (AES-GCM) of a UTF8 Message
    /// </summary>
    /// <param name="encryptedMessage">The encrypted message.</param>
    /// <param name="key">The base 64 encoded 256 bit key.</param>
    /// <returns>Decrypted Message</returns>
    string Decrypt(string encryptedMessage, string key);

    /// <summary>
    /// Simple Encryption And Authentication (AES-GCM) of a UTF8 string.
    /// </summary>
    /// <param name="messageToEncrypt">The string to be encrypted.</param>
    /// <param name="key">The base 64 encoded 256 bit key.</param>
    /// <returns>
    /// Encrypted Message
    /// </returns>
    /// <exception cref="System.ArgumentException">Secret Message Required!;secretMessage</exception>
    /// <remarks>
    /// Adds overhead of (Optional-Payload + BlockSize(16) + Message +  HMac-Tag(16)) * 1.33 Base64
    /// </remarks>
    string Encrypt(string messageToEncrypt, string key);

    /// <summary>
    /// Helper that generates a random new key on each call.
    /// </summary>
    /// <returns>Base 64 encoded string</returns>
    string NewKey();
}