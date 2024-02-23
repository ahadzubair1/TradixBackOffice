using AspNetCoreHero.Boilerplate.Application.DTOs.Settings;
using AspNetCoreHero.Boilerplate.Application.Features.ActivityLog.Commands.AddLog;
using AspNetCoreHero.Boilerplate.Application.Interfaces;
using AspNetCoreHero.Boilerplate.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Web.Areas.Identity.Pages.Account
{
    public class ReCaptchaResponse
    {
        public bool success
        {
            get;
            set;
        }
        public string challenge_ts
        {
            get;
            set;
        }
        public string hostname
        {
            get;
            set;
        }
        [JsonProperty(PropertyName = "error-codes")]
        public List<string> error_codes
        {
            get;
            set;
        }
    }

    [AllowAnonymous]
    public class LoginModel : BasePageModel<LoginModel>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IMediator _mediator;
        private readonly CaptchaSettings _captchaSettings;
        private readonly IIdentityService _identityService;

        public LoginModel(SignInManager<ApplicationUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<ApplicationUser> userManager, IMediator mediator, IOptions<CaptchaSettings> captchaSettings,IIdentityService identityService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _mediator = mediator;
            _captchaSettings = captchaSettings.Value;
            _identityService = identityService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }
        public string CaptchaSiteKey { get; set; }

        public class InputModel
        {
            //[Required]
            //[EmailAddress]
            //public string Email { get; set; }
            [Required]
            [MaxLength(50)]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            CaptchaSiteKey = _captchaSettings.SiteKey;
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(IFormCollection form,string returnUrl = null)
        {
            if (string.IsNullOrWhiteSpace(form["g-recaptcha-response"]))
            {
                //Throw error as required  
                _notyf.Error("Please perform Captcha");
                CaptchaSiteKey = _captchaSettings.SiteKey;
                ModelState.AddModelError(string.Empty, "Please perform Captcha");
                return Page();
            }
            //ReCaptchaResponse reCaptchaResponse = VerifyCaptcha("6Ld3LzUpAAAAAMzn_haTjX_4NeedVMQqmM4WlD27", form["g-recaptcha-response"]);
            ReCaptchaResponse reCaptchaResponse = VerifyCaptcha(_captchaSettings.Secret, form["g-recaptcha-response"]);
            if (!reCaptchaResponse.success)
            {
                _notyf.Error("Captcha Failed");
                CaptchaSiteKey = _captchaSettings.SiteKey;
                ModelState.AddModelError(string.Empty, "Captcha Failed");
                return Page();
            }


            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var userName = Input.Username;
                //if (IsValidEmail(Input.Username))
                //{
                //    var userCheck = await _userManager.FindByEmailAsync(Input.Username);
                //    if (userCheck != null)
                //    {
                //        userName = userCheck.UserName;
                //    }
                //}
                var user = await _userManager.FindByNameAsync(userName);
                var userbyemail = await _userManager.FindByEmailAsync(userName);
                if(userbyemail!=null)
                {
                    user = userbyemail;
                }
                


                if (user != null)
                {
                    if (user.IsActive==0)
                    {
                        return RedirectToPage("./Deactivated");
                    }
                    else if (!user.EmailConfirmed)
                    {
                        _notyf.Error("Email Not Confirmed.");
                        ModelState.AddModelError(string.Empty, "Email Not Confirmed.");
                        return Page();
                    }
                    else
                    {
                        string hashPassword = _identityService.GenerateHash(Input.Password);
                        bool verifyPassword = _identityService.VerifyBcryptHash(hashPassword, user.PasswordHashBcrypt);
                        // string bcryptPassword = _identityServiceGenerateBcrypt(hashPassword);

                        string userPassword = user.PlatformSource == PlatformSource.MiningRace ? hashPassword : Input.Password;

                        var result = await _signInManager.PasswordSignInAsync(user.UserName, userPassword, Input.RememberMe, lockoutOnFailure: false);
                        if (result.Succeeded)
                        {
                            await _mediator.Send(new AddActivityLogCommand() { userId = user.Id, Action = "Logged In" });
                            _logger.LogInformation("User logged in.");
                            _notyf.Success($"Logged in as {user.UserName}.");
                            return LocalRedirect(returnUrl);
                        }
                        await _mediator.Send(new AddActivityLogCommand() { userId = user.Id, Action = "Log-In Failed" });
                        if (result.RequiresTwoFactor)
                        {
                            return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                        }
                        if (result.IsLockedOut)
                        {
                            _notyf.Warning("User account locked out.");
                            _logger.LogWarning("User account locked out.");
                            return RedirectToPage("./Lockout");
                        }
                        else
                        {
                            _notyf.Error("Invalid login attempt.");
                            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                            CaptchaSiteKey = _captchaSettings.SiteKey;
                            return Page();
                        }
                    }
                }

                else
                {
                    _notyf.Error("Email / Username Not Found.");
                    ModelState.AddModelError(string.Empty, "Email / Username Not Found.");
                    CaptchaSiteKey = _captchaSettings.SiteKey;
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        public bool IsValidEmail(string emailaddress)
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

        public static ReCaptchaResponse VerifyCaptcha(string secret, string request)
        {
            ReCaptchaResponse response = null;
            if (request != null)
            {
                using (System.Net.Http.HttpClient hc = new System.Net.Http.HttpClient())
                {
                    var values = new Dictionary<string, string> {
                        {
                            "secret",
                            secret
                        },
                        {
                            "response",
                            request
                        }
                    };
                    var content = new System.Net.Http.FormUrlEncodedContent(values);
                    var Response = hc.PostAsync("https://www.google.com/recaptcha/api/siteverify", content).Result;
                    var responseString = Response.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrWhiteSpace(responseString))
                    {
                        response = JsonConvert.DeserializeObject<ReCaptchaResponse>(responseString);
                        return response;
                    }
                    else
                    { }
                    //Throw error as required  
                }
            }
            else
            { }
            //Throw error as required  
            return response;
        }
    }
}