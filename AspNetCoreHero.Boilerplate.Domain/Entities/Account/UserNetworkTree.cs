﻿
public class UserNetworkTree : BaseEntity
{
    public DefaultIdType UserId { get; set; }
    public byte[] ProfilePicture { get; set; }
    public DefaultIdType? ReferredBy { get; set; }
    public DefaultIdType? ParentUserId { get; set; }
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int? Level { get; set; }
    public string? SubscriptionType { get; set; }
    public NetworkPosition Position { get; set; }
    public int ChildCount { get; set; }

    public string Country { get; set; }

    public string Sponsor { get; set; }
}
