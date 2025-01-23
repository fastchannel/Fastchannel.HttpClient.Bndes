namespace Fastchannel.HttpClient.Bndes
{
    public class BndesSettings
    {
        public string BaseEndpoint { get; set; }

        public string ClientCertificateThumbprint { get; set; }

        public string SessionEndpoint { get; set; }

        public string FinancingSimulationEndpoint { get; set; }

        public string TaxSimulationEndpoint { get; set; }

        public string OrderEndpoint { get; set; }

        public string CatalogEndpoint { get; set; }

        //public string FinalizeOrderEndpoint { get; set; }

        //public string ExecuteOrderPaymentEndpoint { get; set; }

        //public string CancelOrderEndpoint { get; set; }

        //public string CaptureOrderEndpoint { get; set; }

        public bool EnableLog { get; set; }

        public int? DefaultTimeoutInSeconds { get; set; }
    }
}