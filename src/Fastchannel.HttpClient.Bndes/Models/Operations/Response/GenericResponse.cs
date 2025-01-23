using Fastchannel.HttpClient.Bndes.Models.Types;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Fastchannel.HttpClient.Bndes.Models.Operations.Response
{
    [DataContract]
    public class GenericResponse : BaseResponse
    {
        public GenericResponse()
        {
            Messages = new List<ResponseMessage>();
        }
        [IgnoreDataMember]
        public override bool IsHttpStatusCodeOk => HttpStatusCode == 200;
    }
}