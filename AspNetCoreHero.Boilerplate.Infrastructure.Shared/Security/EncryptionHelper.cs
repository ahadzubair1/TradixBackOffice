namespace AspNetCoreHero.Boilerplate.Infrastructure.Shared.Security;

public enum EncryptionState
{
    Decrypt = 1,
    Encrypt = 2,
}
public class EncryptionResult
{
    public EncryptorType EncryptorType { get; set; }
    public EncryptionState EncryptionState { get; set; }
    public string? Result { get; set; }
    public bool HasError { get; set; } // for future use
    public string? ErrorDescription { get; set; } // for future use
}
public class EncryptionHelper
{

    public static EncryptionResult Decrypt(string text, EncryptorType type = EncryptorType.AesGcm)
    {
        var enc = new EncryptorFactory(type).Create();
        var result = new EncryptionResult
        {
            EncryptionState = EncryptionState.Decrypt,
            EncryptorType = type
        };

        if (string.IsNullOrEmpty(text))
        {
            result.Result = string.Empty;
            return result;
        }

        result.Result = enc.Decrypt(text);

        return result;
    }
    public static EncryptionResult Encrypt(string text, EncryptorType type = EncryptorType.AesGcm)
    {
        EncryptionResult result = new EncryptionResult
        {
            EncryptionState = EncryptionState.Encrypt,
            EncryptorType = type,
        };

        var enc = new EncryptorFactory(type).Create();
        result.Result = enc.Encrypt(text);
        return result;
    }

}
