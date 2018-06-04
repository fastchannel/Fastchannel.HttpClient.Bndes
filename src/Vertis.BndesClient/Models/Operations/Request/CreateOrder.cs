using Vertis.BndesClient.Models.Types;

namespace Vertis.BndesClient.Models.Operations.Request
{
    public class CreateOrder : BaseRequest<Order>
    {
        public CreateOrder(Order requestData) : base(requestData)
        {
        }
    }
}