namespace AspNetCoreHero.Boilerplate.Infrastructure.Shared.Security;
#region

using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.IO;
using System.Text;

#endregion

/// <summary>
/// Code taken from this Stack question: 
/// http://codereview.stackexchange.com/questions/14892/review-of-simplified-secure-encryption-of-a-string
/// 
/// The below code uses AES GCM using a 256bit key.
/// 
/// A non secret payload byte[] can be provided as well that won't be encrypted but will be authenticated with GCM.
/// </summary>
public class EncryptionService : IEncryptionService
{
    #region Constants and Fields

    private const int DEFAULT_KEY_BIT_SIZE = 256;
    private const int DEFAULT_MAC_BIT_SIZE = 128;
    private const int DEFAULT_NONCE_BIT_SIZE = 128;

    private readonly int _keySize;
    private readonly int _macSize;
    private readonly int _nonceSize;

    private readonly SecureRandom _random;

    #endregion

    #region Constructors and Destructors

    public EncryptionService()
        : this(DEFAULT_KEY_BIT_SIZE, DEFAULT_MAC_BIT_SIZE, DEFAULT_NONCE_BIT_SIZE)
    { }

    public EncryptionService(int keyBitSize, int macBitSize, int nonceBitSize)
    {
        _random = new SecureRandom();

        _keySize = keyBitSize;
        _macSize = macBitSize;
        _nonceSize = nonceBitSize;
    }

    #endregion

    #region Public Methods and Operators
    /// <summary>
    /// Simple Decryption & Authentication (AES-GCM) of a UTF8 Message
    /// </summary>
    /// <param name="encryptedText">The encrypted message.</param>
    /// <param name="key">The base 64 encoded 256 bit key.</param>
    /// <returns>Decrypted Message</returns>
    public string Decrypt(string encryptedText, byte[] key)
    {
        if (string.IsNullOrEmpty(encryptedText))
        {
            throw new ArgumentException("Encrypted Message Required!", "encryptedText");
        }

        var decodedKey = key;

        var cipherText = Convert.FromBase64String(encryptedText);

        var plaintext = Decrypt(cipherText, decodedKey);

        return Encoding.UTF8.GetString(plaintext);
    }

    /// <summary>
    /// Simple Decryption & Authentication (AES-GCM) of a UTF8 Message
    /// </summary>
    /// <param name="encryptedText">The encrypted message.</param>
    /// <param name="key">The base 64 encoded 256 bit key.</param>
    /// <returns>Decrypted Message</returns>
    public string Decrypt(string encryptedText, string key)
    {
        if (string.IsNullOrEmpty(encryptedText))
        {
            throw new ArgumentException("Encrypted Message Required!", "encryptedText");
        }

        var decodedKey = Convert.FromBase64String(key);

        var cipherText = Convert.FromBase64String(encryptedText);

        var plaintext = Decrypt(cipherText, decodedKey);

        return Encoding.UTF8.GetString(plaintext);
    }

    /// <summary>
    /// Simple Encryption And Authentication (AES-GCM) of a UTF8 string.
    /// </summary>
    /// <param name="text">The string to be encrypted.</param>
    /// <param name="key">The base 64 encoded 256 bit key.</param>
    /// <returns>
    /// Encrypted Message
    /// </returns>
    /// <exception cref="System.ArgumentException">Secret Message Required!;secretMessage</exception>
    /// <remarks>
    /// Adds overhead of (Optional-Payload + BlockSize(16) + Message +  HMac-Tag(16)) * 1.33 Base64
    /// </remarks>
    public string Encrypt(string text, byte[] key)
    {
        if (string.IsNullOrEmpty(text))
        {
            throw new ArgumentException("Message Required!", "text");
        }

        var decodedKey = key;

        var plainText = Encoding.UTF8.GetBytes(text);
        var cipherText = Encrypt(plainText, decodedKey);
        return Convert.ToBase64String(cipherText);
    }

