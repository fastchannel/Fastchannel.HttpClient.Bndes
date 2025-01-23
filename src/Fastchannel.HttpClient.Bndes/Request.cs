using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Fastchannel.HttpClient.Bndes
{
    /// <summary>
    /// Thanks RestSharp!
    /// </summary>
    public class Request
    {
        public Request(HttpMethod method, string resource, int? timeoutInSeconds)
        {
            Method = method;
            Resource = resource;
            Parameters = new List<Parameter>();
            TimeoutInSeconds = timeoutInSeconds;
        }

        public HttpMethod Method { get; set; }

        public string Resource { get; set; }

        public List<Parameter> Parameters { get; }

        public int? TimeoutInSeconds { get; }

        public void AddCookie(string name, string value)
        {
            AddParameter(name, value, ParameterType.Cookie);
        }

        public void AddUrlSegment(string name, object value)
        {
            AddParameter(name, value, ParameterType.UrlSegment);
        }

        public void AddQueryString(string name, string value)
        {
            AddParameter(name, value, ParameterType.QueryString);
        }

        public void AddJsonBody(object obj)
        {
            AddOrUpdateParameter("body", ToJson(obj), ParameterType.RequestBody);
        }

        private static string ToJson(object c)
        {
            //Create a stream to serialize the object to.  
            var ms = new MemoryStream();

            // Serializer the User object to the stream.  
            var ser = new DataContractJsonSerializer(c.GetType());
            ser.WriteObject(ms, c);
            var json = ms.ToArray();
            ms.Close();
            return Encoding.UTF8.GetString(json, 0, json.Length);
        }

        private void AddParameter(Parameter p)
        {
            Parameters.Add(p);
        }

        private void AddParameter(string name, object value, ParameterType type)
        {
            AddParameter(new Parameter
            {
                Name = name,
                Value = value,
                Type = type
            });
        }

        private void AddOrUpdateParameter(Parameter p)
        {
            if (Parameters.Any(param => param.Name == p.Name))
            {
                var parameter = Parameters.First(param => param.Name == p.Name);
                parameter.Value = p.Value;
                return;
            }

            Parameters.Add(p);

        }

        private void AddOrUpdateParameter(string name, object value, ParameterType type)
        {
            AddOrUpdateParameter(new Parameter
            {
                Name = name,
                Value = value,
                Type = type
            });
        }

    }

    /// <summary>
    /// Parameter container for REST requests
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// Name of the parameter
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value of the parameter
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Type of the parameter
        /// </summary>
        public ParameterType Type { get; set; }

        /// <summary>
        /// Return a human-readable representation of this parameter
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            return $"{Name}={Value}";
        }
    }

    ///<summary>
    /// Types of parameters that can be added to requests
    ///</summary>
    public enum ParameterType
    {
        Cookie,
        UrlSegment,
        RequestBody,
        QueryString
    }
}