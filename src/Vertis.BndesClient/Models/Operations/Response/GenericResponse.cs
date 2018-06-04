using System.Collections.Generic;
using System.Runtime.Serialization;
using Vertis.BndesClient.Models.Types;

namespace Vertis.BndesClient.Models.Operations.Response
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