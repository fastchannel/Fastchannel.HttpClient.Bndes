using System.Collections.Generic;
using System.Runtime.Serialization;
using Vertis.BndesClient.Models.Types;

namespace Vertis.BndesClient.Models.Operations.Response
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