using AspNetCoreHero.Boilerplate.Application.DTOs.Identity;
using AspNetCoreHero.Boilerplate.Application.DTOs.Settings;
using AspNetCoreHero.Boilerplate.Application.Enums;
using AspNetCoreHero.Boilerplate.Application.Features.Brands.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Features.Countries.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Features.Networks.Queries;
using AspNetCoreHero.Boilerplate.Application.Interfaces;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Shared;
using AspNetCoreHero.Boilerplate.Infrastructure.Attributes;
using AspNetCoreHero.Boilerplate.Infrastructure.DbContexts;
using AspNetCoreHero.Boilerplate.Infrastructure.Identity.Models;
using AspNetCoreHero.Boilerplate.Web.Abstractions;
using AspNetCoreHero.Boilerplate.Web.Areas.App.Models;
using Mapster;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : BasePageModel<RegisterModel>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IMailService _emailSender;
        private readonly ApplicationDbContext _db;
        private readonly IIdentityService _identityService;
        private readonly CaptchaSettings _captchaSettings;

        private readonly IMediator _mediator;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IMailService emailSender,
            ApplicationDbContext db,
            IMediator mediator, IIdentityService identityService, IOptions<CaptchaSettings> captchaSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _db = db;
            _mediator = mediator;
            _identityService = identityService;
            _captchaSettings = captchaSettings.Value;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public SelectList Countries { get; set; }
        public string CaptchaSiteKey { get; private set; }
        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        
        public class InputModel
        {
            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]

            [Display(Name = "Username")]
            [StringLength(20, MinimumLength = 4, ErrorMessage = "Username must be between 4 and 20 characters.")]
            public string Username { get; set; }
            [Required]
            [MaxLength(250)]
            [Display(Name = "Referral")]
            public string Referral { get; set; }

            [Required]
            [EmailAddress]
            [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(1000, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public string CountryId { get; set; }
            public SelectList Countries { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            CaptchaSiteKey = _captchaSettings.SiteKey;
            await PopulateCountriesAsync();



        }

        private async Task PopulateCountriesAsync()
        {
            var countryList = await _mediator.Send(new GetAllCountriesQuery());

            if (countryList.Succeeded)
            {
                var viewModel = countryList.Data.Adapt<List<CountryViewModel>>();
                Countries = new SelectList(viewModel, nameof(CountryViewModel.Alpha2Code), nameof(CountryViewModel.Name), null, null);
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
        private async Task AssingNetworkAsync(string userId, string referredBy, NetworkPosition position)
        {
            /*check if user already has a downline child attached to the network position, iterate to next level
             unless in the same position unless no further child of the child exists */
            var res = await _mediator.Send(new GetParentUserViaDapperRequest(Guid.Parse(referredBy), position));
            //var net = _mediator.Send(new GetUserNetworkViaDapperRequest(Guid.Parse(userId)));
            //var wouldBeParent = await _db.UserNetworkTrees.Where()

            if (res != null)
            {
                var network = new Network
                {
                    Position = position,
                    UserId = Guid.Parse(userId),
                    ParentUserId = res.UserId,
                    ReferredBy = Guid.Parse(referredBy)
                };

                var addedEntity = await _db.AddAsync(network);
                _notyf.Information(string.Format("User registered"));
            }
            else
            {
                _notyf.Error("Parent Username Not Found.");
                ModelState.AddModelError(string.Empty, "Parent Username Not Found.");
            }

        }
        public async Task<IActionResult> OnPostAsync(IFormCollection form, string returnUrl = null)
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
                CaptchaSiteKey = _captchaSettings.SiteKey;
                return Page();
            }

            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();


            if (ModelState.IsValid)
            {
                try
                {
                    var origin = Request.Headers["origin"];
                    var res = await _identityService.RegisterAsync(new AutoRegisterRequest
                    {
                        Email = Input.Email,
                        FirstName = Input.FirstName,
                        LastName = Input.LastName,
                        Password = Input.Password,
                        ReferredBy = Input.Referral,
                        UserName = Input.Username,
                        CountryCode = Input.CountryId,
                    }, origin);

                    return LocalRedirect(returnUrl);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, string.Format(ex.Message));
                    CaptchaSiteKey = _captchaSettings.SiteKey;
                }
                // get referral user


            }

            await PopulateCountriesAsync();
            // If we got this far, something failed, redisplay form
            return Page();
        }
        
    }
}