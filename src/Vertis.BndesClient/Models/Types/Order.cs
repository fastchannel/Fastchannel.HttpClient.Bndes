using System.Runtime.Serialization;

namespace Vertis.BndesClient.Models.Types
{
    [DataContract]
    public class Order
    {
        [DataMember(Name = "cnpjComprador")]
        public string CustomerDocument { get; set; }
        [DataMember(Name = "binCartao")]
        public string CreditCardBin { get; set; }

    }
}