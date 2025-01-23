using System.Runtime.Serialization;

namespace Fastchannel.HttpClient.Bndes.Models.Types
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