namespace AspNetCoreHero.Boilerplate.Infrastructure.Shared.Security;

public class AesGcmEncryptor : IEncryptor
{
    //private const string password = "NdRgUkXn2r5u8xAA";
    private static byte[] password = { 123, 217, 19, 11, 24, 26, 85, 45, 114, 184, 27, 162, 37, 112, 222, 209, 241, 24, 175, 144, 173, 53, 196, 29, 24, 26, 17, 218, 131, 236, 53, 209 };
    public string Encrypt(string plainText)
    {
        EncryptionService encryptor = new EncryptionService();

        return encryptor.Encrypt(plainText, password);
    }
    public string Decrypt(string plainText)
    {
        EncryptionService encryptor = new EncryptionService();
        return encryptor.Decrypt(plainText, password);
    }
    public string Encrypt(string plainText, byte[] password)
    {

        EncryptionService encryptor = new EncryptionService();
        return encryptor.Encrypt(plainText, password);

    }
    public string Encrypt(string plainText, string password)
    {

        EncryptionService encryptor = new EncryptionService();
        return encryptor.Encrypt(plainText, password);

    }
    public string Decrypt(string plainText, byte[] password)
    {
        EncryptionService encryptor = new EncryptionService();
        return encryptor.Decrypt(plainText, password);
    }
    public string Decrypt(string plainText, string password)
    {
        EncryptionService encryptor = new EncryptionService();
        return encryptor.Decrypt(plainText, password);
    }
    public string GenerateKey()
    {
        EncryptionService encryptor = new EncryptionService();
        return encryptor.NewKey();
    }
}
