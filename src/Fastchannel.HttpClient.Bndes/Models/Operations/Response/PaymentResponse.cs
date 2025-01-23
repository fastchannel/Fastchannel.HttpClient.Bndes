using System.Runtime.Serialization;

namespace Fastchannel.HttpClient.Bndes.Models.Operations.Response
{
    [DataContract]
    public class PaymentResponse
    {
        [DataMember(Name = "prazo")]
        public int ParcelNumber { get; set; }
        [DataMember(Name = "valorParcela")]
        public double ParcelValue { get; set; }
    }
}