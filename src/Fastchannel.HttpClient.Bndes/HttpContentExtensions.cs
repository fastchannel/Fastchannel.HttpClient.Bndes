using System.Net.Http;
using System.Threading.Tasks;

namespace Fastchannel.HttpClient.Bndes
{
    // ReSharper disable once UnusedMember.Global
    public static class HttpContentExtensions
    {
        public static async Task<double> ReadAsDoubleAsync(this HttpContent content, bool asPercentage = false)
        {
            var stringValue = await content.ReadAsStringAsync().ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(stringValue))
                return default;

            return double.TryParse(stringValue, out var value) ? value / (asPercentage ? 100 : 1) : default;
        }

        public static async Task<int> ReadAsIntAsync(this HttpContent content)
        {
            var stringValue = await content.ReadAsStringAsync().ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(stringValue))
                return default;

            return int.TryParse(stringValue, out var value) ? value : default;
        }
    }
}
