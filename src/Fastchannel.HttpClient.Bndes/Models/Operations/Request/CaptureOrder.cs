namespace Fastchannel.HttpClient.Bndes.Models.Operations.Request
{
    public class CaptureOrder : BaseRequest<Types.CaptureOrder>
    {
        public CaptureOrder(Types.CaptureOrder requestData) : base(requestData)
        {
        }
    }
}