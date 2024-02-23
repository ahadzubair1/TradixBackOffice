using Newtonsoft.Json;
using System.Text;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Services;

public interface IReferralService
{
    Task<string> GenerateReferralCodeAsync(string username, string email, NetworkPosition position);
    Task<string> GenerateReferralCodeAsync(ReferCode referral);
    Task<ReferCode> VerifyReferralCodeAsync(string code);
}
public class ReferCode
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public NetworkPosition Position { get; set; }
}
public class ReferralService : IReferralService
{
    public async Task<string> GenerateReferralCodeAsync(string username, string email, NetworkPosition position)
    {
        var referral = new ReferCode
        {
            Email = email,
            Position = position,
            UserName = username,
        };
        return await GenerateReferralCodeAsync(referral);
    }
    public async Task<string> GenerateReferralCodeAsync(ReferCode referral)
    {
        string json = JsonConvert.SerializeObject(referral);
        var data = Encoding.UTF8.GetBytes(json);
        var code = Convert.ToBase64String(data);
        return code;
    }

    public async Task<ReferCode> VerifyReferralCodeAsync(string code)
    {
        var data = Convert.FromBase64String(code);
        string json = Encoding.UTF8.GetString(data);
        ReferCode referral = JsonConvert.DeserializeObject<ReferCode>(json);

        return referral;
    }


}