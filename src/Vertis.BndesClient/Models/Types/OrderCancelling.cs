using System.Runtime.Serialization;

namespace Vertis.BndesClient.Models.Types
{
    [DataContract]
    public class OrderCancelling
    {
        [DataMember(Name = "motivo")]
        public int Reason { get; set; }
        [IgnoreDataMember]
        public int OrderId { get; set; }

    }
}