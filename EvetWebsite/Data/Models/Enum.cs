using System.ComponentModel;

namespace EvetWebsite.Data.Models
{
    public enum Status
    {
       
        [Description("NONE")]
        NONE = 0,
        
        [Description("Pending")]
        Pending = 4,
        [Description("Completed")]
        Completed = 5,
        [Description("Cancelled")]
        Cancelled = 6,

         
    }
    public enum PaymentType
    {

        [Description("NONE")]
        NONE = 0,

        [Description("BankTransfer")]
        BankTransfer = 4,
        [Description("Online")]
        Online = 5,
        [Description("Cash")]
        Cash = 6,


    }
    public enum UserStatus
    {
        [Description("Pending")]
        Pending = 0,

        [Description("Active")]
        Active = 2,
        [Description("Suspended")]
        Suspended = 3,

        [Description("Leave")]
        Leave = 4,
        [Description("Deleted")]
        Deleted = 6,
    }
}