namespace Fastchannel.HttpClient.Bndes.Models.Operations.Request
{
    public class OrderCancelling : BaseRequest<Types.OrderCancelling>
    {
        public OrderCancelling(Types.OrderCancelling requestData) : base(requestData)
        {
        }
    }
}