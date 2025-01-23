using System.Runtime.Serialization;

namespace Fastchannel.HttpClient.Bndes.Models.Types
{
    [DataContract]
    public class OrderItem
    {
        [DataMember(Name = "produto")]
        public int ProductId { get; set; }
        [DataMember(Name = "precoUnitario")]
        public double UnitPrice { get; set; }
        [DataMember(Name = "quantidade")]
        public int Quantity { get; set; }
    }
}