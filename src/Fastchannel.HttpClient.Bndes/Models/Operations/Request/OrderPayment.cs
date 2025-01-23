namespace Fastchannel.HttpClient.Bndes.Models.Operations.Request
{
    public class OrderPayment : BaseRequest<Types.OrderPayment>
    {
        public OrderPayment(Types.OrderPayment requestData) : base(requestData)
        {
        }
    }
}