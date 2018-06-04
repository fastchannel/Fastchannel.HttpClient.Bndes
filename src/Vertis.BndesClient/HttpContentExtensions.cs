using System.Net.Http;
using System.Threading.Tasks;

namespace Vertis.BndesClient
{
    // ReSharper disable once UnusedMember.Global
    public static class HttpContentExtensions
    {
        public static async Task<double> ReadAsDoubleAsync(this HttpContent content)
        {
            var stringValue = await content.ReadAsStringAsync().ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(stringValue))
                return default(double);

            return double.TryParse(stringValue, out var value) ? value : default(double);
        }

        public static async Task<int> ReadAsIntAsync(this HttpContent content)
        {
            var stringValue = await content.ReadAsStringAsync().ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(stringValue))
                return default(int);

            return int.TryParse(stringValue, out var value) ? value : default(int);
        }
    }
}
