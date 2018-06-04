using Vertis.BndesClient.Models.Types;

namespace Vertis.BndesClient.Models.Operations.Request
{
    public class FinalizeOrder : BaseRequest<OrderDetails>
    {
        public FinalizeOrder(OrderDetails requestData) : base(requestData)
        {
        }
    }
}
