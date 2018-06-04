using System.Collections.Generic;
using System.Runtime.Serialization;
using Vertis.BndesClient.Models.Types;

namespace Vertis.BndesClient.Models.Operations.Response
{
    [DataContract]
    public abstract class BaseResponse
    {
        [IgnoreDataMember]
        public int HttpStatusCode { get; set; }
        [DataMember(Name = "mensagens")]
        public List<ResponseMessage> Messages { get; set; }
        [IgnoreDataMember]
        public abstract bool IsHttpStatusCodeOk { get; }

        internal static T FromJsonString<T>(int httpStatusCode, string responseMessage) where T : BaseResponse, new()
        {
            var r = Helper.FromJsonString<T>(responseMessage);
            r.HttpStatusCode = httpStatusCode;
            return r;
        }
    }
}