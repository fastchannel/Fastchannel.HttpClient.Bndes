using Vertis.BndesClient.Models.Types;

namespace Vertis.BndesClient.Models.Operations.Request
{
    public class CreateSession : BaseRequest<Session>
    {
        public CreateSession(Session requestData) : base(requestData)
        {
        }
    }
}