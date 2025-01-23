using Fastchannel.HttpClient.Bndes.Models.Types;

namespace Fastchannel.HttpClient.Bndes.Models.Operations.Request
{
    public class CreateSession : BaseRequest<Session>
    {
        public CreateSession(Session requestData) : base(requestData)
        {
        }
    }
}