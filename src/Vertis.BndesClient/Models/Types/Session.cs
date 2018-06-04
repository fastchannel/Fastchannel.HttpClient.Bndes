using System.Runtime.Serialization;

namespace Vertis.BndesClient.Models.Types
{
    [DataContract]
    public class Session
    {
        [DataMember(Name = "cnpj")]
        public string Cnpj { get; set; }
        [DataMember(Name = "login")]
        public string Login { get; set; }
        [DataMember(Name = "senha")]
        public string Password { get; set; }
    }
}