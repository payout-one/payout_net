using Payout.Lib.Base;

namespace Payout.Lib.Interfaces
{
    public interface IModelValidation
    {
        void ValidateRequest(BaseRequest request);
    }
}