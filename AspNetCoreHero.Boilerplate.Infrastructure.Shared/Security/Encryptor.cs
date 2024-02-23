using Org.BouncyCastle.Crypto.Engines;
using System.Text;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Shared.Security;

public class Encryptor
{
    private const string password = "NdRgUkXn2r5u8xAA";

    public static string Encrypt(string plainText)
    {
        BlockCipherEngine blockCipherEngine = new BlockCipherEngine(new AesEngine(), Encoding.ASCII);

        return blockCipherEngine.Encrypt(plainText, password);
    }
    public static string Decrypt(string plainText)
    {
        BlockCipherEngine blockCipherEngine = new BlockCipherEngine(new AesEngine(), Encoding.ASCII);
        return blockCipherEngine.Decrypt(plainText, password);
    }
    public static string Encrypt(string plainText, string password)
    {
        BlockCipherEngine blockCipherEngine = new BlockCipherEngine(new AesEngine(), Encoding.ASCII);

        return blockCipherEngine.Encrypt(plainText, password);
    }
    public static string Decrypt(string plainText, string password)
    {
        BlockCipherEngine blockCipherEngine = new BlockCipherEngine(new AesEngine(), Encoding.ASCII);
        return blockCipherEngine.Decrypt(plainText, password);
    }
}
