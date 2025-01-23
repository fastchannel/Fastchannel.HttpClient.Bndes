using Fastchannel.HttpClient.Bndes.Models.Types;

namespace Fastchannel.HttpClient.Bndes.Models.Operations.Request
{
    public class FinalizeOrder : BaseRequest<OrderDetails>
    {
        public FinalizeOrder(OrderDetails requestData) : base(requestData)
        {
        }
    }
}
