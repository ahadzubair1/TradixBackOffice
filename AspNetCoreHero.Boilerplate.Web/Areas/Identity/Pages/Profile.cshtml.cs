using AspNetCoreHero.Boilerplate.Infrastructure.DbContexts;
using AspNetCoreHero.Boilerplate.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Web.Areas.Identity.Pages
{
    public class ReferralCodeModel
    {
        public string Code { get; set; }
        public string Position { get; set; }
    }
    public class ProfileModel : PageModel
    {
        public string Username { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; } = "";
        public string Country { get; set; }
        public byte[] ProfilePicture { get; set; }
        public bool IsActive { get; set; }
        public int ImageHeight { get; set; }
        public int ImageWidth { get; set; }
        public List<string> Roles { get; set; }
        public List<ReferralCodeModel> ReferralCodes { get; set; }

        public string LeftReferralCode { get; set; }
        public string RightReferralCode { get; set; }
        public string AutoReferralCode { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsSuperAdmin { get; set; }

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public ProfileModel(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task OnGetAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                UserId = userId;
                Username = user.UserName;
                ProfilePicture = user.ProfilePicture;
                FirstName = user.FirstName;
                LastName = user.LastName;
                IsActive = user.IsActive == 1;
                PhoneNumber = user.PhoneNumber;
                DateOfBirth = (user.DateOfBirth.HasValue ? user.DateOfBirth.Value.ToString("dd/MM/yyyy") : "");
                IsSuperAdmin = roles.Contains("SuperAdmin");
                Roles = roles.ToList();

                ReferralCodes = new List<ReferralCodeModel>();


                if(user.CountryId != null)
                {
                    var country = _dbContext.Countries.Where(x => x.Id.Equals(user.CountryId)).FirstOrDefault();

                    if (country != null)
                    {
                        Country = $"{country.Name} ({country.Alpha2Code})";
                    }
                }
                var referralCodes = _dbContext.ReferralCodes.Where(x => x.UserId == Guid.Parse(UserId)).ToList();

                foreach (var referralCode in referralCodes)
                {
                    ReferralCodes.Add(new ReferralCodeModel
                    {
                        Code = referralCode.Code,
                        Position = referralCode.NetworkPosition.ToString(),
                    });

                    if(referralCode.NetworkPosition == NetworkPosition.Left)
                    {
                        LeftReferralCode = referralCode.Code;
                    }
                    else if(referralCode.NetworkPosition == NetworkPosition.Right)
                    {
                        RightReferralCode = referralCode.Code;
                    }
                    else if (referralCode.NetworkPosition == NetworkPosition.Auto)
                    {
                        AutoReferralCode = referralCode.Code;
                    }
                }
            }
        }

        public async Task<IActionResult> OnPostActivateUserAsync(string userId)
        {
            if (User.IsInRole("SuperAdmin"))
            {
                var currentUser = await _userManager.FindByIdAsync(userId);
                currentUser.IsActive = 1;
                //currentUser.ActivatedBy = _userManager.GetUserAsync(HttpContext.User).Result.Id;
                await _userManager.UpdateAsync(currentUser);
                await OnGetAsync(userId);
                return RedirectToPage("Profile", new { area = "Identity", userId = userId });
            }
            else return default;
        }

        public async Task<IActionResult> OnPostDeActivateUserAsync(string userId)
        {
            if (User.IsInRole("SuperAdmin"))
            {
                var currentUser = await _userManager.FindByIdAsync(userId);
                currentUser.IsActive = 0;
                await _userManager.UpdateAsync(currentUser);
                return RedirectToPage("Profile", new { area = "Identity", userId = userId });
            }
            else return default;
        }
    }
}