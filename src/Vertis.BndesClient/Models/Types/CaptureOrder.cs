using System.Runtime.Serialization;

namespace Vertis.BndesClient.Models.Types
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