using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Payout.Lib
{
    public enum PayoutEnvironment
    {
        sandbox,
        app
    }


    public enum WithdrawalStatus
    {
        [EnumMember(Value = "pending")]
        Pending,

        [EnumMember(Value = "in_transit")]
        InTransit,

        [EnumMember(Value = "paid")]
        Paid
    }
}