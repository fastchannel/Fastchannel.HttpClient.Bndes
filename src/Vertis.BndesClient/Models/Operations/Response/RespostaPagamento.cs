namespace Vertis.BndesClient.Models.Operations.Response
{
    internal class RespostaPagamento
    {
        public string cnpjAdquirente { get; set; }
        public string descricao { get; set; }
        public string numeroAutorizacao { get; set; }
        public int? situacao { get; set; }
        public string tid { get; set; }
    }
}