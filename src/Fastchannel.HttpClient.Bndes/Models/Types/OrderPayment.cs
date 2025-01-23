using System.Runtime.Serialization;

namespace Fastchannel.HttpClient.Bndes.Models.Types
{
    [DataContract]
    public class OrderPayment
    {
        #region Fields
        private string _creditCardMonthExpiration;
        #endregion
        [DataMember(Name = "anoValidade")]
        public int CreditCardYearExpiration { get; set; }
        [DataMember(Name = "codigoSeguranca")]
        public string CreditCardSecurityCode { get; set; }

        [DataMember(Name = "numeroCartao")]
        public string CreditCardNumber { get; set; }
        [IgnoreDataMember]
        public int OrderId { get; set; }
        [DataMember(Name = "mesValidade")]
        public string CreditCardMonthExpiration
        {
            get => string.IsNullOrEmpty(_creditCardMonthExpiration) ? "" : _creditCardMonthExpiration.PadLeft(2, '0');
            set => _creditCardMonthExpiration = value;
        }
    }
}