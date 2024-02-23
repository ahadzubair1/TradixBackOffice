using AspNetCoreHero.Boilerplate.Application.DTOs.Identity;
using AspNetCoreHero.Results;
using System.Data;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces

{

    public interface IIdentityService
    {
        Task<Result<TokenResponse>> GetTokenAsync(TokenRequest request, string ipAddress);
        Task<Result<TokenResponse>> GetTokenAsync(SignInRequest request, string ipAddress);

        Task<Result<string>> RegisterAsync(RegisterRequest request, string origin);
        Task<Result<string>> RegisterAsync(AutoRegisterRequest request, string origin);

        Task<Result<string>> ConfirmEmailAsync(string userId, string code);

        Task ForgotPassword(ForgotPasswordRequest model, string origin);
        Task AssingNetworkAsync(string userId, string referredBy, NetworkPosition position, IDbTransaction transaction = null);
        Task AssingParentAsync(string userId, string parentId, NetworkPosition position, IDbTransaction transaction = null);
        Task ReassignNftId(string userId, string nftId);

        Task<Result<string>> ResetPassword(ResetPasswordRequest model);
        Task<UserDetailsDto> GetAsync(string userId, CancellationToken cancellationToken);
        Task ReassignReferralAsync(string userId, string referralCodeLeft, string referralCodeAuto, string referralCodeRight);
        string GenerateHash(string password);
        string GenerateBcrypt(string password);
        bool VerifyBcryptHash(string User512Hash, string hashedPassword);
        Task<Result<string>> ChangePassword(string userId, string oldpassword, string newpassword);
    }
}