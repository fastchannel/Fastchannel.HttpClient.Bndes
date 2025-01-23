using Fastchannel.HttpClient.Bndes.Models.Types;
using System.Collections.Generic;

namespace Fastchannel.HttpClient.Bndes.Models.Operations.Response
{
    public class FinalizeOrderResponse : BaseResponse
    {
        public FinalizeOrderResponse()
        {
            Messages = new List<ResponseMessage>();
        }
        public override bool IsHttpStatusCodeOk => HttpStatusCode == 200;
        public int OrderId { get; set; }
    }
}