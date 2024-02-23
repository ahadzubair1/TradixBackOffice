using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using VM.WebApi.Domain.App;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [MaxLength]
        public byte[] ProfilePicture { get; set; }

        public short IsActive { get; set; } = 0; // Match [smallint] in SQL Server

        public NetworkPosition Position { get; set; }
        public NetworkPosition DefaultDownlinePlacementPosition { get; set; }

        public DefaultIdType? NetworkId { get; set; }
        public virtual DefaultIdType? KycId { get; set; }
        public DefaultIdType? CountryId { get; set; }
        public DefaultIdType? ReferredBy { get; set; }

        public virtual Network? Network { get; set; }

        public virtual List<UserMembership>? Memberships { get; set; }
        public virtual List<Purchase>? Purchases { get; set; }
        public virtual List<Transaction>? Transactions { get; set; }
        public virtual List<Deposit>? Deposits { get; set; }
        public virtual List<Withdrawal>? Withdrawals { get; set; }
        public virtual List<UserRank>? Ranks { get; set; }
        public virtual List<ReferralCode> ReferralCodes { get; set; }

        [StringLength(250)]
        public string PasswordHash512 { get; set; }

        [StringLength(250)]
        public string PasswordHashBcrypt { get; set; }

        [StringLength(50)]
        public string NftId { get; set; }

        public PlatformSource PlatformSource { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        public bool IsSystemNotified { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow; // New column for creation date
    }
}
