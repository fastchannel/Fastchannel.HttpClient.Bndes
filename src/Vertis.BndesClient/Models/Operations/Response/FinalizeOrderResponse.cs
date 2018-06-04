using System.Collections.Generic;
using Vertis.BndesClient.Models.Types;

namespace Vertis.BndesClient.Models.Operations.Response
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