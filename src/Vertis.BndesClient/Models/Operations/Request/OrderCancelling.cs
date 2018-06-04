using Vertis.BndesClient.Models.Types;

namespace Vertis.BndesClient.Models.Operations.Request
{
    public class OrderCancelling : BaseRequest<Types.OrderCancelling>
    {
        public OrderCancelling(Types.OrderCancelling requestData) : base(requestData)
        {
        }
    }
}