using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Fastchannel.HttpClient.Bndes.Models.Operations.Response
{
    [DataContract]
    public class FinancingSimulationResponse : BaseResponse
    {
        public FinancingSimulationResponse()
        {
            PaymentResponses = new List<PaymentResponse>();
        }
        [IgnoreDataMember]
        public double Tax { get; set; }
        [DataMember(Name = "formasPagamento")]
        public List<PaymentResponse> PaymentResponses { get; set; }

        public override bool IsHttpStatusCodeOk => HttpStatusCode == 200;
    }
}