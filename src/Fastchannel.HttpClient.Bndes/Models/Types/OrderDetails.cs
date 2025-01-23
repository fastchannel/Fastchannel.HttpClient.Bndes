using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Fastchannel.HttpClient.Bndes.Models.Types
{
    [DataContract]
    public class OrderDetails
    {
        public OrderDetails()
        {
            Items = new List<OrderItem>();
        }
        [DataMember(Name = "bairro")]
        public string District { get; set; }
        [DataMember(Name = "cep")]
        public string ZipPostalCode { get; set; }
        [DataMember(Name = "complemento")]
        public string Complement { get; set; }
        [DataMember(Name = "endereco")]
        public string Address { get; set; }
        [DataMember(Name = "numero")]
        public string Number { get; set; }
        [DataMember(Name = "uf")]
        public string StateId { get; set; }
        [DataMember(Name = "municipio")]
        public string City { get; set; }
        [DataMember(Name = "parcelas")]
        public int OrderParcels { get; set; }
        [DataMember(Name = "valorPagamento")]
        public double OrderTotal { get; set; }
        [DataMember(Name = "itens")]
        public List<OrderItem> Items { get; set; }

        [IgnoreDataMember]
        public int OrderId { get; set; }

    }
}