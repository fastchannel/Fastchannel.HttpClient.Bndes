using Fastchannel.HttpClient.Bndes.Models.Types;
using System.Collections.Generic;

namespace Fastchannel.HttpClient.Bndes.Models.Operations.Response
{
    public class CatalogResponse : BaseResponse
    {
        public CatalogResponse()
        {
            Products = new List<Product>();
        }
        public List<Product> Products { get; set; }
        public override bool IsHttpStatusCodeOk => HttpStatusCode == 200;
    }
}