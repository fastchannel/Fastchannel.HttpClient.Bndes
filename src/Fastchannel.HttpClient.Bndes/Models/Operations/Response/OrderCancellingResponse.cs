using Fastchannel.HttpClient.Bndes.Models.Types;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Fastchannel.HttpClient.Bndes.Models.Operations.Response
{
    [DataContract]
    public class OrderCancellingResponse : BaseResponse
    {
        public OrderCancellingResponse()
        {
            Messages = new List<ResponseMessage>();
        }
        [IgnoreDataMember]
        public override bool IsHttpStatusCodeOk => HttpStatusCode == 200;
        [DataMember(Name = "situacao")]
        public int? Status { get; set; }
        [DataMember(Name = "descricao")]
        public string Description { get; set; }
    }
}