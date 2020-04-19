using Payout.Lib.Base;
using Payout.Lib.Notifications;

namespace Payout.Lib.Interfaces
{
    public interface IPayoutSignature
    {
        string SignResponse(BaseSignedResponse response);
        string SignRequest(BaseSignedRequest request);
        string SignNotification(BaseNotification notification);
    }
}