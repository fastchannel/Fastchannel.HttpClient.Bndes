using System.Collections.Generic;
using System.Runtime.Serialization;
using Vertis.BndesClient.Models.Types;

namespace Vertis.BndesClient.Models.Operations.Response
{
    [DataContract]
    public class OrderPaymentResponse : BaseResponse
    {
        public OrderPaymentResponse()
        {
            Messages = new List<ResponseMessage>();
        }
        [IgnoreDataMember]
        public override bool IsHttpStatusCodeOk => HttpStatusCode == 200;
        [DataMember(Name = "cnpjAdquirente")]
        public string AcquirerDocument { get; set; }
        [DataMember(Name = "descricao")]
        public string Description { get; set; }
        [DataMember(Name = "numeroAutorizacao")]
        public string AuthNumber { get; set; }
        [DataMember(Name = "situacao")]
        public int? Status { get; set; }
        [DataMember(Name = "tid")]
        public string Tid { get; set; }
    }
}