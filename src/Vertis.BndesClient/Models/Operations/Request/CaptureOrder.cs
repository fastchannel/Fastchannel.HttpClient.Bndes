using Vertis.BndesClient.Models.Types;

namespace Vertis.BndesClient.Models.Operations.Request
{
    public class CaptureOrder : BaseRequest<Types.CaptureOrder>
    {
        public CaptureOrder(Types.CaptureOrder requestData) : base(requestData)
        {
        }
    }
}