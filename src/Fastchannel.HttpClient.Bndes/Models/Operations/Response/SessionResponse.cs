using Fastchannel.HttpClient.Bndes.Models.Types;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Fastchannel.HttpClient.Bndes.Models.Operations.Response
{
    [DataContract]
    public class SessionResponse : BaseResponse
    {
        public SessionResponse()
        {
            Messages = new List<ResponseMessage>();
        }
        [IgnoreDataMember]
        public string Id { get; set; }
        [IgnoreDataMember]
        public override bool IsHttpStatusCodeOk => HttpStatusCode == 201;
    }
}