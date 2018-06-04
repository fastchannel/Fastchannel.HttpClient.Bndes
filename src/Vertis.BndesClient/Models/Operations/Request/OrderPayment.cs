using Vertis.BndesClient.Models.Types;

namespace Vertis.BndesClient.Models.Operations.Request
{
    public class OrderPayment : BaseRequest<Types.OrderPayment>
    {
        public OrderPayment(Types.OrderPayment requestData) : base(requestData)
        {
        }
    }
}