public class WalletType : AuditableEntity, IAggregateRoot
{
    [MaxLength(50)]
    public string Name { get; set; }
    [MaxLength(50)]
    public string Code { get; set; }
    [MaxLength(250)]
    public string? Description { get; set; }
    public bool AllowTransfer { get; set; }
    public double TransferLimit { get; set; }
    public double TransferFee { get; set; }
    public bool AllowWithdrawal { get; set; }
    public double WithdrawalLimit { get; set; }
    public double WithdrawalFee { get; set; }
    public bool AllowDeposit { get; set; }
    public double DepositLimit { get; set; }
    public double DepositFee { get; set; }
    public bool AllowPurchase { get; set; }
    public double PurchaseLimit { get; set; }
    public double PurchaseFee { get; set; }
    public bool AllowBonusDeposit { get; set; }
    public double BonusDepositPercentage { get; set; }
    public bool AssignOnCreate { get; set; } = true;
}