using AspNetCoreHero.Boilerplate.Application.DTOs.Identity;
using AspNetCoreHero.Results;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Application.Interfaces

{

    public interface IIntegrationService
    {
        Task<UserSignInResponse> PerformSignIn(UserSignInRequest request);
        Task<UserSignUpResponse> PerformSignUp(UserSignUpRequest request);
    }
}