    /// <summary>
    /// Simple Encryption And Authentication (AES-GCM) of a UTF8 string.
    /// </summary>
    /// <param name="text">The string to be encrypted.</param>
    /// <param name="key">The base 64 encoded 256 bit key.</param>
    /// <param name="nonSecretPayload">Optional non-secret payload.</param>
    /// <returns>
    /// Encrypted Message
    /// </returns>
    /// <exception cref="System.ArgumentException">Secret Message Required!;secretMessage</exception>
    /// <remarks>
    /// Adds overhead of (Optional-Payload + BlockSize(16) + Message +  HMac-Tag(16)) * 1.33 Base64
    /// </remarks>
    public string Encrypt(string text, string key)
    {
        if (string.IsNullOrEmpty(text))
        {
            throw new ArgumentException("Message Required!", "text");
        }

        var decodedKey = Convert.FromBase64String(key);

        var plainText = Encoding.UTF8.GetBytes(text);
        var cipherText = Encrypt(plainText, decodedKey);
        return Convert.ToBase64String(cipherText);
    }

    /// <summary>
    /// Helper that generates a random new key on each call.
    /// </summary>
    /// <returns>Base 64 encoded string</returns>
    public string NewKey()
    {
        var key = new byte[_keySize / 8];
        _random.NextBytes(key);
        return Convert.ToBase64String(key);
    }

    #endregion

    #region Methods

    public byte[] Decrypt(byte[] encryptedText, byte[] key)
    {
        //User Error Checks
        CheckKey(key);

        if (encryptedText == null || encryptedText.Length == 0)
        {
            throw new ArgumentException("Encrypted Message Required!", "encryptedText");
        }

        using (var cipherStream = new MemoryStream(encryptedText))
        using (var cipherReader = new BinaryReader(cipherStream))
        {

            //Grab Nonce
            var nonce = cipherReader.ReadBytes(_nonceSize / 8);

            var cipher = new GcmBlockCipher(new AesEngine());
            var parameters = new AeadParameters(new KeyParameter(key), _macSize, nonce);
            cipher.Init(false, parameters);

            //Decrypt Cipher Text
            var cipherText = cipherReader.ReadBytes(encryptedText.Length - nonce.Length);
            var plainText = new byte[cipher.GetOutputSize(cipherText.Length)];

            var len = cipher.ProcessBytes(cipherText, 0, cipherText.Length, plainText, 0);
            cipher.DoFinal(plainText, len);

            return plainText;
        }
    }

    public byte[] Encrypt(byte[] text, byte[] key)
    {
        //User Error Checks
        CheckKey(key);
        ;

        //Using random nonce large enough not to repeat
        var nonce = new byte[_nonceSize / 8];
        _random.NextBytes(nonce, 0, nonce.Length);

        var cipher = new GcmBlockCipher(new AesEngine());
        var parameters = new AeadParameters(new KeyParameter(key), _macSize, nonce);
        cipher.Init(true, parameters);

        //Generate Cipher Text With Auth Tag
        var cipherText = new byte[cipher.GetOutputSize(text.Length)];
        var len = cipher.ProcessBytes(text, 0, text.Length, cipherText, 0);
        cipher.DoFinal(cipherText, len);

        //Assemble Message
        using (var combinedStream = new MemoryStream())
        {
            using (var binaryWriter = new BinaryWriter(combinedStream))
            {
                //Prepend Nonce
                binaryWriter.Write(nonce);
                //Write Cipher Text
                binaryWriter.Write(cipherText);
            }
            return combinedStream.ToArray();
        }
    }

    private void CheckKey(byte[] key)
    {
        if (key == null || key.Length != _keySize / 8)
        {
            throw new ArgumentException(String.Format("Key needs to be {0} bit! actual:{1}", _keySize, key?.Length * 8), "key");
        }
    }

    #endregion
}