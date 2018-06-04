using System.Collections.Generic;
using Vertis.BndesClient.Models.Types;

namespace Vertis.BndesClient.Models.Operations.Response
{
    public class CaptureOrderResponse : BaseResponse
    {
        public CaptureOrderResponse()
        {
            Messages = new List<ResponseMessage>();
        }
        public override bool IsHttpStatusCodeOk => HttpStatusCode == 200;

        public string AcquirerDocument { get; set; }
        public string Description { get; set; }
    }
}