using Payout.Lib.Base;

namespace Payout.Lib.Interfaces
{
    public interface IRequestValidation
    {
        void ModelValidation<T>(T requestModel) where T : class;
    }
}
