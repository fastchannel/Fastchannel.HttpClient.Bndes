using System.Collections.Generic;
using Vertis.BndesClient.Models.Types;

namespace Vertis.BndesClient.Models.Operations.Response
{
    public class CreateOrderResponse : BaseResponse
    {
        public CreateOrderResponse()
        {
            Messages = new List<ResponseMessage>();
        }
        public override bool IsHttpStatusCodeOk => HttpStatusCode == 201;
        public int OrderId { get; set; }
    }
}