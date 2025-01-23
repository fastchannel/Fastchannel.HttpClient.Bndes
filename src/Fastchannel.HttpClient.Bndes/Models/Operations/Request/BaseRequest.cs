namespace Fastchannel.HttpClient.Bndes.Models.Operations.Request
{
    public class BaseRequest<T> where T : class
    {
        public T Data { get; set; }

        public int? TimeoutInSeconds { get; set; }

        public BaseRequest(T requestData)
        {
            Data = requestData;
        }
    }
}