using Fastchannel.HttpClient.Bndes.Models.Types;
using System.Collections.Generic;

namespace Fastchannel.HttpClient.Bndes.Models.Operations.Response
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