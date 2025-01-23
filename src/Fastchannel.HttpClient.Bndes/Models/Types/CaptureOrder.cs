using System.Runtime.Serialization;

namespace Fastchannel.HttpClient.Bndes.Models.Types
{
    [DataContract]
    public class CaptureOrder
    {
        [DataMember(Name = "notaFiscal")]
        public string InvoiceNumbers { get; set; }

        [IgnoreDataMember]
        public int OrderId { get; set; }
    }
}