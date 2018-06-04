using System.Runtime.Serialization;

namespace Vertis.BndesClient.Models.Types
{
    [DataContract]
    public class Product
    {
        [DataMember(Name = "produto")]
        public string ProductId { get; set; }
        [DataMember(Name = "cnpjFabricante")]
        public string ManufacturerDocument { get; set; }
        [DataMember(Name = "numeroReferencia")]
        public string ReferenceNumber { get; set; }
        [DataMember(Name = "descricao")]
        public string Description { get; set; }
        [DataMember(Name = "designacaoComercial")]
        public string BusinessName { get; set; }
        [DataMember(Name = "modelo")]
        public string Model { get; set; }
        [DataMember(Name = "descricaoModel")]
        public string ModelDescription { get; set; }


    }
}