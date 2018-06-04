using System.Collections.Generic;
using Vertis.BndesClient.Models.Types;

namespace Vertis.BndesClient.Models.Operations.Response
{
    public class CatalogResponse : BaseResponse
    {
        public CatalogResponse()
        {
            Products = new List<Product>();
        }
        public List<Types.Product> Products { get; set; }
        public override bool IsHttpStatusCodeOk => HttpStatusCode == 200;
    }
}