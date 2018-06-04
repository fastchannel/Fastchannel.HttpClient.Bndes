using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Vertis.BndesClient
{
    public class Helper
    {
        public static string ToJson<T>(T c) where T : class
        {
            //Create a stream to serialize the object to.  
            var ms = new MemoryStream();

            // Serializer the User object to the stream.  
            var ser = new DataContractJsonSerializer(typeof(T));
            ser.WriteObject(ms, c);
            var json = ms.ToArray();
            ms.Close();
            return Encoding.UTF8.GetString(json, 0, json.Length);
        }

        public static T FromJsonString<T>(string json) where T : class
        {
            var ser = new DataContractJsonSerializer(typeof(T));

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            var data = (T) ser.ReadObject(ms);
            ms.Close();

            return data;
        }
    }

    public static class StringExtensions
    {
        public static string ToFormattedString(this int value)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0:F}", value / 100d);
        }
    }

    public static class ObjectExtensions
    {
        
    }
}