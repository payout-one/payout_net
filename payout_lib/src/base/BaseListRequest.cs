namespace Payout.Lib.Base
{
    public abstract class BaseListRequest : BaseRequest
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
    }
}
