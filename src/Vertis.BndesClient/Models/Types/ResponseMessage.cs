using System.Runtime.Serialization;

namespace Vertis.BndesClient.Models.Types
{
    [DataContract]
    public class ResponseMessage
    {
        [DataMember(Name = "codigo")]
        public int Code { get; set; }
        [DataMember(Name = "mensagem")]
        public string Message { get; set; }
    }
}