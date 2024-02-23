namespace AspNetCoreHero.Boilerplate.Infrastructure.Shared.Security;

public interface IEncryptor
{
    public string Encrypt(string plainText);
    public string Decrypt(string plainText);
    public string Encrypt(string plainText, byte[] password);
    public string Encrypt(string plainText, string password);
    public string Decrypt(string plainText, byte[] password);
    public string Decrypt(string plainText, string password);
    public string GenerateKey();
}
public enum EncryptorType
{
    AesGcm = 1,
}
public interface IEncryptorFactory
{
    public EncryptorType EncryptorType { get; }
    public IEncryptor Create();
}
public class EncryptorFactory : IEncryptorFactory
{
    EncryptorType _type;
    public EncryptorType EncryptorType => _type;
    public EncryptorFactory(EncryptorType type)
    {
        _type = type;
    }

    public IEncryptor Create()
    {
        switch (_type)
        {
            case EncryptorType.AesGcm:
                return new AesGcmEncryptor();
            default:
                return new AesGcmEncryptor();
        }
    }
}
