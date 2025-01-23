using Fastchannel.HttpClient.Bndes.Models.Types;
using System.Collections.Generic;

namespace Fastchannel.HttpClient.Bndes.Models.Operations.Response
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