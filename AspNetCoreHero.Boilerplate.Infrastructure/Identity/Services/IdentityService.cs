using AspNetCoreHero.Boilerplate.Application.DTOs.Identity;
using AspNetCoreHero.Boilerplate.Application.DTOs.Mail;
using AspNetCoreHero.Boilerplate.Application.DTOs.Settings;
using AspNetCoreHero.Boilerplate.Application.Enums;
using AspNetCoreHero.Boilerplate.Application.Exceptions;
using AspNetCoreHero.Boilerplate.Application.Features.Networks.Queries;
using AspNetCoreHero.Boilerplate.Application.Interfaces;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Shared;
using AspNetCoreHero.Boilerplate.Infrastructure.Identity.Models;
using AspNetCoreHero.Boilerplate.Infrastructure.Services;
using AspNetCoreHero.Results;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NanoidDotNet;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Generators;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;
using VM.WebApi.Domain.App;
using static AspNetCoreHero.Boilerplate.Application.Constants.Permissions;
using MassTransit;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Identity.Services
{

    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JWTSettings _jwtSettings;
        private readonly IDateTimeService _dateTimeService;
        private readonly IMailService _mailService;
        private readonly ApplicationDbContext _db;
        private readonly IMediator _mediator;
        private readonly ILogger<IdentityService> _logger;
        private readonly IReferralService _referralService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly MailSettings _mailSettings;
        private readonly IIntegrationService _integrationService;

        public IdentityService(IIntegrationService integrationService, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<JWTSettings> jwtSettings,
            IDateTimeService dateTimeService,
            SignInManager<ApplicationUser> signInManager,
            IMailService mailService,
            ApplicationDbContext db,
            IMediator mediator, ILogger<IdentityService> logger, IReferralService referralService, IUnitOfWork unitOfWork, IOptions<MailSettings> mailSettings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
            _dateTimeService = dateTimeService;
            _signInManager = signInManager;
            _mailService = mailService;
            _db = db;
            _mediator = mediator;
            _logger = logger;
            _referralService = referralService;
            _unitOfWork = unitOfWork;
            _mailSettings = mailSettings.Value;
            _integrationService = integrationService;
        }

        public async Task<Result<TokenResponse>> GetTokenAsync(TokenRequest request, string ipAddress)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            Throw.Exception.IfNull(user, nameof(user), $"No Accounts Registered with {request.Email}.");
            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            Throw.Exception.IfFalse(user.EmailConfirmed, $"Email is not confirmed for '{request.Email}'.");
            Throw.Exception.IfFalse(user.IsActive == 1, $"Account for '{request.Email}' is not active. Please contact the Administrator.");
            Throw.Exception.IfFalse(result.Succeeded, $"Invalid Credentials for '{request.Email}'.");
            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user, ipAddress);
            var response = new TokenResponse();
            response.Id = user.Id;
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            response.IssuedOn = jwtSecurityToken.ValidFrom.ToLocalTime();
            response.ExpiresOn = jwtSecurityToken.ValidTo.ToLocalTime();
            response.Email = user.Email;
            response.UserName = user.UserName;
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;
            var refreshToken = GenerateRefreshToken(ipAddress);
            response.RefreshToken = refreshToken.Token;
            return Result<TokenResponse>.Success(response, "Authenticated");
        }
        public async Task<Result<TokenResponse>> GetTokenAsync(SignInRequest request, string ipAddress)
        {
            var userName = request.Username;
            if (IsValidEmail(request.Username))
            {
                var userCheck = await _userManager.FindByEmailAsync(request.Username);
                if (userCheck != null)
                {
                    userName = userCheck.UserName;
                }
            }
            var user = await _userManager.FindByNameAsync(userName);
            Throw.Exception.IfNull(user, nameof(user), $"No Accounts Registered with {request.Username}.");
            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            Throw.Exception.IfFalse(user.EmailConfirmed, $"Email is not confirmed for '{request.Username}'.");
            Throw.Exception.IfFalse(user.IsActive==1, $"Account for '{request.Username}' is not active. Please contact the Administrator.");
            Throw.Exception.IfFalse(result.Succeeded, $"Invalid Credentials for '{request.Username}'.");
            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user, ipAddress);
            var response = new TokenResponse();
            response.Id = user.Id;
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            response.IssuedOn = jwtSecurityToken.ValidFrom.ToLocalTime();
            response.ExpiresOn = jwtSecurityToken.ValidTo.ToLocalTime();
            response.Email = user.Email;
            response.UserName = user.UserName;
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;
            var refreshToken = GenerateRefreshToken(ipAddress);
            response.RefreshToken = refreshToken.Token;
            return Result<TokenResponse>.Success(response, "Authenticated");
        }

        private bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user, string ipAddress)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
                new Claim("first_name", user.FirstName),
                new Claim("last_name", user.LastName),
                new Claim("full_name", $"{user.FirstName} {user.LastName}"),
                new Claim("ip", ipAddress)
            }
            .Union(userClaims)
            .Union(roleClaims);
            return JWTGeneration(claims);
        }

        private JwtSecurityToken JWTGeneration(IEnumerable<Claim> claims)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
        }

        //[OpenApiIgnore]
        public async Task<Result<string>> RegisterAsync(RegisterRequest request, string origin)
        {


            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                throw new AppException($"Username '{request.UserName}' is already taken.");
            }
            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName
            };
            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());
                    var verificationUri = await SendVerificationEmail(user, origin);
                    //TODO: Attach Email Service here and configure it via appsettings
                    //await _mailService.SendAsync(new MailRequest() { From = "mail@codewithmukesh.com", To = user.Email, Body = $"Please confirm your account by <a href='{verificationUri}'>clicking here</a>.", Subject = "Confirm Registration" });
                    return Result<string>.Success(user.Id, message: $"User Registered. Confirmation Mail has been delivered to your Mailbox. (DEV) Please confirm your account by visiting this URL {verificationUri}");
                }
                else
                {
                    throw new AppException($"{result.Errors}");
                }
            }
            else
            {
                throw new AppException($"Email {request.Email} is already registered.");
            }
        }
        public async Task<Result<string>> RegisterAsync(AutoRegisterRequest request, string origin)
        {


            var referralCode = _db.ReferralCodes.Where(x => x.Code.Equals(request.ReferredBy)).FirstOrDefault();

            if (referralCode == null)
            {
                throw new AppException($"Referral '{request.ReferredBy}' does not exists");
            }
            var id = referralCode.UserId.ToString();
            //await _userManager.FindByIdAsync(referralCode.UserId.ToString());

            var referredBy = await _userManager.FindByIdAsync(referralCode.UserId.ToString());//await _userManager.FindByNameAsync(request.Referral);
            if (referredBy == null)
            {
                //throw new CustomException($"Referred user {request.ReferredByUsername} does not exists");

                throw new AppException($"Referral {request.ReferredBy} does not exists");

            }
            var country = _db.Countries.Where(x => x.Alpha2Code.Equals(request.CountryCode)).FirstOrDefault();

            MailAddress address = new MailAddress(request.Email);

            string userName = request.UserName; //address.User;
            var platformSource = request.FromApi ? PlatformSource.MiningRace :PlatformSource.IconX ;
            
            var user =new ApplicationUser();
            if (request.FromApi)
            {
                user = new ApplicationUser
                {
                    UserName = userName,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    NormalizedUserName = userName.ToUpper(),
                    NormalizedEmail = request.Email.ToUpper(),
                    Position = NetworkPosition.Right,
                    DefaultDownlinePlacementPosition = NetworkPosition.Right,
                    ReferredBy = Guid.Parse(referredBy.Id),
                    EmailConfirmed = request.FromApi,
                    IsActive = 1,
                    //PasswordHash512 = hashPassword,
                    PasswordHashBcrypt = request.PasswordHashBcrypt,
                    CountryId = country.Id,
                    NftId= userName,
                    PlatformSource = platformSource,

                };
            }
            else
            {
                var hashPassword = GenerateHash(request.Password) ;
                string bcryptPassword = GenerateBcrypt(hashPassword);

                user = new ApplicationUser
                {
                    UserName = userName,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    NormalizedUserName = userName.ToUpper(),
                    NormalizedEmail = request.Email.ToUpper(),
                    Position = NetworkPosition.Right,
                    DefaultDownlinePlacementPosition = NetworkPosition.Right,
                    ReferredBy = Guid.Parse(referredBy.Id),
                    EmailConfirmed = request.FromApi,
                    IsActive = 1,
                    PasswordHash512 = hashPassword,// We could use this field if require later
                    PasswordHashBcrypt = bcryptPassword,
                    CountryId = country.Id,
                    PlatformSource = platformSource,

                };

            }
           

            //_unitOfWork.BeginTransaction();
            var result = await _userManager.CreateAsync(user, request.FromApi?"M0n1t0r!" :request.Password);// Dirty Hack update it later
            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");
                await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));


                await AssingWalletsAsync(user.Id);
                NetworkPosition position = referralCode.NetworkPosition == NetworkPosition.None ? referredBy.DefaultDownlinePlacementPosition : referralCode.NetworkPosition;





                await AssingNetworkAsync(user.Id, referredBy.Id, position);
                await AssingReferralCodeAsync(user.Id);

                await _db.SaveChangesAsync();
                if (!request.FromApi)
                {
                    var verificationUri = await SendVerificationEmail(user, origin);
                    var mailRequest = new MailRequest()
                    {
                        From = _mailSettings.From,
                        To = user.Email,
                        Body = $@"<div> <br class=''><div class=''><div class=''><table align='center' border='0' cellpadding='0' cellspacing='0' style='max-width:800px;margin:0 auto' width='100%'><tbody><tr><td  style='border: 4px solid #4324ad;'><table border='0' cellpadding='25' cellspacing='0' width='100%'><tbody style='background: #f0f8ff;'><tr><td colspan='2'  style='padding-top:5.25%;padding-right:5.25%;padding-bottom:5.25%;padding-left:5.25%' align='left'><p style='margin:0 0 0 0'><img alt='IconX' border='0'  src='https://app.iconx.network/images/logo-v-dark.png' style='width:150px'></p></td></tr><tr><td colspan='2' style='font-weight:700;font-size: 25px;color:#4a26a0;font-family:open sans,Calibri,Tahoma,sans-serif;' align='center'>Activate Your Account</td></tr><tr><td colspan='2'  style=''><p style='font-family:open sans,Calibri,Tahoma,sans-serif;color:#606060;font-size:18px;font-weight:400;line-height:1;margin:0 0 20px 0'>Dear {user.FirstName},</p><p style='font-family:open sans,Calibri,Tahoma,sans-serif;color:#606060;font-size:18px;font-weight:400;line-height:1;margin:0 0 20px 0'>This is your activation link:</p></td></tr><tr><td colspan='2' align='center' style='padding:0'><p style='font-family:open sans,Calibri,Tahoma,sans-serif;color:#606060;margin:0 0 40px 0;font-size:18px;font-weight:400;'><a href='{verificationUri}'  style='border-radius:8px;background:#4324ad;padding:10px 20px;color: #fff;text-decoration: none;font-family: open sans,Calibri,Tahoma,sans-serif;font-size:20px;margin:0 0 30px 0;'>Open Live Account</a></p></td></tr><tr><td><p style='font-family:open sans,Calibri,Tahoma,sans-serif;color:#606060;font-size:18px;line-height:1.5;font-weight:400;'>You are receiving this email because you have made an action on Icon X.</p><p style='font-family:open sans,Calibri,Tahoma,sans-serif;color:#606060;font-size:18px;line-height:1.5;font-weight:400;'>If you believe this mail has been sent to you by mistake, please contact us here, or email us at <a href='support@iconx.network' style='color:#4324ad'>support@iconx.network</a>.</p><p style='font-family:open sans,Calibri,Tahoma,sans-serif;color:#606060;font-size:18px;line-height:1.5;margin:30px 0 10px 0;font-weight:400;'>Sincerely,</p><p style='font-family:open sans,Calibri,Tahoma,sans-serif;color:#606060;font-size:18px;line-height:1.5;margin:0 0 10px 0;font-weight:400;'>Team IconX.</p><hr style='border:0;border-top:1px solid #c7c7c7;line-height:1px;margin:25px 0 20px 0'><p style='font-family:open sans,Calibri,Tahoma,sans-serif;color:#6a7070;font-size:12px;line-height:1.33;margin:0'> © IConX</p></td></tr></tbody></table><td></tr></tbody></table></div></div></div><p>&nbsp;</p>",
                        Subject = "Confirm Registration"
                    };
                    await _mailService.SendAsync(mailRequest);
                }

                //TODO: Attach Email Service here and configure it via appsettings

                var message = "User Registered. Confirmation Mail has been delivered to your Mailbox. (DEV) Please confirm your account by visiting this URL";
                if (request.FromApi)
                {
                    message = "User Registered Successfully";
                }
                return Result<string>.Success(user.Id, message: message);

            }
            else
            {
                StringBuilder sb = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    sb.AppendLine(string.Format("* {0}", error.Description));
                }
                //return Result<string>.Fail(sb.ToString());

                throw new AppException(sb.ToString());
            }

            //try
            //{


            //}
            //catch (Exception ex)
            //{
            //    //_unitOfWork.Rollback();
            //    throw;
            //}
        }
        private async Task AssingWalletsAsync(string userId)
        {
            var wallets = _db.WalletTypes.Where(x => x.AssignOnCreate == true).ToList();
            foreach (var wallet in wallets)
            {
                await _db.Wallets.AddAsync(new Wallet
                {
                    UserId = Guid.Parse(userId),
                    WalletTypeId = wallet.Id,
                    Balance = 0
                });
            }
        }
        private async Task AssingReferralCodeAsync(string userId)
        {

            await _db.ReferralCodes.AddAsync(new ReferralCode
            {
                UserId = Guid.Parse(userId),
                Code = Nanoid.Generate(),
                NetworkPosition = NetworkPosition.Left
            });

            await _db.ReferralCodes.AddAsync(new ReferralCode
            {
                UserId = Guid.Parse(userId),
                Code = Nanoid.Generate(),
                NetworkPosition = NetworkPosition.Right
            });

            await _db.ReferralCodes.AddAsync(new ReferralCode
            {
                UserId = Guid.Parse(userId),
                Code = Nanoid.Generate(),
                NetworkPosition = NetworkPosition.Auto
            });
        }
        public async Task AssingNetworkAsync(string userId, string referredBy, NetworkPosition position, IDbTransaction transaction = null)
        {

            if (!_db.Networks.Any(x => x.UserId == Guid.Parse(userId)))
            {
                var network = new Network
                {
                    Position = position,
                    UserId = Guid.Parse(userId),
                    //ParentUserId = addParentNode ? res.UserId : Guid.Empty,
                    ReferredBy = Guid.Parse(referredBy)
                };

                var addedEntity = await _db.Networks.AddAsync(network);
                _db.SaveChanges();
            }
        }
        private async Task<string> SendVerificationEmail(ApplicationUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "Identity/Account/ConfirmEmail/";
            var _enpointUri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "code", code);
            //Email Service Call Here
            return verificationUri;
        }
        public async Task<Result<string>> ConfirmEmailAsync(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                long currentTimestampMillis = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                //var userData = new UserSignUpRequest { firstName=user.FirstName,lastName=user.LastName, email =user.Email,userName=user.UserName,password=user.PasswordHash512,confirmPassword=user.PasswordHash512,referredBy= "user.referredBy", countryCode="AE", createdAt = currentTimestampMillis };
                //var sendmining = await _integrationService.PerformSignUp(userData);
                return Result<string>.Success(user.Id, message: $"Account Confirmed for {user.Email}. You can now use the /api/identity/token endpoint to generate JWT.");
            }
            else
            {
                throw new AppException($"An error occured while confirming {user.Email}.");
            }
        }
        public async Task ForgotPassword(ForgotPasswordRequest model, string origin)
        {
            var account = await _userManager.FindByEmailAsync(model.Email);

            // always return ok response to prevent email enumeration
            if (account == null) return;

            var code = await _userManager.GeneratePasswordResetTokenAsync(account);
            var route = "api/identity/reset-password/";
            var _enpointUri = new Uri(string.Concat($"{origin}/", route));
            var emailRequest = new MailRequest()
            {
                Body = $"You reset token is - {code}",
                To = model.Email,
                Subject = "Reset Password",
            };
            //await _mailService.SendAsync(emailRequest);
        }
        public async Task<Result<string>> ResetPassword(ResetPasswordRequest model)
        {
            var account = await _userManager.FindByEmailAsync(model.Email);
            if (account == null) throw new AppException($"No Accounts Registered with {model.Email}.");
            var result = await _userManager.ResetPasswordAsync(account, model.Token, model.Password);
            if (result.Succeeded)
            {
                return Result<string>.Success(model.Email, message: $"Password Resetted.");
            }
            else
            {
                throw new AppException($"Error occured while reseting the password.");
            }
        }


        public async Task<UserDetailsDto> GetAsync(string userId, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
                .AsNoTracking()
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync(cancellationToken);

            _ = user ?? throw new AppException($"UserId {userId} does not exists");

            return user.Adapt<UserDetailsDto>();
        }

        public string GenerateHash(string text)
        {
            //var hash = string.Empty;
            byte[] data = Encoding.UTF8.GetBytes(text);
            string hashedString = string.Empty;
            // Create a new instance of SHA-512
            using (SHA512 sha512 = SHA512.Create())
            {
                // Compute the hash value from the input data
                byte[] hash = sha512.ComputeHash(data);

                // Convert the byte array to a hexadecimal string
                hashedString = BitConverter.ToString(hash).Replace("-", "").ToLower();

            }
            return hashedString;
        }
        public string GenerateBcrypt(string hashedString)
        {
            string bcryptHash = BCrypt.Net.BCrypt.HashPassword(hashedString, BCrypt.Net.BCrypt.GenerateSalt(10));
            return bcryptHash;
        }

        public bool VerifyBcryptHash(string User512Hash, string hashedPassword)
        {
            // Verify the bcrypt hash
            return BCrypt.Net.BCrypt.Verify(User512Hash, hashedPassword);
        }

        public async Task ReassignReferralAsync(string userId, string referralCodeLeft, string referralCodeAuto, string referralCodeRight)
        {


            foreach (NetworkPosition position in Enum.GetValues(typeof(NetworkPosition)))
            {
                // Retrieve the referral code to update
                var referralCodeToUpdate = _db.ReferralCodes
                    .FirstOrDefault(x => x.UserId == Guid.Parse(userId) && x.NetworkPosition == position);

                // Check if the referral code is found
                if (referralCodeToUpdate != null)
                {
                    var codeExists = _db.ReferralCodes.Any(x => x.Code == referralCodeAuto && x.Id != referralCodeToUpdate.Id);
                    // Update the referral code based on the position
                    if (!codeExists)
                    {
                        switch (position)
                        {
                            case NetworkPosition.Left:
                                referralCodeToUpdate.Code = referralCodeLeft;
                                break;
                            case NetworkPosition.Auto:
                                referralCodeToUpdate.Code = referralCodeAuto;
                                break;
                            case NetworkPosition.Right:
                                referralCodeToUpdate.Code = referralCodeRight;
                                break;
                        }

                        // Mark the entity as modified
                        _db.Entry(referralCodeToUpdate).State = EntityState.Modified;

                        // Save changes to the database
                        _db.SaveChanges();
                    }
                    else
                    {
                        throw new InvalidOperationException("Referral code already exists in the database.");
                    }
                }
            }

        }

        public async Task AssingParentAsync(string userId, string referredBy, NetworkPosition position, IDbTransaction transaction = null)
        {
            // get network of user
            // update parentid in record
            // save it in db
            if (position == NetworkPosition.Auto)
            {
                //find left
                var leftDownline = await _mediator.Send(new GetParentUserViaDapperRequest(Guid.Parse(referredBy), NetworkPosition.Left, transaction));

                //find right
                var rightDownline = await _mediator.Send(new GetParentUserViaDapperRequest(Guid.Parse(referredBy), NetworkPosition.Right, transaction));

                if (leftDownline != null && rightDownline != null)
                {
                    if (leftDownline.Level < rightDownline.Level)
                    {
                        position = NetworkPosition.Left;
                    }
                    else if (leftDownline.Level == rightDownline.Level)
                    {
                        position = NetworkPosition.Left;
                    }
                    else
                    {
                        position = NetworkPosition.Right;
                    }
                }
                else
                {
                    position = NetworkPosition.Left;
                }
            }
            /*check if user already has a downline child attached to the network position, iterate to next level
             unless in the same position unless no further child of the child exists */
            var parentUser = await _mediator.Send(new GetParentUserViaDapperRequest(Guid.Parse(referredBy), position, transaction));

            var network = await _db.Networks.FirstOrDefaultAsync(x => x.UserId == Guid.Parse(userId));
            network.ParentUserId = parentUser.UserId;
            network.Position = position;
            await _db.SaveChangesAsync();
            //var net = _mediator.Send(new GetUserNetworkViaDapperRequest(Guid.Parse(userId)));
            //var wouldBeParent = await _db.UserNetworkTrees.Where()

        }
        public async Task ReassignNftId(string userId, string nftId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.NftId = nftId;
                _db.Update(user); // Track the entity changes
                await _db.SaveChangesAsync();
            }

        }

        public async Task<Result<string>> ChangePassword(string userId, string oldpassword, string newpassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var changePasswordResult = await _userManager.ChangePasswordAsync(user, oldpassword, newpassword);
            if (changePasswordResult.Succeeded)
            {
                string hashPassword = GenerateHash(newpassword);
                user.PasswordHashBcrypt = GenerateBcrypt(hashPassword);

                await _userManager.UpdateAsync(user);
                return Result<string>.Success(user.Id, message: "Password successfully updated");
            }
            else
            {
                return Result<string>.Fail(message: $"User with '{userId}' doest not exists.");
            }
        }
    }
}