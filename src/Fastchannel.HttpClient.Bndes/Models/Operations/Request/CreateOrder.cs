using Fastchannel.HttpClient.Bndes.Models.Types;

namespace Fastchannel.HttpClient.Bndes.Models.Operations.Request
{
    public class CreateOrder : BaseRequest<Order>
    {
        public CreateOrder(Order requestData) : base(requestData)
        {
        }
    }
}