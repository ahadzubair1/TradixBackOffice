using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Text;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Shared.Security;
/// <summary>
/// The block cipher engine
/// </summary>
public class BlockCipherEngine
{
    /// <summary>
    /// The encoding.
    /// </summary>
    private readonly Encoding _encoding;

    /// <summary>
    /// The block cipher.
    /// </summary>
    private readonly IBlockCipher _blockCipher;

    /// <summary>
    /// The buffered block cipher
    /// </summary>
    private PaddedBufferedBlockCipher _bufferedBlockCipher;

    public BlockCipherEngine(IBlockCipher blockCipher, Encoding encoding)
    {
        _blockCipher = blockCipher;
        _encoding = encoding;
    }

    /// <summary>
    /// Encrypts the specified plain.
    /// </summary>
    /// <param name="textToEncrypt">The text to encrypt.</param>
    /// <param name="key">The key.</param>
    /// <returns>Encrypted string.</returns>
    public string Encrypt(string textToEncrypt, string key)
    {
        byte[] result = BouncyCastleCrypto(true, _encoding.GetBytes(textToEncrypt), key);
        return Convert.ToBase64String(result);
    }

    /// <summary>
    /// Decrypts the specified cipher.
    /// </summary>
    /// <param name="textToDecrypt">The text to decrypt.</param>
    /// <param name="key">The key.</param>
    /// <returns>Decrypted string.</returns>
    public string Decrypt(string textToDecrypt, string key)
    {
        byte[] result = BouncyCastleCrypto(false, Convert.FromBase64String(textToDecrypt), key);
        return _encoding.GetString(result);
    }

    /// <summary>
    /// Bouncies the castle crypto.
    /// </summary>
    /// <param name="forEncrypt">if set to <c>true</c> [for encrypt].</param>
    /// <param name="input">The input.</param>
    /// <param name="key">The key.</param>
    /// <returns>Byte.</returns>
    /// <exception cref="CryptoException"></exception>
    private byte[] BouncyCastleCrypto(bool forEncrypt, byte[] input, string key)
    {
        try
        {
            _bufferedBlockCipher = new PaddedBufferedBlockCipher(_blockCipher, new Pkcs7Padding());

            _bufferedBlockCipher.Init(forEncrypt, new KeyParameter(_encoding.GetBytes(key)));

            return _bufferedBlockCipher.DoFinal(input);
        }
        catch (CryptoException ex)
        {
            throw new CryptoException($"Exception during Encryption/Decryption - error: {ex.Message}");
        }
    }
}